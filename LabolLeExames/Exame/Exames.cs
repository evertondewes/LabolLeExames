using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Collections;

namespace LabolLeExames.Exame
{
    static class Exames
    {

        //public static List<Exame> getExames(string strExame, string strCaminhoXEXAS, string strCaminhoTEXAS)
        public static SortedDictionary<string, Exame> getExames(string strExame, string strCaminhoXEXAS, string strCaminhoTEXAS, string strCaminhoTEXAB)
        {

            FileStream fsArquivoInciceExame = File.OpenRead(strCaminhoXEXAS);
            FileStream fsArquivoExame = File.OpenRead(strCaminhoTEXAS);
            //List<Exame> lCampos = new List<Exame>();
            SortedDictionary<string, Exame> lCampos = new SortedDictionary<string, Exame>();
            Dictionary<string, string> dCamposAdicionados = new Dictionary<string, string>();
            string strCodExame;
            string strKeyExame;
            int intPosicaoBloco = 512;
            string strPos1 = null;
            string strPos2 = null;
            string strInicioBloco = null;
            long lPosCampo = 0;
            int i = 0;
            int iContadorValoresNormais;
            Exame exAtual;
            ValorNormal vnAtual;
            string strTemp; string strTemp2;
            fsArquivoInciceExame.Position = 512;

            while (fsArquivoExame.CanRead && fsArquivoExame.Position < fsArquivoExame.Length)
            {
                strCodExame = Util.getTexto(ref fsArquivoExame, 6); // strCodExame1 = Util.getTexto(ref fsArquivoExame, 6);
                if (!strCodExame.Equals(strExame))
                {
                    fsArquivoExame.Position = fsArquivoExame.Position + 141;
                    continue;
                }
                exAtual = new Exame();
                exAtual.strCodExame = strCodExame;
                exAtual.strCodCampo = Util.getTexto(ref fsArquivoExame, 3);
                exAtual.chave = exAtual.strCodCampo;

                exAtual.strDescricao = Util.getTexto(ref fsArquivoExame, 30).Trim();

                exAtual.strMaterial = Util.getTexto(ref fsArquivoExame, 2);

                exAtual.strOpcoes = Util.getTexto(ref fsArquivoExame, 29);
                exAtual.strTipoCampo = Util.getTexto(ref fsArquivoExame, 2);
                exAtual.iTipoCampo = int.Parse(exAtual.strTipoCampo);

                exAtual.strGrupo = Util.getTexto(ref fsArquivoExame, 2);
                exAtual.strUnidade = Util.getTexto(ref fsArquivoExame, 2);
                exAtual.strOpcoes1 = Util.getTexto(ref fsArquivoExame, 15); //20);

                exAtual.strCasasDecimais = Util.getTexto(ref fsArquivoExame, 1);

                vnAtual = new ValorNormal();
                vnAtual.strLimInferior = Util.getTexto(ref fsArquivoExame, 9);
                vnAtual.strLimSuperior = Util.getTexto(ref fsArquivoExame, 9);

                vnAtual.strIdadeTipo = Util.getTexto(ref fsArquivoExame, 1);
                vnAtual.iIdadeInferior = int.Parse(Util.getTexto(ref fsArquivoExame, 2));
                vnAtual.iIdadeSuperior = int.Parse(Util.getTexto(ref fsArquivoExame, 2));
                vnAtual.strSexo = Util.getTexto(ref fsArquivoExame, 1);
                exAtual.iPulo = int.Parse(Util.getTexto(ref fsArquivoExame, 1));

                vnAtual.strLimDigitacaoInferior = Util.getTexto(ref fsArquivoExame, 9);
                vnAtual.strLimDigitacaoSuperior = Util.getTexto(ref fsArquivoExame, 9);
                exAtual.strOpcoes3 = Util.getTexto(ref fsArquivoExame, 1);
                exAtual.strLetra = Util.getTexto(ref fsArquivoExame, 1);

                exAtual.strOpcoes4 = Util.getTexto(ref fsArquivoExame, 10);

                strKeyExame = exAtual.strDescricao + exAtual.strTipoCampo;
                if (exAtual.iTipoCampo == 11)
                {
                    exAtual.strDescricao = Exames.getComplementoTEXAB(strCaminhoTEXAB, exAtual.strCodExame + exAtual.strCodCampo);
                    //strKeyExame = exAtual.chave + strKeyExame;
                    exAtual.posicao = i++;
                    //dCamposAdicionados.Add(strKeyExame, exAtual.chave);
                    lCampos.Add(exAtual.chave, exAtual);
                    lCampos[exAtual.chave].dValoresNormais = new List<ValorNormal>();// new Dictionary<string, List<ValorNormal>>();
                    lCampos[exAtual.chave].dValoresNormais.Add(vnAtual);
                }
                else
                {
                    if (exAtual.strDescricao.Trim().Length == 0)
                    {
                        strKeyExame += i;
                        exAtual.posicao = i++;
                        lCampos.Add(exAtual.chave, exAtual);
                    }
                    else
                    {
                        if (dCamposAdicionados.ContainsKey(strKeyExame))
                        {
                            exAtual.chave = dCamposAdicionados[strKeyExame];
                        }
                        else
                        {
                            exAtual.posicao = i++;
                            dCamposAdicionados.Add(strKeyExame, exAtual.chave);
                            lCampos.Add(exAtual.chave, exAtual);
                            lCampos[exAtual.chave].dValoresNormais = new List<ValorNormal>();// new Dictionary<string, List<ValorNormal>>();
                        }
                        if (exAtual.iTipoCampo == 1)
                        {
                            vnAtual.dLimInferior = double.Parse(vnAtual.strLimInferior) / 100;
                            vnAtual.dLimSuperior = double.Parse(vnAtual.strLimSuperior) / 100;

                            if (exAtual.strCasasDecimais == "1")
                            {
                                vnAtual.strLimInferior = String.Format("{0:#,##0.0}", vnAtual.dLimInferior);
                                vnAtual.strLimSuperior = String.Format("{0:#,##0.0}", vnAtual.dLimSuperior);
                            }
                            else
                            {
                                if (exAtual.strCasasDecimais == "0")
                                {
                                    vnAtual.strLimInferior = String.Format("{0:#,##0}", vnAtual.dLimInferior);
                                    vnAtual.strLimSuperior = String.Format("{0:#,##0}", vnAtual.dLimSuperior);
                                }
                                else
                                {
                                    vnAtual.strLimInferior = String.Format("{0:#,##0.00}", vnAtual.dLimInferior);
                                    vnAtual.strLimSuperior = String.Format("{0:#,##0.00}", vnAtual.dLimSuperior);
                                }
                            }

                            //vnAtual.strLimInferior = vnAtual.dLimInferior.ToString();
                            //vnAtual.strLimSuperior = vnAtual.dLimSuperior.ToString();
                        }
                        else
                        {
                            if (exAtual.iTipoCampo == 9)
                            {
                                vnAtual.dLimInferior = (int)(int.Parse(vnAtual.strLimInferior) / 100);
                                vnAtual.dLimSuperior = (int)(int.Parse(vnAtual.strLimSuperior) / 100);

                                vnAtual.strLimInferior = String.Format("{0:#,##0}", vnAtual.dLimInferior);
                                vnAtual.strLimSuperior = String.Format("{0:#,##0}", vnAtual.dLimSuperior);
                            }
                        }

                        lCampos[exAtual.chave].dValoresNormais.Add(vnAtual);
                    }
                }
            }
            return lCampos;
        }


        public static string getComplementoTEXAB(string strCaminhoTEXAB, string strExameCampo)
        {
            string[] strValor = new String[99];
            string strCodExameCodCampo;
            FileStream fsArquivoComplCampo = File.OpenRead(strCaminhoTEXAB);
            int iLinha;
            while (fsArquivoComplCampo.CanRead && fsArquivoComplCampo.Position < fsArquivoComplCampo.Length)
            {
                strCodExameCodCampo = Util.getTexto(ref fsArquivoComplCampo, 9);

                if (strCodExameCodCampo.Equals(strExameCampo))
                {
                    iLinha = int.Parse(Util.getTexto(ref fsArquivoComplCampo, 2));
                    strValor[iLinha] += Util.getTexto(ref fsArquivoComplCampo, 75);
                }
                else
                {
                    fsArquivoComplCampo.Position = fsArquivoComplCampo.Position + 77;
                }

            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strValor.Length; i++)
            {
                if (strValor[i] != null && strValor[i].Trim().Length != 0)
                {
                    sb.Append(strValor[i]);
                    sb.Append("\n");
                }
            }
            return sb.ToString();

        }

    }

}
