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
using System.Xml;
using System.Diagnostics;

namespace WindowsFormsApplication2
{
    public partial class Updating : Form
    {
        public Updating(string OnlinePath,string Localurl,string filename)
        {
            InitializeComponent();
            this.Localurl = Localurl;
            this.Onlineurl = OnlinePath;
            this.filename = filename;
            UPDATE();
        }

        public string filename = "";

        public string Onlineurl = "";

        public string Localurl;

        public int upcount = 0;

        private Dictionary<string, string> ReStartCoverage = new Dictionary<string, string>();

        private bool restart;

        private AutoUpdate.Update Up = new AutoUpdate.Update();
        private void UPDATE()
        {
            this.Up.SetLocalUrl(this.Localurl);
            this.Up.SetOnlineUrl(this.Onlineurl);
            this.Up.SetUpdateFileName(this.filename);
            this.Up.SetUpdateExeName("自动更新软件.exe");
            this.restart = Up.Readxml();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.progressBar1.Value==40)
            {
                this.label1.Text = "正在更新...";
            }
            
            if (this.progressBar1.Value<this.progressBar1.Maximum)
            {
                this.progressBar1.Value++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.restart==true)
            {
                try
                {
                    Up.RestartUpdate();
                }
                catch(Exception ex)
                {
                    throw;
                }
                this.Close();
                Application.Exit();
            }
            else
            {
                this.Close();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(this.progressBar1.Value==this.progressBar1.Maximum)
            {
                if(this.restart==true)
                {
                    this.label1.Visible = false;
                    this.label2.Visible = true;
                    this.button1.Text = "确定";
                }
                else
                {
                    this.label1.Text = "更新完成!";
                }
                this.button1.Enabled = true;
            }
        }

        private void Updating_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
