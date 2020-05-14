using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LabolLeExames.Exame
{

    class Exame
    {
        public string chave { get; set; }
        public int posicao { get; set; }
        public int iTipoCampo { get; set; }
        public string strCompBloco { get; set; }
        public long posicaoIndice { get; set; }

        public string strCodExame { get; set; }
        public string strCodCampo { get; set; }
        public string strDescricao { get; set; }
        public string strMaterial { get; set; }
        public string strOpcoes { get; set; }
        public string strTipoCampo { get; set; }
        public string strGrupo { get; set; }
        public string strUnidade { get; set; }
        public string strOpcoes1 { get; set; }
        public string strCasasDecimais { get; set; }
        public List<ValorNormal> dValoresNormais { get; set; } 
        public int iPulo { get; set; }
        public string strOpcoes3 { get; set; }
        public string strLetra { get; set; }        
        public string strOpcoes4  { get; set; }

        //public string strComplemento { get; set; }
    }
}

