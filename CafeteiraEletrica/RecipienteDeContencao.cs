﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteiraEletrica
{
    public abstract class RecipienteDeContencao
    {
        private FonteDeAguaQuente _fonteDeAguaQuente;
        private InterfaceDoUsuario _interfaceDoUsuario;
        protected bool EstaPreparando;

        public void Inicio(InterfaceDoUsuario interfaceDoUsuario, FonteDeAguaQuente fonteDeAguaQuente)
        {
            _interfaceDoUsuario = interfaceDoUsuario;
            _fonteDeAguaQuente = fonteDeAguaQuente;
        }

        protected internal abstract bool EstaPronto { get; }

        internal abstract void Prepare();
        private protected abstract void RecipienteDeContencaoRemovido();

        private protected abstract void RecipienteDeContencaoDevolvido();

        private protected abstract void RecipienteConsumidoCompleto();

        private protected void InterrompaProducao()
        {
            _fonteDeAguaQuente.InterrompaProducao();
        }

        private protected void RetomeProducao()
        {
            _fonteDeAguaQuente.RetomeProducao();
        }

        private protected void EncerraCiclo()
        {
            _fonteDeAguaQuente.EncerraCiclo();
            _interfaceDoUsuario.EncerraCiclo();
        }

        internal abstract void FinalizaPreparo();
    }
}
