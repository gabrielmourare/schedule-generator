using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace FeriadosBrasileiros
{
    public class Feriado
    {
        public string Nome { get; set; }
        public DateTime Data { get; set; }

        public Feriado() { }
        public Feriado(string nome, DateTime data) 
        {
            Nome = nome;
            Data = data;
        }

        /// <summary>
        /// Pega Feriados do ano atual
        /// </summary>
        /// <param name="currentY"></param>
        /// <returns></returns>
        public IEnumerable<Feriado> GetHolidays(int currentY)
        {
            List<Feriado> feriados = new List<Feriado>();
            int currentYear = currentY;

            DateTime firstDayOfYear = new DateTime(currentYear, 1, 1);
            DateTime lastDayOfYear = new DateTime(currentYear, 12, 31);
            DateTime dataPascoa = calculaPascoa(currentY);

            Feriado pascoa = new Feriado("Páscoa", dataPascoa);
            List<Feriado> feriadosBaseadosPascoa = (List<Feriado>)calculaFeriadosBaseadosPascoa(dataPascoa);


            for (var day = firstDayOfYear.Date; day.Date <= lastDayOfYear.Date; day = day.AddDays(1))
            {
                switch (day.ToString("dd/MM"))
                {
                    case "01/01":
                        Feriado confratUni = new Feriado("Confraternização Universal", day);
                        feriados.Add(confratUni);
                        break;
                    case "21/04":
                        Feriado tiradentes = new Feriado("Tiradentes", day);
                        feriados.Add(tiradentes);
                        break;
                    case "01/05":
                        Feriado diaTrabalho = new Feriado("Dia do Trabalho", day);
                        feriados.Add(diaTrabalho);
                        break;
                    case "07/09":
                        Feriado independencia = new Feriado("Independência", day);
                        feriados.Add(independencia);
                        break;
                    case "12/10":
                        Feriado nsaSenhoraAparecida = new Feriado("N. Senhora Aparecida", day);
                        feriados.Add(nsaSenhoraAparecida);
                        break;
                    case "02/11":
                        Feriado finados = new Feriado("Finados", day);
                        feriados.Add(finados);
                        break;
                    case "15/11":
                        Feriado proclamacaoRepublica = new Feriado("Proclamação da República", day);
                        feriados.Add(proclamacaoRepublica);
                        break;
                    case "25/12":
                        Feriado natal = new Feriado("Natal", day);
                        feriados.Add(natal);
                        break;
                }               
            }

            feriados.Add(pascoa);
            
            foreach(Feriado feriado in feriadosBaseadosPascoa)
            {
                feriados.Add(feriado);
            }

            return feriados.ToList();
        }

        /// <summary>
        /// calcula páscoa
        /// </summary>
        /// <param name="currentYear"></param>
        /// <returns></returns>
        public DateTime calculaPascoa(int currentYear)
        {           
            int dia = new DateTime().Day;
            int mes = new DateTime().Month;

            int x = 24;
            int y = 5;
            int a, b, c, d, e;

            a = currentYear % 19;
            b = currentYear % 4;
            c = currentYear % 7;
            d = (19 * a + x) % 30;
            e = (2 * b + 4 * c + 6 * d + y) % 7;

            if(d + e > 9)
            {
                dia = (d + e - 9);
                mes = 4;
            } else
            {
                dia = (d + e + 22);
                mes = 3;
            }


            DateTime pascoa = new DateTime(currentYear, mes, dia);




            return pascoa;
        }

        public IEnumerable<Feriado> calculaFeriadosBaseadosPascoa(DateTime pascoa)
        {
            List<Feriado> feriadosBaseadosPascoa = new List<Feriado>();

            DateTime dataCarnaval = pascoa.AddDays(-47);
            DateTime dataSabadoCarnaval = pascoa.AddDays(-50);
            DateTime dataQuartaCinzas = pascoa.AddDays(-46);
            DateTime dataSextaSanta = pascoa.AddDays(-2);
            DateTime dataCorpusChristi = pascoa.AddDays(60);

            Feriado carnaval = new Feriado("Carnaval", dataCarnaval);
            Feriado sabadoCarnaval = new Feriado("Sábado de Carnaval", dataSabadoCarnaval);
            Feriado quartaDeCinzas = new Feriado("Quarta-Feira de Cinzas", dataQuartaCinzas);
            Feriado sextaSanta = new Feriado("Sexta-Feira Santa", dataSextaSanta);
            Feriado corpusChristi = new Feriado("Corpus-Christi", dataCorpusChristi);

            feriadosBaseadosPascoa.Add(carnaval);
            feriadosBaseadosPascoa.Add(sabadoCarnaval);
            feriadosBaseadosPascoa.Add(quartaDeCinzas);
            feriadosBaseadosPascoa.Add(sextaSanta);
            feriadosBaseadosPascoa.Add(corpusChristi);

            return feriadosBaseadosPascoa;

        }

        public string PegaNomeFeriado(DateTime data)
        {
            List<Feriado> feriados = GetHolidays(data.Year).ToList();

            foreach(Feriado feriado in feriados)
            {
                if( data == feriado.Data ) return feriado.Nome;
            }
            return string.Empty;
        }
        
        public string PegaNomeFeriado(DateTime data, string estado)
        {

        }

        public bool IsFeriado(DateTime data)
        {
            List<Feriado> feriados = GetHolidays(data.Year).ToList();

            foreach (Feriado feriado in feriados)
            {
                if (data == feriado.Data) return true;
            }
            return false;
        }

    }

}