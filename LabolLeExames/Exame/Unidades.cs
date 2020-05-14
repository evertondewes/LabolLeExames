using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LabolLeExames.Exame
{
    static class Unidades
    {
        public static Dictionary<string, string> dUnidades = new Dictionary<string, string>();
        public static void carregarUnidades(string strArquivoUnidades)
        {
            FileStream fsArquivoUnidades = File.OpenRead(strArquivoUnidades);
            string strTipo;
            while (fsArquivoUnidades.CanRead && fsArquivoUnidades.Position < fsArquivoUnidades.Length)
            {
                strTipo = Util.getTexto(ref fsArquivoUnidades, 2);
                if (strTipo.Equals("03"))
                {
                    strTipo += Util.getTexto(ref fsArquivoUnidades, 8);
                    dUnidades.Add(strTipo, Util.getTexto(ref fsArquivoUnidades, 20));
                    fsArquivoUnidades.Position += 335;
                }
                else
                {
                    if (strTipo.Equals("04"))
                    {
                        strTipo += Util.getTexto(ref fsArquivoUnidades, 8);
                        dUnidades.Add(strTipo, Util.getTexto(ref fsArquivoUnidades, 12));
                        fsArquivoUnidades.Position += 343;
                    }
                    else
                    {
                        if (strTipo.Equals("02"))
                        {
                            strTipo += Util.getTexto(ref fsArquivoUnidades, 8);
                            dUnidades.Add(strTipo, (Util.getTexto(ref fsArquivoUnidades, 49)).Substring(41, 8));
                            fsArquivoUnidades.Position += 306;
                        }
                        else
                        {
                            fsArquivoUnidades.Position += 363;
                        }
                    }
                }
            }
        }

        public static string getUnidadeCampo(string strTipoCampo, string strUnidade)
        {
            if (strUnidade.Equals("00") || strTipoCampo.Equals("00"))
                return "";

            if (strTipoCampo.Equals("01") || strTipoCampo.Equals("02"))
            {
                strTipoCampo = "04";
            }
            return Unidades.dUnidades[strTipoCampo + "000000" + strUnidade];
        }
        public static string getConvenio(string strConvenio)
        {
            try
            {
                return Unidades.dUnidades["0200000" + strConvenio];
            }
            catch (Exception exp)
            {
                return strConvenio;
            }
        }
    }
}
