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
    public partial class Updating : Form
    {
        public Updating(string sorcePath,string savePath)
        {
            InitializeComponent();
            DelectDir(savePath);
            CopyDirectory(sorcePath, savePath);
        }

        public string url = "";

        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        public void CopyDirectory(string sourceDirPath, string saveDirPath)
        {
            try
            {
                if (!Directory.Exists(saveDirPath))
                {
                    Directory.CreateDirectory(saveDirPath);
                }
                string[] files = Directory.GetFiles(sourceDirPath);
                foreach (string file in files)
                {
                    string pFilePath = saveDirPath + "\\" + Path.GetFileName(file);
                    if (File.Exists(pFilePath))
                        continue;
                    File.Copy(file, pFilePath, true);
                }

                string[] dirs = Directory.GetDirectories(sourceDirPath);
                foreach (string dir in dirs)
                {
                    CopyDirectory(dir, saveDirPath + "\\" + Path.GetFileName(dir));
                }
            }
            catch (Exception ex)
            {

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

        private void Updating_Load(object sender, EventArgs e)
        {

        }
    }
}
