using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using LabolLeExames.Exame;
using LabolLeExames.Laudo;
using LabolLeExames.Cliente;

namespace LabolLeExames
{
    public partial class Form1 : Form
    {
        public string strResultado;
        public string strArquivoGerado;
        public List<LabolLeExames.Cliente.Cliente> lClientes;
        public Cliente.Cliente cAtual;
        public string strCaminhoArqTemp = Directory.GetCurrentDirectory() + "\\" + Settings1.Default.arquivos_temporarios;
        private Dictionary<string, SortedDictionary<string, Exame.Exame>> dExames = new Dictionary<string, SortedDictionary<string, LabolLeExames.Exame.Exame>>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            if (DateTime.Now.Month == 1)
            {
                dtpInicial.Value = new DateTime(DateTime.Now.Year-1, 12, 1);
            }
            else
            {
                dtpInicial.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month -1, 1);
            }
            dtpFinal.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            //dtpInicial.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //dtpFinal.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);//DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
            if (!Directory.Exists(this.strCaminhoArqTemp))
            {
                Directory.CreateDirectory(this.strCaminhoArqTemp);
            }
                                                                        
            Unidades.carregarUnidades(Settings1.Default.caminho_labol + "\\TABELAS.LAB");
            this.copiarArquivos(Settings1.Default.caminho_labol + "\\CLIENTES.LAB", this.strCaminhoArqTemp + "\\CLIENTES.LAB");
            this.copiarArquivos(Settings1.Default.caminho_labol + "\\TEXAS.LAB", this.strCaminhoArqTemp + "\\TEXAS.LAB");
            this.copiarArquivos(Settings1.Default.caminho_labol + "\\XEXAS.LAB", this.strCaminhoArqTemp + "\\XEXAS.LAB");
            this.copiarArquivos(Settings1.Default.caminho_labol + "\\TEXTAB.LAB", this.strCaminhoArqTemp + "\\TEXTAB.LAB");


        }

        public void carregarResultadoLaudo(Cliente.Cliente cCliente, string strExame)
        {
            if(strExame.Equals("//////"))
                return;

            string strExemeArquivo = strExame.Replace('/', '_');
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Labol_Resultados\\" + cCliente.ano + cCliente.mes + "\\";
            //string path = Directory.GetCurrentDirectory() + "\\" + cCliente.ano + cCliente.mes + "\\";
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                //this.copiarArquivos(Directory.GetCurrentDirectory() + "\\img\\lab_3.gif", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Labol_Resultados\\lab_3.gif");
            }

            string strFileNameBase = cCliente.codigo + "_" + strExemeArquivo + "_";
            string[] filePaths = Directory.GetFiles(path, strFileNameBase + "*www");

            if (true)//filePaths.Length == 0)
            {


                //List<String> lCamposJaAdicionados = new List<string>();
                SortedDictionary<string, Exame.Exame> sdCamposExame;

                //List<Exame.Exame> lCampos = Exames.getExames(strExame, this.strCaminhoArqTemp + "\\XEXAS.LAB", this.strCaminhoArqTemp + "\\TEXAS.LAB");
                if (this.dExames.ContainsKey(strExame))
                {
                    sdCamposExame = this.dExames[strExame];
                }
                else
                {
                    sdCamposExame = Exames.getExames(strExame, this.strCaminhoArqTemp + "\\XEXAS.LAB", this.strCaminhoArqTemp + "\\TEXAS.LAB", this.strCaminhoArqTemp + "\\TEXTAB.LAB");
                    this.dExames[strExame] = sdCamposExame;
                }

                //List<Exame.Exame> retornoPosRegistro = Exames.getCamposExameValNormais(cCliente, lCampos, strExame);

                string strArqLaudo = "\\LAUDOS" + cCliente.mes + "." + cCliente.ano.Substring(2, 2);

                this.copiarArquivos(Settings1.Default.caminho_labol + strArqLaudo, this.strCaminhoArqTemp + strArqLaudo);

                List<LaudoValor>[] lLaudoValor = LabolLeExames.Laudo.Laudos.getIndiceLaudo(cCliente.codigo, strExame, this.strCaminhoArqTemp + strArqLaudo);

                this.strResultado = Resultado.Apresentacao.montaApresentacaoResultado(cCliente, sdCamposExame, lLaudoValor);



                Random r = new Random();
                this.strArquivoGerado = path + cCliente.codigo + "_" + strExemeArquivo + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss_") + r.Next(999999).ToString() + ".html";
                TextWriter tw = new StreamWriter(this.strArquivoGerado);
                tw.WriteLine(this.strResultado);
                tw.Close();
                System.Diagnostics.Process.Start("IEXPLORE.EXE", this.strArquivoGerado);
                //this.webBrowser1.Url = new Uri(this.strArquivoGerado);
            }
            //else
            //{
            //    this.webBrowser1.Url = new Uri(filePaths[0]);

            //}

        }

        //private void btSalvar_Click(object sender, EventArgs e)
        //{
        //    this.salvarResultado();
        //}

        //public void salvarResultado()
        //{
        //    Random r = new Random();
        //    this.strArquivoGerado = Directory.GetCurrentDirectory() + "\\" + this.tbCodPaciente.Text + "_" + this.tbExame.Text + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss_") + r.Next(999999).ToString() + ".html";
        //    TextWriter tw = new StreamWriter(this.strArquivoGerado);
        //    tw.WriteLine(this.strResultado);
        //    tw.Close();
        //}

        public void copiarArquivos(string source, string destino)
        {
            System.IO.File.Copy(source, destino, true);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var lengths = from element in this.lClientes where element.codigo.Equals(this.dataGridView1[1, e.RowIndex].Value) select element;
            if (lengths != null)
            {
                this.cAtual = lengths.First();
                if (this.cAtual != null)
                {
                    this.dataGridView2.DataSource = cAtual.lExames;
                }
            }
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.cAtual != null)
                this.carregarResultadoLaudo(this.cAtual, this.dataGridView2[0, e.RowIndex].Value.ToString());
        }

        private void btSalvar_Click_1(object sender, EventArgs e)
        {

        }

        private void btPesquisar_Click(object sender, EventArgs e)
        {

            string strExame = (this.tbExame.Text + "      ").Substring(0, 6);
            this.tbExame.Text = strExame;

            if (this.tbCodPaciente.Text.Length > 8)
            {
                MessageBox.Show("Código do paciente maior que 8 digitos.");
                return;
            }

            string strCodPaciente = ((("00000000").Substring(0, 8 - this.tbCodPaciente.Text.Length)) + this.tbCodPaciente.Text);
            this.tbCodPaciente.Text = strCodPaciente;
            this.lClientes = Clientes.lerClientes(this.strCaminhoArqTemp + "\\CLIENTES.LAB", Int32.Parse(dtpInicial.Value.ToString("yyyyMMdd")), Int32.Parse(dtpFinal.Value.ToString("yyyyMMdd")), tbNome.Text, tbCodPaciente.Text, tbExame.Text, cbIncluirEmitidos.Checked);
            this.dataGridView1.DataSource = this.lClientes;
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);//DataGridViewAutoSizeColumnsMode.Fill);
            //for (int i = 0; i < this.dataGridView1.ColumnCount; i++)
            //{
            //    this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic; 
            //}

        }

        private void btMarcar_Click(object sender, EventArgs e)
        {
            //this.dataGridView1.RowCount
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                this.dataGridView1[0, i].Value = true;
            }
        }

        private void btDesmarcar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                this.dataGridView1[0, i].Value = false;
            }
        }

        //private void btImprimir_Click(object sender, EventArgs e)
        //{
        //    this.webBrowser1.Print();
        //}

    }
}