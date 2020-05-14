using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabolLeExames.Exame
{
    class ValorNormal
    {
        public string strLimInferior { get; set; }
        public string strLimSuperior { get; set; }
        public double dLimInferior { get; set; }
        public double dLimSuperior { get; set; }
        public string strIdadeTipo { get; set; }
        public int iIdadeInferior { get; set; }
        public int iIdadeSuperior { get; set; }
        public string strSexo { get; set; }
        public string strLimDigitacaoInferior { get; set; }
        public string strLimDigitacaoSuperior { get; set; }

        //    
        public static ValorNormal getValNormal(Cliente.Cliente cCliente, List<ValorNormal> dValoresNormais)
        {
            if(dValoresNormais == null)
                return new ValorNormal();


            TimeSpan tsIdade = cCliente.tsIdade;
            ValorNormal vnSelecionadoG = null;
            ValorNormal vnSelecionadoMF = null;


            // Procura Dias
            for(int i=0; i< dValoresNormais.Count; i++)
            {
                if(dValoresNormais[i].strIdadeTipo == "D")
                {
                    if (dValoresNormais[i].iIdadeInferior <= tsIdade.Days && tsIdade.Days <= dValoresNormais[i].iIdadeSuperior)
                    {
                        if (dValoresNormais[i].strSexo == cCliente.strSexo)
                        {
                            return dValoresNormais[i];
                        }
                        else
                        {
                            if (dValoresNormais[i].strSexo == "G")
                            {
                                vnSelecionadoG = dValoresNormais[i];
                            }
                        }
                    }
                }
            }
            if (vnSelecionadoG != null)
            {
                return vnSelecionadoG;
            }

            // Procura Meses
            int iMeses = tsIdade.Days / 30;
            for (int i = 0; i < dValoresNormais.Count; i++)
            {
                if (dValoresNormais[i].strIdadeTipo == "M")
                {
                    
                    if (dValoresNormais[i].iIdadeInferior <= iMeses && iMeses <= dValoresNormais[i].iIdadeSuperior)
                    {
                        if (dValoresNormais[i].strSexo == cCliente.strSexo)
                        {
                            return dValoresNormais[i];
                        }
                        else
                        {
                            if (dValoresNormais[i].strSexo == "G")
                            {
                                vnSelecionadoG = dValoresNormais[i];
                            }
                        }
                    }
                }
            }
            if (vnSelecionadoG != null)
            {
                return vnSelecionadoG;
            }
            // Procura Anos
            DateTime age = DateTime.MinValue + tsIdade;
            int ageInYears = age.Year - 1;
            for (int i = 0; i < dValoresNormais.Count; i++)
            {
                if (dValoresNormais[i].strIdadeTipo == "A")
                {

                    if (dValoresNormais[i].iIdadeInferior <= ageInYears && ageInYears <= dValoresNormais[i].iIdadeSuperior)
                    {
                        if (dValoresNormais[i].strSexo == cCliente.strSexo)
                        {
                            return dValoresNormais[i];
                        }
                        else
                        {
                            if (dValoresNormais[i].strSexo == "G")
                            {
                                vnSelecionadoG = dValoresNormais[i];
                            }
                        }
                    }
                }
            }
            if (vnSelecionadoG != null)
            {
                return vnSelecionadoG;
            }

            // Procura Geral
                //DateTime age = DateTime.MinValue + tsIdade;
                //int ageInYears = age.Year - 1;
            for (int i = 0; i < dValoresNormais.Count; i++)
            {
                if (dValoresNormais[i].strIdadeTipo == "G")
                {

                    if (dValoresNormais[i].iIdadeInferior <= ageInYears && ageInYears <= dValoresNormais[i].iIdadeSuperior)
                    {
                        if (dValoresNormais[i].strSexo == cCliente.strSexo)
                        {
                            return dValoresNormais[i];
                        }
                        else
                        {
                            if (dValoresNormais[i].strSexo == "G")
                            {
                                vnSelecionadoG = dValoresNormais[i];
                            }
                        }
                    }
                }
            }
            if (vnSelecionadoG != null)
            {
                return vnSelecionadoG;
            }
            return null;
        }

        public override string ToString()
        {
            if (this.strLimInferior == null || this.strLimInferior.Length == 0)
                return "";

            return  this.strLimInferior + " a " + this.strLimSuperior;
        }
    }

}
