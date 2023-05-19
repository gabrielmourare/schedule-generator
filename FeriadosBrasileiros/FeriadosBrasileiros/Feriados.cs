using System.Globalization;

namespace FeriadosBrasileiros
{
    public class Feriados
    {
        /// <summary>
        /// Pega Feriados do ano atual
        /// </summary>
        /// <param name="currentY"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> GetHolidays(int currentY)
        {
            List<DateTime> holidaysOfTheYear = new List<DateTime>();
            int currentYear = currentY;

            DateTime firstDayOfYear = new DateTime(currentYear, 1, 1);
            DateTime lastDayOfYear = new DateTime(currentYear, 12, 31);
            DateTime pascoa = calculaPascoa(currentY);
            List<DateTime> feriadosBaseadosPascoa = (List<DateTime>)calculaFeriadosBaseadosPascoa(pascoa);


            for (var day = firstDayOfYear.Date; day.Date <= lastDayOfYear.Date; day = day.AddDays(1))
            {
                switch (day.ToString("dd/MM"))
                {
                    case "01/01":
                    case "21/04":
                    case "01/05":
                    case "07/09":
                    case "12/10":
                    case "02/11":
                    case "15/11":
                    case "25/12":
                        holidaysOfTheYear.Add(day);
                        break;
                }               
            }

            holidaysOfTheYear.Add(pascoa);
            
            foreach(DateTime feriado in feriadosBaseadosPascoa)
            {
                holidaysOfTheYear.Add(feriado);
            }

            return holidaysOfTheYear.OrderBy(x => x.Month).ToList();
        }

        /// <summary>
        /// calcula páscoa
        /// </summary>
        /// <param name="currentYear"></param>
        /// <returns></returns>
        public DateTime calculaPascoa(int currentYear)
        {
            GregorianCalendar gregorianCalendar = new GregorianCalendar();
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

        public IEnumerable<DateTime> calculaFeriadosBaseadosPascoa(DateTime pascoa)
        {
            List<DateTime> feriadosBaseadosPascoa = new List<DateTime>();

            DateTime carnaval = pascoa.AddDays(-47);
            DateTime sabadoCarnaval = pascoa.AddDays(-50);
            DateTime quartaCinzas = pascoa.AddDays(-46);
            DateTime sextaSanta = pascoa.AddDays(-2);
            DateTime corpusChristi = pascoa.AddDays(60);

            feriadosBaseadosPascoa.Add(carnaval);
            feriadosBaseadosPascoa.Add(sabadoCarnaval);
            feriadosBaseadosPascoa.Add(quartaCinzas);
            feriadosBaseadosPascoa.Add(sextaSanta);
            feriadosBaseadosPascoa.Add(corpusChristi);

            return feriadosBaseadosPascoa;

        }
    }

}