using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMakerApi;

namespace CafeteiraEletrica
{
    public class M4RecipienteDeContencao : RecipienteDeContencao, IPrepararCafe
    {
        private ICoffeeMakerApi _api;

        public M4RecipienteDeContencao(ICoffeeMakerApi api)
        {
            _api = api;
        }

        protected internal override bool EstaPronto
        {
            get
            {
                return _api.GetWarmerPlateStatus() == WarmerPlateStatus.POT_EMPTY;
            }
        }

        internal override void Prepare()
        {
            EstaPreparando = true;
            _api.SetWarmerState(WarmerState.ON);
        }

        internal override void FinalizaPreparo() {
            EstaPreparando = false;
        }

        private protected override void RecipienteDeContencaoRemovido()
        {
            if (EstaPreparando && _api.GetWarmerPlateStatus() == WarmerPlateStatus.WARMER_EMPTY)
            {
                _api.SetWarmerState(WarmerState.OFF);
                InterrompaProducao();
            }

            return;
            
        }

        private protected override void RecipienteDeContencaoDevolvido()
        {
            if (EstaPreparando && _api.GetWarmerPlateStatus() != WarmerPlateStatus.WARMER_EMPTY)
            {
                Prepare();
                RetomeProducao();
            }

            return;
                
        }


        private protected override void RecipienteConsumidoCompleto() { 
            if(!EstaPreparando && _api.GetWarmerPlateStatus() == WarmerPlateStatus.WARMER_EMPTY)
            {
                EstaPreparando = false;
                _api.SetWarmerState(WarmerState.OFF);
                EncerraCiclo();
            }

            return;
        }

        public void Preparando()
        {
            RecipienteDeContencaoRemovido();
            RecipienteDeContencaoDevolvido();
            RecipienteConsumidoCompleto();
        }
    }
}
