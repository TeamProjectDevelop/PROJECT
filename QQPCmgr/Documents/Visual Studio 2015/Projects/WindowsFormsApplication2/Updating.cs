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
        public Updating(string OnlinePath,string Localurl)
        {
            InitializeComponent();
            this.Localurl = Localurl;
            this.Onlineurl = OnlinePath;
            Readxml(OnlinePath, Localurl);
        }

        public string Onlineurl = "";

        public string Localurl;

        public int upcount = 0;

        private Dictionary<string, string> ReStartCoverage = new Dictionary<string, string>();

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        public void Readxml(string OnlineDirPath, string LocalDirPath)
        {
            try
            {
                String routeBuf = OnlineDirPath + "\\files.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(@routeBuf);
                XmlNode root = xmlDoc.SelectSingleNode("Books");

                XmlNodeList xnl = root.ChildNodes;

                foreach (XmlNode xn1 in xnl)
                {
                    XmlElement xe = (XmlElement)xn1;
                    XmlNodeList xnl0 = xe.ChildNodes;
                    string filename = xnl0.Item(0).InnerText;
                    string filepath = xnl0.Item(1).InnerText;
                    string fileupstyle = xnl0.Item(2).InnerText;
                    UpdateFile(filename,filepath,fileupstyle,OnlineDirPath,LocalDirPath);
                }
                File.Copy(routeBuf, LocalDirPath + "\\files.xml", true);
                if (upcount==0)
                {
                    this.label1.Visible = false;
                    this.label2.Visible = true;
                    this.button1.Text = "确定";
                    this.progressBar1.Visible = false;
                    this.button1.Enabled = true;
                    this.timer2.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UpdateFile(string filename,string filepath,string fileupstyle,string OnlinePath,string LocalDirPath)
        {
            if(fileupstyle=="新增")
            {
                File.Copy(Path.Combine(OnlinePath,filename),Path.Combine(LocalDirPath,filename));
                upcount++;
            }
            else if(fileupstyle=="替换")
            {
                File.Copy(Path.Combine(OnlinePath, filename), Path.Combine(LocalDirPath, filename),true);
                upcount++;
            }
            else if(fileupstyle=="删除")
            {
                File.Delete(Path.Combine(LocalDirPath, filename));
                upcount++;
            }
            else if(fileupstyle=="重启覆盖")
            {
                ReStartCoverage.Add(filename, fileupstyle);
            }
            else if(fileupstyle=="执行")
            {
                ReStartCoverage.Add(filename, fileupstyle);
            }
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
            if(ReStartCoverage.Count!=0)
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    string upexepath = this.Localurl + "\\ReStartUpdate.exe";
                    startInfo.FileName = upexepath;
                    startInfo.Arguments = this.Onlineurl;
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    Process.Start(startInfo);
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
                if(ReStartCoverage.Count!=0)
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
