using SpreadsheetLight;
using System.Data;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Globalization;
using FeriadosBrasileiros;

namespace ScheduleGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            QuestPDF.Settings.License = LicenseType.Community;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DATE", typeof(string));
            if (checkedListBox1.CheckedItems.Count > 0)
            {
                DateTime initialDate = dateTimePicker1.Value.Date;
                DateTime finalDate = dateTimePicker2.Value.Date;


                List<DateTime> days = new List<DateTime>();


                List<string> diasSemanaSelecionados = new List<string>();

                for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                {
                    diasSemanaSelecionados.Add(checkedListBox1.CheckedItems[i].ToString());
                }

                foreach (DateTime day in EachDay(initialDate, finalDate))
                {
                    days.Add(day);
                }

                foreach (DateTime day in days)
                {
                    foreach (string diasemana in diasSemanaSelecionados)
                    {
                        if (day.DayOfWeek.ToString() == diasemana)
                        {
                            DataRow dtRow = dt.NewRow();

                            dtRow["DATE"] = day.Date.ToShortDateString();

                            dt.Rows.Add(dtRow);
                        }

                    }
                }

                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    Feriados feriados = new Feriados();
                   

                    List<DateTime> FeriadosNacionais = (List<DateTime>)feriados.GetHolidays(DateTime.Now.Year);
                                        
                    DataRow monthRow = dt.NewRow();
                    DataRow currentRow = dt.Rows[i];

                    DataRow previousRow = dt.Rows[i - 1];

                    if(i == 1)
                    {
                        monthRow["DATE"] = Convert.ToDateTime(previousRow["DATE"]).ToString("MMMM", new CultureInfo("en-US"));
                        dt.Rows.InsertAt(monthRow, i - 1);
                    }

                    DateTime temp;
                    if (!DateTime.TryParse(previousRow["DATE"].ToString(), out temp))
                    {
                        i++; continue;
                    }                                      

                    
                    if (Convert.ToDateTime(previousRow["DATE"]).Month != Convert.ToDateTime(currentRow["DATE"]).Month)
                    {
                        monthRow["DATE"] = Convert.ToDateTime(currentRow["DATE"]).ToString("MMMM", new CultureInfo("en-US"));
                        dt.Rows.InsertAt(monthRow, i);
                    }
                }
                dataGridView1.DataSource = dt;
            }
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable? dt = dataGridView1.DataSource as DataTable;

            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }


            QuestPDF.Fluent.Document doc = QuestPDF.Fluent.Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(12, Unit.Millimetre);

                    page.Size(PageSizes.A4);

                    page.Header()
                        .ShowOnce()
                        .Background(Colors.Blue.Lighten1)
                        .Height(1, Unit.Inch)
                        .AlignCenter()
                        .Text("TESTE CRONOGRAMA");

                    page.Content().PaddingVertical(25).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Date");

                        });


                        foreach (DataRow row in dt.Rows)
                        {

                            table.Cell().Text(row["DATE"].ToString());
                        }

                    });
                });
            });

            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "PDF files (*.pdf)|*.pdf";
            sf.FilterIndex = 2;
            sf.RestoreDirectory = true;
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string filepath = sf.FileName;
                doc.GeneratePdf(filepath);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable? dt = dataGridView1.DataSource as DataTable;

            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            using (SLDocument sl = new SLDocument())
            {
                sl.ImportDataTable("A1", dt, true);
                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "Excel files (*.xlsx)|*.xlsx";
                sf.FilterIndex = 2;
                sf.RestoreDirectory = true;
                sl.SetColumnWidth("DATE", 15);
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    string filepath = sf.FileName;
                    sl.SaveAs(filepath);
                }

            }

        }
    }
}