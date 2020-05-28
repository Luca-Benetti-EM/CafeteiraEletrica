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
            throw new NotImplementedException();
        }

        internal override void InterrompaProducao()
        {
            _api.SetBoilerState(BoilerState.OFF);
            _api.SetReliefValveState(ReliefValveState.OPEN);
        }

        internal override void Prepare()
        {
            _api.SetBoilerState(BoilerState.ON);
            _api.SetReliefValveState(ReliefValveState.CLOSED);
        }

        internal override void RetomeProducao()
        {
            Prepare();
        }
    }
}
