//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using LabolLeExames.Exame;
//using LabolLeExames.Laudo;
//using System.Drawing;
//using System.IO;

//namespace LabolLeExames.Resultado
//{
//    static class Apresentacao
//    {
//        public static string montaApresentacaoResultado(Cliente.Cliente cCliente, SortedDictionary<string, Exame.Exame> sdCamposExame, List<LaudoValor>[] lLaudoValor)
//        {



//            StringBuilder sbHtml = new StringBuilder();
//            StringBuilder sbHtmlResultados = new StringBuilder();
//            double dValorPercentual = 0;
//            double dValorReal = 0;
//            string strValorReal;
//            string strValorPercentual;
//            string strResultado;
//            string strConteudo;
//            string strUltimaUnidade = "";
//            string strCasasDecimaisAnterior = "0";
//            int iTipoCampoAnterior = -1;
//            bool bolImprimeReferencia = false;
//            ValorNormal vnAtual = null;
//            ValorNormal vnAnterior = null;
//            // inicio do html

//            sbHtml.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01//EN'>");
//            sbHtml.Append("<HEAD>");
//            sbHtml.Append("<meta http-equiv='Content-Type' content='text/html; charset=UTF-8'/>");
//            sbHtml.Append("<STYLE type='text/css' media='print'>  body, td, th { font-family: 'Courier New', Courier, monospace; font-size: 16px; } .linha_sup_inf { border-top-style: solid; border-bottom-style: solid; } .linha_inf { margin: 0px; padding: 0px; border-bottom-width: thin; border-bottom-color: #000; } .div_folha { } .div_interno { padding-top:5mm;} .td_valores_ref {font-size: 9px; } .resultado {font-style:italic;	font-weight:bold;} .fonte_pequena {font-size: 11px;}</STYLE>");
//            sbHtml.Append("<STYLE type='text/css' media='screen'> body, td, th { font-family: 'Courier New', Courier, monospace; font-size: 16px; } .linha_sup_inf { border-top-style: solid; border-bottom-style: solid; } .linha_inf { margin: 0px; padding: 0px; border-bottom-width: thin; border-bottom-color: #000; } .div_folha { width: 240mm; margin:auto; background-color:#FFF } .div_interno { width: 210mm; height:400mm;margin:auto; padding-top:20mm;padding-bottom:20mm; background-color:#FFF } .td_valores_ref {font-size: 9px; } .resultado {font-style:italic;	font-weight:bold;} .fonte_pequena {font-size: 11px;}</STYLE>");
//            sbHtml.Append("</HEAD>");
//            sbHtml.Append("<body  align='center' bgcolor='#666666'>");
//            sbHtml.Append("<div  class='div_folha'>");
//            sbHtml.Append("<div  class='div_interno'>");
//            sbHtml.Append("<table  width='100%' align='left' > <tr><td nowrap align='center'>");
//            //sbHtml.Append("<div align='center'>");
//            //sbHtml.Append("<div >");  // cabeçalho

//            //sbHtml.Append("<div style='float:left;'><img src='" + Logotipo.data + "'  style='width: 180mm; height:15mm; background-color:#FFF' /> </div>");
//            //sbHtml.Append("<img src='" + Settings1.Default.logotipo + "'  style='width: 210mm; height:16mm; background-color:#FFF' />");
//            sbHtml.Append("<img src='data:image/jpeg;base64," + Apresentacao.ImageToBase64("logotipo.jpg") + "'  style='width: 210mm; height:16mm; background-color:#FFF' />");
//            sbHtml.Append("</td></tr>");

//            sbHtml.Append("<tr><td nowrap align='center'> <strong><pre class='linha_inf'>");
//            sbHtml.Append(Settings1.Default.TituloLinha1);
//            sbHtml.Append("</pre></strong></td></tr>");
//            sbHtml.Append("<tr><td nowrap align='center'> <strong><pre class='linha_inf'>");
//            sbHtml.Append(Settings1.Default.TituloLinha2);
//            sbHtml.Append("</pre></strong></td></tr>");
//            //sbHtml.Append("</div>");
//            //sbHtml.Append("</div>");
//            //sbHtml.Append("<div >");

//            sbHtml.Append("<tr><td>");

//            // dados do paciente
//            sbHtml.Append("<table  width='100%' align='left' class='linha_sup_inf'>");
//            sbHtml.Append("<tr><td nowrap><pre class='linha_inf'>" + ("Paciente: " + cCliente.nome + "                                                ").Substring(0, 48) + " No.: " + cCliente.codigo + "</pre></td></tr>");
//            sbHtml.Append("<tr><td nowrap><pre class='linha_inf'>" + ("Idade: " + cCliente.strIdade + "                                                ").Substring(0, 48) + " Data: " + cCliente.dia + "/" + cCliente.mes + "/" + cCliente.ano + " " + cCliente.strHorarioAtendimento + "</pre></td></tr>");
//            sbHtml.Append("<tr><td nowrap><pre class='linha_inf'>" + ("Médico(a): " + cCliente.strNomeMedico + "                                             ").Substring(0, 48) + "</pre></td></tr>");
//            sbHtml.Append("<tr><td nowrap><pre class='linha_inf'>" + ("Matrícula: " + cCliente.matricula + "                                                ").Substring(0, 48) + " Convênio: " + Unidades.getConvenio(cCliente.convenio) + "</pre></td></tr>");
//            sbHtml.Append("</table>");
//            sbHtml.Append("</td></tr><tr><td>");
//            // resultado do exame
//            sbHtml.Append("<table  width='100%' align='left'>");
//            sbHtml.Append("<tr><td> <pre class='linha_inf' style=\"font-family: 'Courier New', Courier, monospace; font-size: 11px;\">");

//            //sbHtml.Append("<tr><td nowrap> <pre class='linha_inf'>");
//            //sbHtml.Append("12345678901234567890123456789012345678901234567890123456789012345678901234567890");
//            //sbHtml.Append("</pre></td></tr>");
//            int[] iIndiceTipos = new int[99];

//            bool bApresentaCampos = true;
//            foreach (KeyValuePair<string, Exame.Exame> kvp in sdCamposExame)
//            {
//                int iTipoCampo = kvp.Value.iTipoCampo;

//                if (iTipoCampo == 7)
//                {
//                    string teste = lLaudoValor[iTipoCampo][iIndiceTipos[iTipoCampo]].valor;
//                    if (teste.Equals("N"))
//                    {
//                        bApresentaCampos = false;
//                    }
//                    //08S01NS003+E14+Ix___FB7990010000300A0015
//                    iIndiceTipos[iTipoCampo]++;
//                }
//                //if (iTipoCampo == 11)
//                //{
//                //    iTipoCampo = iTipoCampo;
//                //    string teste = lLaudoValor[0][0].valor;
//                //    if (teste.Substring(0, 3).Equals("06N"))
//                //    {
//                //        bApresentaCampos = true;
//                //    }
//                //    else
//                //    {
//                //        bApresentaCampos = false;
//                //    }
//                //    //08S01NS003+E14+Ix___FB7990010000300A0015

//                //}
//                if (iTipoCampo != 7 && bApresentaCampos)
//                {
//                    if ((lLaudoValor != null && lLaudoValor[iTipoCampo] != null) || (iTipoCampo == 11))
//                    {
//                        switch (iTipoCampo)
//                        {
//                            case 1:
//                                vnAnterior = ValorNormal.getValNormal(cCliente, kvp.Value.dValoresNormais);

//                                dValorReal = (double.Parse(lLaudoValor[iTipoCampo][iIndiceTipos[iTipoCampo]].valor) / 100);
//                                strCasasDecimaisAnterior = kvp.Value.strCasasDecimais;
//                                if (strCasasDecimaisAnterior == "1")
//                                {
//                                    strValorReal = "          " + String.Format("{0:#,##0.0}", dValorReal);
//                                }
//                                else
//                                {
//                                    if (strCasasDecimaisAnterior == "2")
//                                    {
//                                        strValorReal = "          " + String.Format("{0:#,##0.00}", dValorReal);
//                                    }
//                                    else
//                                    {
//                                        strValorReal = "          " + String.Format("{0:#,##0}", dValorReal);
//                                    }
//                                }

//                                strUltimaUnidade = Unidades.getUnidadeCampo(kvp.Value.strTipoCampo, kvp.Value.strUnidade);

//                                strConteudo = (kvp.Value.strDescricao + "...............................").Substring(0, 31);
//                                strConteudo += "<font class='resultado'>" + strValorReal.Substring(strValorReal.Length - 10, 10) + " ";
//                                strConteudo += (strUltimaUnidade + "             ").Substring(0, 13) + " </font>";

//                                string strValoresNormaisFormatados = Apresentacao.apresentacaoValoresNormais1(vnAnterior);
//                                if (strValoresNormaisFormatados.Length == 0)
//                                {
//                                    strConteudo += "<font class='fonte_pequena'>  </font>";
//                                }
//                                else
//                                {
//                                    strConteudo += "<font class='fonte_pequena'>" + Apresentacao.marcarValorAlterado(vnAnterior, dValorReal) + strValoresNormaisFormatados + " </font>";
//                                    bolImprimeReferencia = true;
//                                }

//                                sbHtmlResultados.Append(Apresentacao.gerarLinha(strConteudo));
//                                sbHtmlResultados.Append(Apresentacao.gerarLinhasVazias(kvp.Value.iPulo));

//                                iIndiceTipos[iTipoCampo]++;
//                                break;
//                            case 9:

//                                //strValorReal = "          " + String.Format("{0:n}", dValorPercentual * dValorReal / 100);

//                                int codComponente = iTipoCampo;
//                                string i = lLaudoValor[codComponente][iIndiceTipos[iTipoCampo]].valor;
//                                dValorPercentual = (double.Parse(i) / 100);

//                                strValorPercentual = "      " + String.Format("{0:#,##0}", dValorPercentual);

//                                if (strCasasDecimaisAnterior == "1")
//                                {
//                                    strValorReal = "       " + String.Format("{0:#,##0.0}", dValorPercentual * dValorReal / 100);
//                                }
//                                else
//                                {
//                                    if (strCasasDecimaisAnterior == "2")
//                                    {
//                                        strValorReal = "       " + String.Format("{0:#,##0.00}", dValorPercentual * dValorReal / 100);
//                                    }
//                                    else
//                                    {
//                                        strValorReal = "       " + String.Format("{0:#,##0}", dValorPercentual * dValorReal / 100);
//                                    }
//                                }

//                                //strValorReal = "          " + String.Format("{0:n}", dValorPercentual * dValorReal / 100);

//                                strConteudo = (kvp.Value.strDescricao + "...............................").Substring(0, 31);
//                                strConteudo += "<font class='resultado'>" + strValorPercentual.Substring(strValorPercentual.Length - 6, 6) + " % ";

//                                strConteudo += strValorReal.Substring(strValorReal.Length - 7, 7);
//                                strConteudo += strUltimaUnidade.Substring(0, 8) + "</font>";


//                                ValorNormal vnAtual9 = ValorNormal.getValNormal(cCliente, kvp.Value.dValoresNormais);
//                                string strValoresNormaisFormatados9 = Apresentacao.apresentacaoValoresNormais9(vnAtual, vnAnterior);
//                                //if (strValoresNormaisFormatados9.Length == 0)
//                                //{
//                                //    strConteudo += "<font class='fonte_pequena'>  </font>";
//                                //}
//                                //else
//                                //{
//                                //strConteudo += "<font class='fonte_pequena'>" + Apresentacao.marcarValorAlterado(vnAtual9, dValorReal) + strValoresNormaisFormatados9 + " </font>";
//                                //bolImprimeReferencia = true;
//                                ////}


//                                strConteudo += "<font class='fonte_pequena'>" + Apresentacao.apresentacaoValoresNormais9(ValorNormal.getValNormal(cCliente, kvp.Value.dValoresNormais), vnAnterior) + " </font>";


//                                strConteudo += strValoresNormaisFormatados9;
//                                sbHtmlResultados.Append(Apresentacao.gerarLinha(strConteudo));
//                                sbHtmlResultados.Append(Apresentacao.gerarLinhasVazias(kvp.Value.iPulo));
//                                iIndiceTipos[iTipoCampo]++;
//                                bolImprimeReferencia = true;
//                                break;
//                            case 2:
//                                strResultado = lLaudoValor[iTipoCampo][iIndiceTipos[iTipoCampo]].valor;
//                                if (!strResultado.Trim().Equals("Nao processado"))
//                                {
//                                    if (kvp.Value.strDescricao.Trim().Length == 0)
//                                        strConteudo = "                               ";
//                                    else
//                                        strConteudo = (kvp.Value.strDescricao + "...............................").Substring(0, 31);
//                                    strConteudo += "<font class='resultado'>" + lLaudoValor[iTipoCampo][iIndiceTipos[iTipoCampo]].valor + " " + Unidades.getUnidadeCampo(kvp.Value.strTipoCampo, kvp.Value.strUnidade) + "</font>";
//                                    //strConteudo += ValorNormal.getValNormal(cCliente, kvp.Value.dValoresNormais);
//                                    sbHtmlResultados.Append(Apresentacao.gerarLinha(strConteudo));
//                                    sbHtmlResultados.Append(Apresentacao.gerarLinhasVazias(kvp.Value.iPulo));
//                                }

//                                iIndiceTipos[iTipoCampo]++;
//                                break;
//                            case 3:
//                                strResultado = lLaudoValor[iTipoCampo][iIndiceTipos[iTipoCampo]].valor;
//                                if (!strResultado.Trim().Equals("Nao processado") && strResultado.Trim().Length != 0)
//                                {

//                                    strConteudo = (kvp.Value.strDescricao + "...............................").Substring(0, 31);
//                                    if (kvp.Value.strLetra == "P")
//                                        strConteudo += "<font class='fonte_pequena'>";
//                                    else
//                                        strConteudo += "<font class='resultado'>";

//                                    strConteudo += lLaudoValor[iTipoCampo][iIndiceTipos[iTipoCampo]].valor + "</font>";
//                                    sbHtmlResultados.Append(Apresentacao.gerarLinha(strConteudo));
//                                    sbHtmlResultados.Append(Apresentacao.gerarLinhasVazias(kvp.Value.iPulo));
//                                }
//                                iIndiceTipos[iTipoCampo]++;
//                                break;
//                            case 5:
//                                strResultado = lLaudoValor[iTipoCampo][iIndiceTipos[iTipoCampo]].valor;
//                                if (!strResultado.Trim().Equals("Nao processado") && strResultado.Trim().Length != 0)
//                                {
//                                    //iIndiceTipos[iTipoCampo]++;
//                                    //strResultado += lLaudoValor[iTipoCampo][iIndiceTipos[iTipoCampo]].valor;

//                                    if (kvp.Value.strLetra == "P")
//                                        strConteudo = "<font class='fonte_pequena'>";
//                                    else
//                                        strConteudo = "<font class='resultado'>";
//                                    strConteudo += kvp.Value.strDescricao;

//                                    strConteudo += strResultado + "</font>";
//                                    sbHtmlResultados.Append(Apresentacao.gerarLinha(strConteudo));
//                                    sbHtmlResultados.Append(Apresentacao.gerarLinhasVazias(kvp.Value.iPulo));

//                                }
//                                iIndiceTipos[iTipoCampo]++;
//                                break;
//                            case 11:
//                                vnAtual = ValorNormal.getValNormal(cCliente, kvp.Value.dValoresNormais);
//                                if (vnAtual != null)
//                                {
//                                    if (kvp.Value.strLetra == "P")
//                                        strConteudo = "<font class='fonte_pequena'>";
//                                    else
//                                        strConteudo = "<font class='resultado'>";
//                                    strConteudo += kvp.Value.strDescricao;
//                                    strConteudo += "</font>";
//                                    sbHtmlResultados.Append(Apresentacao.gerarLinha(strConteudo));
//                                    sbHtmlResultados.Append(Apresentacao.gerarLinhasVazias(kvp.Value.iPulo));
//                                    iIndiceTipos[iTipoCampo]++;
//                                }

//                                break;
//                            default:
//                                sbHtmlResultados.Append("opc nr. " + kvp.Value.strDescricao);
//                                break;
//                        }
//                        iTipoCampoAnterior = iTipoCampo;


//                    }
//                    else
//                    {
//                        if (iTipoCampo != 7 && kvp.Value.strDescricao.Trim().Length != 0)
//                        {
//                            if (kvp.Value.strLetra == "P")
//                                strConteudo = "<font class='fonte_pequena'>";
//                            else
//                                strConteudo = "<font class='resultado'>";
//                            strConteudo += kvp.Value.strDescricao + "</font>";
//                            sbHtmlResultados.Append(Apresentacao.gerarLinha(strConteudo));
//                            sbHtmlResultados.Append(Apresentacao.gerarLinhasVazias(kvp.Value.iPulo));
//                            iIndiceTipos[iTipoCampo]++;
//                        }
//                    }
//                    bApresentaCampos = true;
//                }

//            }
//            if (bolImprimeReferencia)
//            {
//                sbHtml.Append("Material: " + Unidades.getUnidadeCampo("03", sdCamposExame["000"].strMaterial) + "                                                      Refêrencias     ");
//            }
//            else
//            {
//                sbHtml.Append("Material: " + Unidades.getUnidadeCampo("03", sdCamposExame["000"].strMaterial));
//            }

//            sbHtml.Append("</pre></td></tr>");
//            sbHtml.Append(sbHtmlResultados);
//            sbHtml.Append("<tr><td  class='linha_inf' colspan=5> &nbsp;</td></tr>");
//            sbHtml.Append("</table>");
//            sbHtml.Append("</td></tr></table>");
//            sbHtml.Append("</div>");
//            sbHtml.Append("</div>");
//            //sbHtml.Append("</div>");
//            sbHtml.Append("</BODY>");
//            return sbHtml.ToString();

//        }

//        public static string gerarLinha(string strConteudo)
//        {
//            return "<tr><td nowrap> <pre class='linha_inf'>" + strConteudo + "</pre></td></tr>";
//        }

//        public static string gerarLinhasVazias(int iLinhas)
//        {
//            if (iLinhas > 1)
//                return "<tr><td> <pre class='linha_inf'> </pre></td></tr>";
//            else return "";
//        }

//        public static string apresentacaoValoresNormais9(ValorNormal vnAtual, ValorNormal vnAnterior)
//        {
//            //;// return vnAtual.strLimInferior + " a " + vnAtual.strLimSuperior + "</td><td align='right' class='td_valores_ref'>" + ((vnAtual.dLimInferior / 100) * vnAnterior.dLimInferior).ToString() + " a " + ((vnAtual.dLimSuperior / 100) * vnAnterior.dLimSuperior).ToString();
//            return "(" + ("          " + vnAtual.strLimInferior).Substring(vnAtual.strLimInferior.Length, 10) + " a " + (vnAtual.strLimSuperior + "          ").Substring(0, 10) + ")";         ////
//            //if (vnAnterior == null || ((vnAnterior.dLimInferior == 0) && (vnAnterior.dLimSuperior == 0)))
//            //{
//            //    return "";
//            //}
//            //else
//            //{
//            //string strInf = ((vnAtual.dLimInferior / 100) * vnAnterior.dLimInferior).ToString();
//            //string strSup = ((vnAtual.dLimSuperior / 100) * vnAnterior.dLimSuperior).ToString();

//            //return ("  " + vnAtual.strLimInferior).Substring(vnAtual.strLimInferior.Length, 2) + " a " + ("  " + vnAtual.strLimSuperior).Substring(vnAtual.strLimSuperior.Length, 2) + " " + ("      " + strInf).Substring(strInf.Length, 6) + " a " + ("      " + strSup).Substring(strSup.Length, 6);
//            ////}
//            string strInf = ((vnAtual.dLimInferior / 100) * vnAnterior.dLimInferior).ToString();
//            string strSup = ((vnAtual.dLimSuperior / 100) * vnAnterior.dLimSuperior).ToString();

//            return ("  " + vnAtual.strLimInferior).Substring(vnAtual.strLimInferior.Length, 2) + " a " + ("  " + vnAtual.strLimSuperior).Substring(vnAtual.strLimSuperior.Length, 2) + " " + ("      " + strInf).Substring(strInf.Length, 6) + " a " + ("      " + strSup).Substring(strSup.Length, 6);

//        }
//        public static string apresentacaoValoresNormais1(ValorNormal vnAtual)
//        {
//            if (vnAtual == null || vnAtual.strLimInferior == null || vnAtual.strLimSuperior == null || (vnAtual.strLimInferior.Equals("0,0") && vnAtual.strLimInferior.Equals("0,0")))
//            {
//                return "";
//            }
//            else
//            {
//                return "(" + ("          " + vnAtual.strLimInferior).Substring(vnAtual.strLimInferior.Length, 10) + " a " + (vnAtual.strLimSuperior + "          ").Substring(0, 10) + ")";

//            }
//        }

//        public static string marcarValorAlterado(ValorNormal vnAtual, double dValorReal)
//        {
//            if (vnAtual == null || ((vnAtual.dLimInferior <= dValorReal) && (vnAtual.dLimSuperior >= dValorReal)))
//            {
//                return " ";
//            }
//            else
//            {
//                return "*";
//            }
//        }
//        //public static string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)

//        public static string ImageToBase64(string strImgPath)
//        {
//            Image myImg = Image.FromFile(strImgPath);
//            using (MemoryStream ms = new MemoryStream())
//            {
//                // Convert Image to byte[]
//                myImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // image.Save(ms, format);
//                byte[] imageBytes = ms.ToArray();

//                // Convert byte[] to Base64 String
//                string base64String = Convert.ToBase64String(imageBytes);
//                return base64String;
//            }
//        }
//    }

//}
