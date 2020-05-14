using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabolLeExames.Cliente
{
    public class Cliente
    {
        public string codigo { get; set; }
        public string nome { get; set; }
        public string strSexo { get; set; }
        public string strNascimentoDia;// { get; set; }
        public string strNascimentoMes;// { get; set; }
        public string strNascimentoAno;// { get; set; }
        public string dia;// { get; set; }
        public string mes;// { get; set; }
        public string ano;// { get; set; }

        public string convenio { get; set; }
        public string tabela { get; set; }
        public string matricula { get; set; }
        public string strHorarioAtendimento { get; set; }
        public string strCRM { get; set; }
        public string strNomeMedico { get; set; }

        public string DataNascimento
        {
            get
            {
                return this.strNascimentoDia + "/" + this.strNascimentoMes + "/" + this.strNascimentoAno;
            }
            set { }
        }
        public string DataAtendimento
        {
            get
            {
                return this.dia + "/" + this.mes + "/" + this.ano;
            }
            set { }
        }
        private TimeSpan _tsIdade = new TimeSpan();
        public TimeSpan tsIdade
        {
            get
            {

                int iAno;
                int iMes;
                int iDia;
                DateTime dataNascimento;
                DateTime dataAtendimento;

                if (_tsIdade == new TimeSpan())
                {
                    if (this.strNascimentoDia == "00")
                        this.strNascimentoDia = "01";
                    if (this.strNascimentoMes == "00")
                        this.strNascimentoMes = "01";

                    iAno = int.Parse(this.strNascimentoAno);
                    iMes = int.Parse(this.strNascimentoMes);
                    iDia = int.Parse(this.strNascimentoDia);
                    try
                    {
                        dataNascimento = new DateTime(iAno, iMes, iDia);
                    }
                    catch (Exception exp)
                    {
                        dataNascimento = new DateTime(iAno, iMes, 28);
                    }
                    try
                    {
                        dataAtendimento = new DateTime(int.Parse(this.ano), int.Parse(this.mes), int.Parse(this.dia));
                    }
                    catch (Exception exp)
                    {
                        dataAtendimento = new DateTime(int.Parse(this.ano), int.Parse(this.mes), 28);
                    }


                    this._tsIdade = dataAtendimento.Subtract(dataNascimento);

                }
                return this._tsIdade;

            }

            set { this._tsIdade = value; }
        }

        public string strIdade
        {
            get
            {

                DateTime age = DateTime.MinValue + this.tsIdade;

                int ageInYears = age.Year - 1;
                int ageInMonths = age.Month - 1;
                int ageInDays = age.Day - 1;
                if (ageInYears == 0)
                {
                    return ageInMonths + " mês(es) e " + ageInDays + " dia(s)";
                }
                else
                {
                    if (ageInYears == 1)
                    {
                        return "1 ano, " + ageInMonths + " mêse(s) e " + ageInDays + " dia(s)";
                    }
                    else
                    {
                        return ageInYears + " anos";
                    }
                }

            }
            set { }
        }

        public List<ClienteExame> lExames { get; set; }

        public Cliente()
        {
            this.lExames = new List<ClienteExame>();
        }

    }
}
