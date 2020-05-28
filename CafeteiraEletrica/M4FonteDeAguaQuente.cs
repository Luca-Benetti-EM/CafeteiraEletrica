using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMakerApi;

namespace CafeteiraEletrica
{
    public class M4FonteDeAguaQuente : FonteDeAguaQuente, IPrepararCafe
    {
        private readonly ICoffeeMakerApi _api;

        public M4FonteDeAguaQuente(ICoffeeMakerApi api)
        {
            _api = api;
        }

        protected internal override bool EstaPronto
        {
            get {
                return _api.GetBoilerStatus() == BoilerStatus.NOT_EMPTY;
            }
        }

        public void Preparando()
        {
            PreparoConcluido();
        }

        private protected override void PreparoConcluido()
        {
            if (EstaPreparando && _api.GetBoilerStatus() == BoilerStatus.EMPTY) {
                _api.SetBoilerState(BoilerState.OFF);
                _api.SetReliefValveState(ReliefValveState.OPEN);
                FinalizaPreparo();
            }

            return;
        }

        internal override void InterrompaProducao()
        {
            _api.SetBoilerState(BoilerState.OFF);
            _api.SetReliefValveState(ReliefValveState.OPEN);
        }

        internal override void Prepare()
        {
            EstaPreparando = true;
            _api.SetBoilerState(BoilerState.ON);
            _api.SetReliefValveState(ReliefValveState.CLOSED);
        }

        internal override void EncerraCiclo()
        {
            EstaPreparando = false;
            return;
        }

        internal override void RetomeProducao()
        {
            Prepare();
        }
    }
}
