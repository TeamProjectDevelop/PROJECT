using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Setupwebsite : Form
    {
        public event SetUpwb setupwb;

        public Setupwebsite()
        {
            InitializeComponent();
        }

        public static int GetPage(String url)
        {
            Regex regex = new Regex(@"^(file:\\\\[a-zA-Z]:\\)?[^\/\:\*\?\""\<\>\|\,]*$");
            Match m = regex.Match(url);
            if (!m.Success)
            {
                return 0 ;
            }
            if(Directory.Exists(url.Remove(0,7)))
            {
                return 1;
            }
            else
            {
                MessageBox.Show("文件夹不存在,请重新输入！");
                return 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = this.textBox1.Text;
            if (GetPage(url) == 0)
            {
                this.label3.Visible = false;
                this.label2.Visible = true;
            }
            else
            {
                this.label2.Visible = false;
                this.label3.Visible = true;
                this.label4.Visible = true;
                this.timer2.Enabled = true;
<<<<<<< HEAD
                FileStream fs = new FileStream(Application.StartupPath + "\\" + "WebSite.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.Write(url);
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
=======
>>>>>>> 344d7fc34cf825dfab57f5e18b7d5832cec320db
                setupwb(url);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(this.textBox1.Text.Length!=0)
            {
                this.button1.Enabled = true;
            }
            else
            {
                this.button1.Enabled = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

    public delegate void SetUpwb(string url);
}
