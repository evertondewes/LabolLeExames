using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace LabolLeExames.Cliente
{
    static class Clientes
    {

        public static List<Cliente> lerClientes(string strCaminhoArqCLIENTES, int iDataInicial, int iDataFinal, string strFiltroNome, string strFiltroCodPaciente, string strFiltroExame, bool bExamesEmitidos)
        {
            FileStream fsArquivoClientes = File.OpenRead(strCaminhoArqCLIENTES);
            List<Cliente> lCliente = new List<Cliente>();
            Cliente cAtual;
            ClienteExame ceAtual;
            int  i, de;

            ArrayList lista = new ArrayList();

            try
            {
                while (fsArquivoClientes.CanRead && fsArquivoClientes.Position < fsArquivoClientes.Length)
                {
                    cAtual = new Cliente();
                    cAtual.codigo = Util.getTexto(ref fsArquivoClientes, 8);

                    // string strFiltroNome, string strFiltroCodPaciente, string strFiltroExame
                    if (strFiltroCodPaciente != null && (!strFiltroCodPaciente.Trim().Equals("00000000")) && (!strFiltroCodPaciente.Trim().Equals(cAtual.codigo)))
                    {
                        fsArquivoClientes.Position = fsArquivoClientes.Position + 282+55;
                        continue;
                    }

                    if (cAtual.codigo.Equals("........") || cAtual.codigo.Equals("________"))
                        break;

                    cAtual.nome = Util.getTexto(ref fsArquivoClientes, 42);
                    cAtual.strSexo = Util.getTexto(ref fsArquivoClientes, 1);
                    cAtual.strNascimentoDia = Util.getTexto(ref fsArquivoClientes, 2);
                    cAtual.strNascimentoMes = Util.getTexto(ref fsArquivoClientes, 2);
                    cAtual.strNascimentoAno = Util.getTexto(ref fsArquivoClientes, 1);

                    string strCompAno = Util.getHexa(ref fsArquivoClientes, 1);
                    int iteste = int.Parse(strCompAno[1].ToString(), System.Globalization.NumberStyles.HexNumber);
                    if (strCompAno.Equals("7B"))
                    {
                        cAtual.strNascimentoAno = "19" + cAtual.strNascimentoAno + '0';
                    }
                    else
                    {
                        if (strCompAno.Equals("7D"))
                        {
                            cAtual.strNascimentoAno = "20" + cAtual.strNascimentoAno + '0';
                        }
                        else
                        {
                            if (strCompAno[0] == '5')
                            {
                                cAtual.strNascimentoAno = "20" + cAtual.strNascimentoAno + (7 + (int)(strCompAno[1] - '0')).ToString();
                            }
                            else
                            {
                                if (strCompAno[0] == '4')
                                {
                                    int decAgain = int.Parse(strCompAno[1].ToString(), System.Globalization.NumberStyles.HexNumber);
                                    if (decAgain < 10)
                                    {
                                        cAtual.strNascimentoAno = "19" + cAtual.strNascimentoAno + strCompAno[1];
                                    }
                                    else
                                    {
                                        cAtual.strNascimentoAno = "20" + cAtual.strNascimentoAno + (decAgain + 1).ToString()[1];
                                    }

                                }
                                else
                                {
                                    cAtual.strNascimentoAno = "verificar valor 20" + cAtual.strNascimentoAno + strCompAno[1];
                                }
                            }
                        }
                    }

                    cAtual.ano = Util.getTexto(ref fsArquivoClientes, 2);
                    try
                    {
                        if (Int32.Parse(cAtual.ano) > 70)
                            cAtual.ano = "19" + cAtual.ano;
                        else
                            cAtual.ano = "20" + cAtual.ano;
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.StackTrace);
                    }
                    cAtual.mes = Util.getTexto(ref fsArquivoClientes, 2);
                    cAtual.dia = Util.getTexto(ref fsArquivoClientes, 2);

                    de = Int32.Parse(cAtual.ano + cAtual.mes + cAtual.dia);

                    // se a data não esta no intervalo selecionado, vai para o próximo registro
                    if (((de < iDataInicial) || (de > iDataFinal)) )
                    {
                        fsArquivoClientes.Position = fsArquivoClientes.Position + 282;
                        continue;
                    }

                    fsArquivoClientes.Position = fsArquivoClientes.Position + 6;
                    cAtual.convenio = Util.getTexto(ref fsArquivoClientes, 3);

                    cAtual.matricula = Util.getTexto(ref fsArquivoClientes, 20);


                    fsArquivoClientes.Position = fsArquivoClientes.Position + 10;
                    cAtual.strHorarioAtendimento = Util.getTexto(ref fsArquivoClientes, 2) + ":" + Util.getTexto(ref fsArquivoClientes, 2);
                    fsArquivoClientes.Position = fsArquivoClientes.Position + 8;
                    cAtual.strCRM = Util.getHexa(ref fsArquivoClientes, 5).Remove(9, 1);
                    //medico = medico.Remove(9, 1);
                    cAtual.strNomeMedico = Util.getTexto(ref fsArquivoClientes, 30);
                    fsArquivoClientes.Position = fsArquivoClientes.Position + 49;

                    for (i = 0; i < 20; i++)
                    {
                        ceAtual = new ClienteExame();
                        ceAtual.strCodExame = Util.getTexto(ref fsArquivoClientes, 6);
                        if (ceAtual.strCodExame.Trim().Equals(""))  // 
                        {
                            fsArquivoClientes.Position++;
                            continue;
                        }

                        ceAtual.strStatus = Util.getTexto(ref fsArquivoClientes, 1);

                        if ((ceAtual.strStatus == "4" && bExamesEmitidos) || ceAtual.strStatus == "3")
                            cAtual.lExames.Add(ceAtual);
                    }

                    fsArquivoClientes.Position = fsArquivoClientes.Position + (141 - (i * 7));//                srClientes.BaseStream.Position += (141 - (i * 7));

                    if (i >= 20)
                        fsArquivoClientes.Position = fsArquivoClientes.Position + 6;  //srClientes.BaseStream.Position += 6;

                    if (cAtual.lExames.Count > 0)
                        lCliente.Add(cAtual);

                } // Fim do While dos Clientes
            }
            catch (Exception exp)
            {
                MessageBox.Show("Erro 0111." + exp);
            }
            finally
            {
                fsArquivoClientes.Close();
            }
            return lCliente;
        }

    }
}
