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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "软件自动更新软件";
            dgv.DataSource = createSouce();
        }

        private DataTable createSouce()//创建数据源  
        {
            DataTable dt = new DataTable();//创建DataTable对象  
            dt.Columns.Add("列一", System.Type.GetType("System.Int32"));
            DataColumn dc = new DataColumn("列二", System.Type.GetType("System.String"));
            dt.Columns.Add(dc);//添加1字符串列  
            for (int i = 0; i < 20; i++)
            {
                DataRow dr = dt.NewRow();//创建1行  
                dr[0] = i;//添加第一列数据  
                dr[1] = Convert.ToString((i + 80) / 3);//添加第二列数据  
                dt.Rows.Add(dr);//把行加入到；列表中       
            }
            return dt;
        }

        private void 另存为ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
