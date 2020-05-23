using CafeteiraEletrica.Teste.Stubs;
using CoffeeMakerApi;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace CafeteiraEletrica.Teste.Steps
{
    [Binding]
    public class EspecificacaoDaCafeteiraEletricaStep
    {
        private M4FonteDeAguaQuente _fonteDeAguaQuente;
        private M4RecipienteDeContencao _recipienteDeContencao;
        private M4InterfaceDoUsuario _interfaceDoUsuario;

        private CoffeeMakerApiStub _coffeeMakerApi = new CoffeeMakerApiStub();
       
        #region Given
       
        [Given(@"uma fonte de água quente")]
        public void GivenUmaFonteDeAguaQuente()
        {
            _fonteDeAguaQuente = new M4FonteDeAguaQuente(_coffeeMakerApi);
        }

        [Given(@"que a fonte não contém água")]
        public void GivenQueAFonteNaoContemAgua()
        {
            _coffeeMakerApi.SetBoilerStatus(BoilerStatus.EMPTY);
        }

        [Given(@"um recipiente de contenção")]
        public void GivenUmRecipienteDeContencao()
        {
            _recipienteDeContencao = new M4RecipienteDeContencao(_coffeeMakerApi);
        }

        [Given(@"que o recipiente não esteja acoplado")]
        public void GivenQueORecipienteNaoEstejaAcoplado()
        {
            _coffeeMakerApi.SetWarmerPlateStatus(WarmerPlateStatus.WARMER_EMPTY);
            _coffeeMakerApi.SetWarmerState(WarmerState.OFF);
        }

        [Given(@"uma interface de usuario")]
        public void GivenUmInterfaceDeUsuario()
        {
            _interfaceDoUsuario = new M4InterfaceDoUsuario(_coffeeMakerApi);
        }

        [Given(@"que o preparo do café foi iniciado")]
        public void GivenQueOPreparoDoCafeFoiIniciado()
        {
            Inicializacao();
        }

        [Given(@"o preparo do café e interrompido")]
        public void GivenOPreparoDoCafeEInterrompido()
        {
            Inicializacao();
            _interfaceDoUsuario.InterrompaExecucao();
        }

        [Given(@"o preparo do café e iniciado")]
        public void GivenOPreparoDoCafeEIniciado()
        {
            throw new PendingStepException();
        }

        [Given(@"o café pronto para consumo")]
        public void GivenOCafeProntoParaConsumo()
        {
            throw new PendingStepException();
        }

        [Given(@"precionando o botão de inicio")]
        public void DadoPrecionandoOBotaoDeInicio()
        {
            _coffeeMakerApi.SetBrewButtonStatus(BrewButtonStatus.PUSHED);
        }
        
        [Given(@"que a fonte contém água")]
        public void GivenQueAFonteContemAgua()
        {
            _coffeeMakerApi.SetBoilerStatus(BoilerStatus.NOT_EMPTY);
            _coffeeMakerApi.SetBoilerState(BoilerState.ON);
        }

        [Given(@"que o recipiente esteja acoplado e vazio")]
        public void GivenQueORecipienteEstejaAcoplado()
        {
            _coffeeMakerApi.SetWarmerPlateStatus(WarmerPlateStatus.POT_EMPTY);
            _coffeeMakerApi.SetWarmerState(WarmerState.ON);
        }
        #endregion

        #region When
        [When(@"iniciado o preparo de cafe")]
        public void QuandoIniciadoOPreparoDeCafe()
        {
            _interfaceDoUsuario.Inicio(_fonteDeAguaQuente, _recipienteDeContencao);
            _interfaceDoUsuario.Preparando();
        }


        [When(@"o usuario precionar o botão de inicio")]
        public void WhenOUsuarioPrecionarOBotaoDeInicio()
        {
            _coffeeMakerApi.SetIndicatorState(IndicatorState.ON);
        }

        [When(@"o recipiente de conteção e extraido")]
        public void WhenORecipienteDeContecaoEExtraido()
        {
            _coffeeMakerApi.SetWarmerPlateStatus(WarmerPlateStatus.WARMER_EMPTY);
            _interfaceDoUsuario.InterrompaExecucao();
        }

        [When(@"o recipiente de conteção e devolvido")]
        public void WhenORecipienteDeContecaoEDevolvido()
        {
            _coffeeMakerApi.SetWarmerPlateStatus(WarmerPlateStatus.POT_NOT_EMPTY);
            _interfaceDoUsuario.RetornaExecucao();
        }

        [When(@"comcluido o preparo do café")]
        public void WhenComcluidoOPreparoDoCafe()
        {
            _coffeeMakerApi.SetBoilerStatus(BoilerStatus.EMPTY);
        }

        [When(@"identificado o consumido completo")]
        public void WhenIdentificadoOConsumidoCompleto()
        {
            throw new PendingStepException();
        }

        [When(@"identificado que ainda não foi consumido por completo")]
        public void WhenIdentificadoQueAindaNaoFoiConsumidoPorCompleto()
        {
            throw new PendingStepException();
        }
        #endregion
       
        #region Then
        [Then(@"o preparo do café não e iniciado")]
        public void ThenOPreparoDoCafeNaoEIniciado()
        {
            Assert.That(_coffeeMakerApi.GetBoilerStatus(), Is.EqualTo(BoilerStatus.EMPTY));
            Assert.That(_coffeeMakerApi.GetBoilerState(), Is.EqualTo(BoilerState.OFF));
            Assert.That(_coffeeMakerApi.GetWarmerPlateStatus(), Is.EqualTo(WarmerPlateStatus.WARMER_EMPTY));
            Assert.That(_coffeeMakerApi.GetWarmerState(), Is.EqualTo(WarmerState.OFF));
            Assert.That(_coffeeMakerApi.GetReliefValveState(), Is.EqualTo(ReliefValveState.OPEN));
            Assert.That(_coffeeMakerApi.GetIndicatorState(), Is.EqualTo(IndicatorState.OFF));
            Assert.That(_coffeeMakerApi.GetBrewButtonStatus(), Is.EqualTo(BrewButtonStatus.PUSHED));
        }

        [Then(@"o preparo do café e iniciado")]
        public void ThenOPreparoDoCafeEIniciado()
        {
            Assert.That(_coffeeMakerApi.GetBoilerStatus(), Is.EqualTo(BoilerStatus.NOT_EMPTY));
            Assert.That(_coffeeMakerApi.GetBoilerState(), Is.EqualTo(BoilerState.ON));
            Assert.That(_coffeeMakerApi.GetWarmerPlateStatus(), Is.EqualTo(WarmerPlateStatus.POT_EMPTY));
            Assert.That(_coffeeMakerApi.GetWarmerState(), Is.EqualTo(WarmerState.ON));
            Assert.That(_coffeeMakerApi.GetReliefValveState(), Is.EqualTo(ReliefValveState.CLOSED));
            Assert.That(_coffeeMakerApi.GetIndicatorState(), Is.EqualTo(IndicatorState.OFF));
            Assert.That(_coffeeMakerApi.GetBrewButtonStatus(), Is.EqualTo(BrewButtonStatus.PUSHED));
        }

        [Then(@"o preparo do café e interrompido")]
        public void ThenOPreparoDoCafeEInterrompido()
        {
            Assert.That(_coffeeMakerApi.GetBoilerStatus(), Is.EqualTo(BoilerStatus.NOT_EMPTY));
            Assert.That(_coffeeMakerApi.GetBoilerState(), Is.EqualTo(BoilerState.OFF));
            Assert.That(_coffeeMakerApi.GetWarmerPlateStatus(), Is.EqualTo(WarmerPlateStatus.WARMER_EMPTY));
            Assert.That(_coffeeMakerApi.GetWarmerState(), Is.EqualTo(WarmerState.OFF));
            Assert.That(_coffeeMakerApi.GetReliefValveState(), Is.EqualTo(ReliefValveState.OPEN));
            Assert.That(_coffeeMakerApi.GetIndicatorState(), Is.EqualTo(IndicatorState.OFF));
            Assert.That(_coffeeMakerApi.GetBrewButtonStatus(), Is.EqualTo(BrewButtonStatus.PUSHED));
        }

        [Then(@"o preparo do café e retomado")]
        public void ThenOPreparoDoCafeERetomado()
        {
            Assert.That(_coffeeMakerApi.GetBoilerStatus(), Is.EqualTo(BoilerStatus.NOT_EMPTY));
            Assert.That(_coffeeMakerApi.GetBoilerState(), Is.EqualTo(BoilerState.ON));
            Assert.That(_coffeeMakerApi.GetWarmerPlateStatus(), Is.EqualTo(WarmerPlateStatus.POT_NOT_EMPTY));
            Assert.That(_coffeeMakerApi.GetWarmerState(), Is.EqualTo(WarmerState.ON));
            Assert.That(_coffeeMakerApi.GetReliefValveState(), Is.EqualTo(ReliefValveState.CLOSED));
            Assert.That(_coffeeMakerApi.GetIndicatorState(), Is.EqualTo(IndicatorState.OFF));
            Assert.That(_coffeeMakerApi.GetBrewButtonStatus(), Is.EqualTo(BrewButtonStatus.PUSHED));
        }

        [Then(@"mantido aquecido até ser consumo por completo")]
        public void ThenMantidoAquecidoAteSerConsumoPorCompleto()
        {
            Assert.That(_coffeeMakerApi.GetBoilerStatus(), Is.EqualTo(BoilerStatus.EMPTY));
            Assert.That(_coffeeMakerApi.GetBoilerState(), Is.EqualTo(BoilerState.OFF));
            Assert.That(_coffeeMakerApi.GetWarmerPlateStatus(), Is.EqualTo(WarmerPlateStatus.POT_NOT_EMPTY));
            Assert.That(_coffeeMakerApi.GetWarmerState(), Is.EqualTo(WarmerState.ON));
            Assert.That(_coffeeMakerApi.GetReliefValveState(), Is.EqualTo(ReliefValveState.OPEN));
            Assert.That(_coffeeMakerApi.GetIndicatorState(), Is.EqualTo(IndicatorState.ON));
            Assert.That(_coffeeMakerApi.GetBrewButtonStatus(), Is.EqualTo(BrewButtonStatus.PUSHED));
        }

        [Then(@"o café está pronto para o consumo")]
        public void ThenOCafeEstaProntoParaOConsumo()
        {
            _coffeeMakerApi.SetIndicatorState(IndicatorState.ON);
        }

        [Then(@"o ciclo de preparo e finalizado")]
        public void ThenOCicloDePreparoEFinalizado()
        {
            throw new PendingStepException();
        }
        #endregion

        private void Inicializacao()
        {
            GivenUmaFonteDeAguaQuente();
            GivenQueAFonteContemAgua();
            GivenUmRecipienteDeContencao();
            GivenQueORecipienteEstejaAcoplado();
            GivenUmInterfaceDeUsuario();
            DadoPrecionandoOBotaoDeInicio();
            QuandoIniciadoOPreparoDeCafe();
        }
    }
}
