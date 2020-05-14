using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace LabolLeExames.Laudo
{
    class Laudos
    {
        public SortedList slLaudos = new SortedList();
        public ArrayList arrLaudos = new ArrayList();

        public static List<LaudoValor>[] getIndiceLaudo(string strCodPaciente, string strCodExame, string strArqLaudos)
        {
            List<LaudoValor>[] valores = new List<LaudoValor>[99];
            string strCodCampo;
            string strSeqCampo;
            string strDescricao;
            int iCampo = 0;
            List<int> iValores = new List<int>();
            FileStream fsArquivoExame = File.OpenRead(strArqLaudos);
            string strRegistro = strCodPaciente + strCodExame;
            string strCod;
            int iContCampo5 = 0;
            string strAcumula5 = "";
            string strAcumula5Temp;

            valores[7] = new List<LaudoValor>();
            while (fsArquivoExame.CanRead && fsArquivoExame.Position < fsArquivoExame.Length)
            {
                strCod = Util.getTexto(ref fsArquivoExame, 14);
                if (strCod.Trim().Equals(strRegistro.Trim()))
                {
                    strCodCampo = Util.getTexto(ref fsArquivoExame, 2);
                    strSeqCampo = Util.getTexto(ref fsArquivoExame, 2);
                    int iCodCampo = int.Parse(strCodCampo);
                    int iSeqCampo = int.Parse(strSeqCampo);
                    if (valores[iCodCampo] == null)
                        valores[iCodCampo] = new List<LaudoValor>();
                    switch (iCodCampo)
                    {
                        case 0:
                            strDescricao = Util.getTexto(ref fsArquivoExame, 20) + Util.getHexa(ref fsArquivoExame, 10);
                            iValores.Add(iCodCampo);
                            valores[iCodCampo].Add(new LaudoValor(iCampo, strCodCampo, (iSeqCampo * 100 + iCampo).ToString(), strDescricao));
                            iCampo++;
                            valores[7].Add(new LaudoValor(iCampo, "07", (100 + iCampo).ToString(), strDescricao.Substring(6, 1)));
                            iCampo++;
                            valores[7].Add(new LaudoValor(iCampo, "07", (200 + iCampo).ToString(), strDescricao.Substring(2, 1)));

                            break;
                        case 1:
                        case 9:
                            strDescricao = Util.getHexa(ref fsArquivoExame, 30);
                            iValores.Add(iCodCampo);
                            for (int iLaudoValor = 0; iLaudoValor < 7; iLaudoValor++)
                            {
                                valores[iCodCampo].Add(new LaudoValor(iCampo, strCodCampo, (iSeqCampo * 100 + iCampo).ToString(), strDescricao.Substring(iLaudoValor * 8, 7)));
                                iCampo++;
                            }
                            break;
                        case 2:
                        case 3:
                            valores[iCodCampo].Add(new LaudoValor(iCampo, strCodCampo, (iSeqCampo * 100 + iCampo).ToString(), Util.getTexto(ref fsArquivoExame, 15)));
                            iCampo++;
                            valores[iCodCampo].Add(new LaudoValor(iCampo, strCodCampo, (iSeqCampo * 100 + iCampo).ToString(), Util.getTexto(ref fsArquivoExame, 15)));
                            iCampo++;
                            break;
                        case 8:
                        case 5:
                            if (iContCampo5 == 1 || iContCampo5 == 3)
                            {
                                strAcumula5 += Util.getTexto(ref fsArquivoExame, 30) + "\n";
                            }
                            else
                            {
                                
                               strAcumula5Temp = Util.getTexto(ref fsArquivoExame, 30);
                                if (!strAcumula5Temp.Trim().Equals("@"))
                                {
                                    strAcumula5 += strAcumula5Temp;
                                }
                            }

                            if (iContCampo5 == 5)
                            {
                                valores[iCodCampo].Add(new LaudoValor(iCampo, strCodCampo, (iSeqCampo * 100 + iCampo).ToString(), strAcumula5));
                                iCampo++;
                                iContCampo5 = 0;
                                strAcumula5 = "";
                            }
                            else
                            {
                                iContCampo5++;
                            }
                            break;
                        default:
                            fsArquivoExame.Position = fsArquivoExame.Position + 30;
                            break;
                    }
                }
                else
                {
                    fsArquivoExame.Position = fsArquivoExame.Position + 34;
                }
            }
            fsArquivoExame.Close();
            for (int indice = 0; indice < iValores.Count; indice++)
            {
                valores[iValores[indice]].Sort((x, y) => string.Compare(x.sequencial, y.sequencial));

                //newList[iValores[indice]] = (List<LaudoValor>)this.valores[iValores[indice]].OrderBy(x => x.sequencial);
            }
            return valores;

        }
    }
}
