﻿using System;
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
            this.url = url.Replace("file:\\\\","");
            this.url = this.url.Replace("\\\\","\\");
            if(GetUpdate() == true)
            {
                this.button2.Enabled = true;
            }
            else
            {
                this.button2.Enabled = false;
                this.timer1.Enabled = true;
                this.label1.Text = "未检测到更新，即将自动退出";
            }
        }

        public static string LocalUrl = System.Environment.CurrentDirectory.Replace("Debug", "Release");
        //存放原来文件的文件夹，在这里为：D:\softwaredesigningfiles\C#\PROJECT\QQPCmgr\Documents\Visual Studio 2015\Projects\WindowsFormsApplication2\bin\Release

        private bool GetUpdate()
        {
            if(FileSize(LocalUrl)==FileSize(this.url))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static long FileSize(string filePath)
        {
            long temp = 0;
            //判断当前路径所指向的是否为文件
            if (File.Exists(filePath) == false)
            {
                string[] str1 = Directory.GetFileSystemEntries(filePath);
                foreach (string s1 in str1)
                {
                    temp += FileSize(s1);
                }
            }
            else
            {
                //定义一个FileInfo对象,使之与filePath所指向的文件向关联,
                //以获取其大小
                FileInfo fileInfo = new FileInfo(filePath);
                return fileInfo.Length;
            }
            return temp;
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
            Updating updating = new Updating(this.url,LocalUrl);
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
    }
}
