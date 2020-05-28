using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteiraEletrica
{
    public abstract class FonteDeAguaQuente
    {
        private RecipienteDeContencao _recipienteDeContencao;
        private InterfaceDoUsuario _interfaceDoUsuario;
        protected bool EstaPreparando;

        public void Inicio(InterfaceDoUsuario interfaceDoUsuario, RecipienteDeContencao recipienteDeContencao)
        {
            _interfaceDoUsuario = interfaceDoUsuario;
            _recipienteDeContencao = recipienteDeContencao;
        }

        protected internal abstract bool EstaPronto { get; }
        internal abstract void Prepare();
        internal abstract void InterrompaProducao();
        internal abstract void RetomeProducao();

        internal abstract void EncerraCiclo();

        private protected void FinalizaPreparo()

        {
            _recipienteDeContencao.FinalizaPreparo();
            _interfaceDoUsuario.FinalizaPreparo();
        }
        private protected abstract void PreparoConcluido();
    }
}
