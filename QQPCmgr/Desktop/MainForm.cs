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

namespace WindowsFormsApplication2
{
    public partial class MainForm : Form
    {
        public string url = "";
        public MainForm()
        {
            InitializeComponent();
            StreamReader sr = new StreamReader(Application.StartupPath + "\\" + "WebSite.txt", Encoding.Default);
            String line = sr.ReadLine();
            this.url = line;
            sr.Close();
        }

        private ComboBox cmb_Temp = new ComboBox();

        //定义下拉框
        private void BindStyle()
        {
            DataTable dtStyle = new DataTable();
            dtStyle.Columns.Add("Value");
            dtStyle.Columns.Add("Name");

            DataRow drStyle;
            drStyle = dtStyle.NewRow();
            drStyle[0] = "0";
            drStyle[1] = "替换";
            dtStyle.Rows.Add(drStyle);
            drStyle = dtStyle.NewRow();
            drStyle[0] = "1";
            drStyle[1] = "删除";
            dtStyle.Rows.Add(drStyle);
            drStyle = dtStyle.NewRow();
            drStyle[0] = "2";
            drStyle[1] = "新增";
            dtStyle.Rows.Add(drStyle);
            drStyle = dtStyle.NewRow();
            drStyle[0] = "3";
            drStyle[1] = "启动";
            dtStyle.Rows.Add(drStyle);
            drStyle = dtStyle.NewRow();
            drStyle[0] = "4";
            drStyle[1] = "重启覆盖";
            dtStyle.Rows.Add(drStyle);

            cmb_Temp.ValueMember = "Value";
            cmb_Temp.DisplayMember = "Name";
            cmb_Temp.DataSource = dtStyle;
            cmb_Temp.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private DataTable dtData = new DataTable();

        private void BindData()
        {
            DataRow drData;
            drData = dtData.NewRow();
            drData[0] = "MainForm.resx";
            drData[1] = @"D:\QQPCmgr\Desktop\MainForm.resx";
            drData[2] = "1";
            drData[3] = "32KB";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = "手机扫码比价软件不那么好使_陈博.pdf";
            drData[1] = @"D:\手机扫码比价软件不那么好使_陈博.pdf";
            drData[2] = "4";
            drData[3] = "54KB";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = "Composer-Setup.exe";
            drData[1] = @"D:\Composer-Setup.exe";
            drData[2] = "3";
            drData[3] = "12KB";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = "Blog.dotx";
            drData[1] = @"D:\office\Templates\2052\Blog.dotx";
            drData[2] = "1";
            drData[3] = "14KB";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = "BUSINESS.ONE";
            drData[1] = @"D:\office\ONENOTE\15\BUSINESS.ONE";
            drData[2] = "2";
            drData[3] = "4KB";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = "Composer-Setup.exe";
            drData[1] = @"D:\Composer-Setup.exe";
            drData[2] = "3";
            drData[3] = "12KB";
            dtData.Rows.Add(drData);

            this.dgv.DataSource = dtData;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 绑定性别下拉列表框
            BindStyle();

            //绑定数据表
            dtData.Columns.Add("文件名");
            dtData.Columns.Add("文件路径");
            dtData.Columns.Add("更新方式");
            dtData.Columns.Add("大小");
            dgv.DataSource = dtData;
            DataGridViewButtonColumn column = new DataGridViewButtonColumn();
            //设定列的名字
            //在所有按钮上表示"点击阅览"
            column.UseColumnTextForButtonValue = true;
            column.Text = "浏览";
            //向DataGridView追加
            dgv.Columns.Add(column);
            dgv.Columns[4].DefaultCellStyle.NullValue = "浏览";
            dgv.Columns[4].Width = 67;

            // 添加下拉列表框事件
            cmb_Temp.SelectedIndexChanged += new EventHandler(cmb_Temp_SelectedIndexChanged);

            // 将下拉列表框加入到DataGridView控件中
            this.dgv.Controls.Add(cmb_Temp);

            this.Text = "自动更新软件";
            dgv.Font = new Font("宋体", 14);
            dgv.Columns[1].MinimumWidth = 300;
            dgv.Columns[0].Width = 130;
        }

        private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "替换")
            {
                dgv.CurrentCell.Value = "替换";
                dgv.CurrentCell.Tag = "0";
            }
            else if (((ComboBox)sender).Text == "删除")
            {
                dgv.CurrentCell.Value = "删除";
                dgv.CurrentCell.Tag = "1";
            }
            else if (((ComboBox)sender).Text == "新增")
            {
                dgv.CurrentCell.Value = "新增";
                dgv.CurrentCell.Tag = "2";
            }
            else if (((ComboBox)sender).Text == "执行")
            {
                dgv.CurrentCell.Value = "执行";
                dgv.CurrentCell.Tag = "3";
            }
            else
            {
                dgv.CurrentCell.Value = "重启覆盖";
                dgv.CurrentCell.Tag = "4";
            }
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

        private void 工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 检测更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (this.url == "" )
            {
                MessageBox.Show("更新路径还没有设置，请先设置更新路径");
                return ;
            }
            else
            {
                Detectupdate detectupdate = new Detectupdate(this.url);
                detectupdate.Show();
            }
        }

        private void fontDialog3_Apply(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 设置网址ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setupwebsite setupwebsite = new Setupwebsite();
            setupwebsite.setupwb += new SetUpwb(setupwebsite_setupwb);
            setupwebsite.ShowDialog();
        }

        void setupwebsite_setupwb(string url)
        {
            this.url = url;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgv_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.cmb_Temp.Visible = false;
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.cmb_Temp.Visible = false;
        }

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dgv.CurrentCell.ColumnIndex == 2)
                {
                    Rectangle rect = dgv.GetCellDisplayRectangle(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex, false);
                    string styleValue = dgv.CurrentCell.Value.ToString();
                    if (styleValue == "0")
                    {
                        cmb_Temp.Text = "替换";
                    }
                    if (styleValue == "1")
                    {
                        cmb_Temp.Text = "删除";
                    }
                    if (styleValue == "2")
                    {
                        cmb_Temp.Text = "新增";
                    }
                    if (styleValue == "3")
                    {
                        cmb_Temp.Text = "执行";
                    }
                    else
                    {
                        cmb_Temp.Text = "重启覆盖";
                    }
                    cmb_Temp.Left = rect.Left;
                    cmb_Temp.Top = rect.Top;
                    cmb_Temp.Width = rect.Width;
                    cmb_Temp.Height = rect.Height;
                    cmb_Temp.Visible = true;
                }
                else
                {
                    cmb_Temp.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < this.dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells[2].Value != null && dgv.Rows[i].Cells[2].ColumnIndex == 2)
                {
                    dgv.Rows[i].Cells[2].Tag = dgv.Rows[i].Cells[2].Value.ToString();
                    if (dgv.Rows[i].Cells[2].Value.ToString() == "0")
                    {
                        dgv.Rows[i].Cells[2].Value = "替换";
                    }
                    else if (dgv.Rows[i].Cells[2].Value.ToString() == "1")
                    {
                        dgv.Rows[i].Cells[2].Value = "删除";
                    }
                    else if (dgv.Rows[i].Cells[2].Value.ToString() == "2")
                    {
                        dgv.Rows[i].Cells[2].Value = "新增";
                    }
                    else if (dgv.Rows[i].Cells[2].Value.ToString() == "3")
                    {
                        dgv.Rows[i].Cells[2].Value = "执行";
                    }
                    else if (dgv.Rows[i].Cells[2].Value.ToString() == "4")
                    {
                        dgv.Rows[i].Cells[2].Value = "重启覆盖";
                    }
                }
            }
        }

        private void dgv_Scroll(object sender, ScrollEventArgs e)
        {
            this.cmb_Temp.Visible = false;
        }

        //右键删除控件。
        private int index = 0;

        private void dgv_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.dgv.Rows[e.RowIndex].Selected = true;//是否选中当前行
                index = e.RowIndex;


                //每次选中行都刷新到datagridview中的活动单元格
                this.cms.Show(this.dgv, e.Location);
                //指定控件（DataGridView），指定位置（鼠标指定位置）
                this.cms.Show(Cursor.Position);//锁定右键列表出现的位置

            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.dgv.Rows[index].IsNewRow)//判断是否为新行
            {
                this.dgv.Rows.RemoveAt(index);//从集合中移除指定的行
            }
        }

        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
            e.RowBounds.Location.Y,
            dgv.RowHeadersWidth - 4,
            e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics,
                  (e.RowIndex + 1).ToString(),
                   dgv.RowHeadersDefaultCellStyle.Font,
                   rectangle,
                   dgv.RowHeadersDefaultCellStyle.ForeColor,
                   TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void create_Click(object sender, EventArgs e)
        {
            
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    String routeBuf = fileDialog.FileName;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(routeBuf);
                    XmlNode root = xmlDoc.DocumentElement;

                    dtData.Clear();
                    DataRow drData;
                    int i;
                    i = 0;
                    //BindData();
                    foreach (XmlNode xmlNode in root.ChildNodes)
                    {
                        i = 0;
                        drData = dtData.NewRow();
                        foreach (XmlNode xmlElement in xmlNode.ChildNodes)
                        {
                            drData[i]=xmlElement.InnerText;
                            i++;
                        }
                        dtData.Rows.Add(drData);
                    }
                    dgv.DataSource = dtData;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (4 == e.ColumnIndex)
            {
                //初始化一个OpenFileDialog类
                OpenFileDialog fileDialog = new OpenFileDialog();

                //判断用户是否正确的选择了文件
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    //获取用户选择文件路径和文件名。
                    String route = fileDialog.FileName;
                    System.IO.FileInfo file = new System.IO.FileInfo(fileDialog.FileName);
                    String length = ((file.Length) / 1024 + 1).ToString();//大小",
                    String name = file.Name;
                    //修改
                    if (e.RowIndex < dgv.RowCount - 1)
                    {
                        dgv.Rows[e.RowIndex].Cells[0].Value = name;
                        dgv.Rows[e.RowIndex].Cells[1].Value = route;
                        dgv.Rows[e.RowIndex].Cells[2].Value = "1";
                        dgv.Rows[e.RowIndex].Cells[3].Value = length + "KB";
                    }
                    //插入
                    else
                    {
                        DataRow drData;
                        drData = dtData.NewRow();
                        drData[1] = route;
                        drData[0] = name;
                        drData[2] = "1";
                        drData[3] = length + "KB";
                        dtData.Rows.Add(drData);
                        dtData.AcceptChanges();
                        dgv.Rows.RemoveAt(e.RowIndex + 1);
                    }

                }
            }
        }

        private void 打开ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void 生成版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(versionNum.Text.ToString()) || (versionNum.Text.ToString())[0] < 48 || (versionNum.Text.ToString()[0]) > 59)
            {
                MessageBox.Show("请输入正确的版本号");
            }
            else if (1 == dgv.RowCount)
            {
                MessageBox.Show("请添加至少一个文件");
            }
            else
            {
                FolderBrowserDialog fDialog = new FolderBrowserDialog();
                fDialog.Description = "请选择版本保存路径";
              
                if (fDialog.ShowDialog() == DialogResult.OK)
                {
                    string destPath = fDialog.SelectedPath;

                    if (File.Exists(destPath+@"\files.xml"))
                    {
                        File.Create(destPath + @"\files.xml");
                    }
                    try
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "Books", null);
                        XmlAttribute xmlAttri = xmlDoc.CreateAttribute("versionNum");
                        xmlAttri.InnerText = versionNum.Text.ToString();
                        root.Attributes.Append(xmlAttri);
                        xmlDoc.AppendChild(root);
                       for (int i = 0; i < this.dgv.RowCount-1; i++)
                        {
                            XmlNode xmlElement = xmlDoc.CreateNode(XmlNodeType.Element, "file", null);
                            XmlAttribute xmlAttribute = xmlDoc.CreateAttribute("id");
                            xmlAttribute.InnerText = i+1.ToString();
                            xmlElement.Attributes.Append(xmlAttribute);
                            XmlNode xmlItemName = xmlDoc.CreateNode(XmlNodeType.Element, "name", null);
                            xmlItemName.InnerText = dgv.Rows[i].Cells[0].Value.ToString();
                            XmlNode xmlItemRoute = xmlDoc.CreateNode(XmlNodeType.Element, "route", null);
                            xmlItemRoute.InnerText = dgv.Rows[i].Cells[1].Value.ToString();
                            XmlNode xmlItemStyle = xmlDoc.CreateNode(XmlNodeType.Element, "upStyle", null);
                            xmlItemStyle.InnerText = dgv.Rows[i].Cells[2].Value.ToString();
                            XmlNode xmlItemSize = xmlDoc.CreateNode(XmlNodeType.Element, "size", null);
                            xmlItemSize.InnerText = dgv.Rows[i].Cells[3].Value.ToString();

                            root.AppendChild(xmlElement);
                            xmlElement.AppendChild(xmlItemName);
                            xmlElement.AppendChild(xmlItemRoute);
                            xmlElement.AppendChild(xmlItemStyle);
                            xmlElement.AppendChild(xmlItemSize);
                        }

                        XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                        xmlDoc.InsertBefore(declaration, xmlDoc.DocumentElement);
                        xmlDoc.Save(destPath + @"\files.xml");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        string filepath = dtData.Rows[i]["文件路径"].ToString();
                        if (filepath != "")
                        {
                            string filename = filepath.Substring(filepath.LastIndexOf("\\"));
                            System.IO.File.Copy(filepath, destPath + filename,true);
                        }
                    }
                    MessageBox.Show("已生成新版本");
                }
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void create_Click_1(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;

            //默认情况下，如MessageBox.Show("确定要退出吗？")只显示一个“确定”按钮。
            DialogResult dr = MessageBox.Show("是否保存files.xml已修改内容?", "保存", messButton);

            if (dr == DialogResult.OK)//如果点击“确定”按钮

            {

                dtData.Clear();
                dgv.DataSource = dtData;
            }

            else//如果点击“取消”按钮

            {



            }
        }

        private void versionNum_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
