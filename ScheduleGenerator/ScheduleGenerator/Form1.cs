using SpreadsheetLight;
using System.Data;

namespace ScheduleGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                            dtRow["DATE"] = day.Date.DayOfWeek.ToString() + " " + day.Date.ToShortDateString();

                            dt.Rows.Add(dtRow);
                        }
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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;

           

            using (SLDocument sl = new SLDocument())
            {
                sl.ImportDataTable("A1", dt, true);
                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "Excel files (*.xlsx)|*.xlsx";
                sf.FilterIndex = 2;
                sf.RestoreDirectory = true;
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(@"D:\somefile.json", sf.FileName, true);
                }
                sl.SaveAs();
            }
           
        }
    }
}