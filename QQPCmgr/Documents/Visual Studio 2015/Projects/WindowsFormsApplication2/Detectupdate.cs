using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace WindowsFormsApplication2
{
    

    public partial class Detectupdate : Form
    {
        private string url;

        public Detectupdate(string url)
        {
            InitializeComponent();
            this.url = url.Replace("file:\\\\", "");
            checkupdate();
        }

        public static string LocalUrl = System.Environment.CurrentDirectory;
        //存放原来文件的文件夹，在这里为：D:\softwaredesigningfiles\C#\PROJECT\QQPCmgr\Documents\Visual Studio 2015\Projects\WindowsFormsApplication2\bin\Debug

        private void checkupdate()
        {
            AutoUpdate.Detect Det = new AutoUpdate.Detect();
            Det.getOnlineXml(this.url);
            Det.getLocalXml(LocalUrl);
            Det.getXmlName("files.xml");
            int Result = Det.GetUpdate();
            if (Result == 2)
            {
                this.button2.Enabled = true;
                this.label1.Visible = true;
                this.label2.Visible = false;
            }
            else if (Result == 1)
            {
                this.button2.Enabled = false;
                this.timer1.Enabled = true;
                this.label2.Visible = false;
                this.label1.Text = "未检测到更新，即将自动退出";
                this.label1.Visible = true;
            }
            else
            {
                this.timer1.Enabled = true;
                this.label2.Visible = true;
                this.label1.Visible = false;
                this.button2.Enabled = false;
            }
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
            Updating updating = new Updating(this.url,LocalUrl,"files.xml");
            updating.ShowDialog();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Detectupdate_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
