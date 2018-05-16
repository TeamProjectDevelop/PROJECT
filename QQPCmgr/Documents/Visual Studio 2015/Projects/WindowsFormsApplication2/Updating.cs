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
    public partial class Updating : Form
    {
        public Updating()
        {
            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.progressBar1.Value==40)
            {
                this.label1.Text = "正在安装...";
            }
            
            if (this.progressBar1.Value<this.progressBar1.Maximum)
            {
                this.progressBar1.Value++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(this.progressBar1.Value==this.progressBar1.Maximum)
            {
                this.label1.Text = "更新完成!";
                this.button1.Enabled = true;
            }
        }
    }
}
