using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabolLeExames.Laudo
{
    class LaudoValor
    {
        public int posicao { get; set; }
        public string tipo { get; set; }
        public string sequencial { get; set; }
        public string valor { get; set; }

        public LaudoValor(int posicao, string tipo, string sequencial, string valor)
        {
            this.posicao = posicao;
            this.tipo = tipo;
            this.sequencial = sequencial;
            this.valor = valor;
        }
    }
}
