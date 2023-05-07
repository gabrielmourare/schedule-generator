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
            if(checkedListBox1.CheckedItems.Count > 0)
            {
                List<string> diasSemanaSelecionados = new List<string>();
                
                for(int i = 0;i < checkedListBox1.CheckedItems.Count; i++)
                {
                    diasSemanaSelecionados.Add(checkedListBox1.CheckedItems[i].ToString());
                }

               
                
            }

        }
    }
}