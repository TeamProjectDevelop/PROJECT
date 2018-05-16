using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApplication2
{
    public partial class Detectupdate : Form
    {
        public Detectupdate()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
     
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                for (int i = 0; i < this.dataGridView1.RowCount; i++)
                {
                    if (this.dataGridView1.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")//或者=="False"
                    {
                        this.dataGridView1.Rows[i].Cells[0].Value = false;
                    }
                    else
                    {
                        this.dataGridView1.Rows[i].Cells[0].Value = true;
                    }
                }
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Updating updating = new Updating();
            updating.ShowDialog();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Detectupdate_Load(object sender, EventArgs e)
        {

        }
    }
}
