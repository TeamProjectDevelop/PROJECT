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
        public MainForm()
        {
            InitializeComponent();
            InitConfi();

        }

        private string url = "";
        private List<Form1> forms = new List<Form1>();


        private void InitConfi()
        {

            //获取保存在工作目录下的网址。
            StreamReader sr = new StreamReader(Application.StartupPath + "\\" + "WebSite.txt", Encoding.Default);
            String line = sr.ReadLine();
            this.url = line;
            sr.Close();
            //把最近打开文件列表加载进来
            AddRecentFile("");
        }

        private void 检测更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.url == "")
            {
                MessageBox.Show("更新路径还没有设置，请先设置更新路径");
                return;
            }
            else
            {
                Detectupdate detectupdate = new Detectupdate(this.url);
                detectupdate.Show();
            }
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

        private void 生成版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidateFormat())
            {

                FolderBrowserDialog fDialog = new FolderBrowserDialog();
                fDialog.Description = "请选择版本保存路径";
                int index = this.tabControl1.SelectedIndex;//当前的页面的索引
                if (fDialog.ShowDialog() == DialogResult.OK)
                {
                    string destPath = fDialog.SelectedPath;

                    //forms[index]修改
                    if (File.Exists(destPath + @"\" + forms[index].fname.Text.ToString() + ".xml"))
                    {
                        File.Create(destPath + @"\" + forms[index].fname.Text.ToString() + ".xml");
                    }
                    try
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "Books", null);
                        XmlAttribute xmlAttri = xmlDoc.CreateAttribute("versionNum");
                        xmlAttri.InnerText = forms[index].versionNum.Text.ToString();
                        root.Attributes.Append(xmlAttri);
                        xmlDoc.AppendChild(root);
                        for (int i = 0; i < forms[index].dgv.RowCount - 1; i++)
                        {
                            XmlNode xmlElement = xmlDoc.CreateNode(XmlNodeType.Element, "file", null);
                            XmlAttribute xmlAttribute = xmlDoc.CreateAttribute("id");
                            xmlAttribute.InnerText = i + 1.ToString();
                            xmlElement.Attributes.Append(xmlAttribute);
                            XmlNode xmlItemName = xmlDoc.CreateNode(XmlNodeType.Element, "name", null);
                            xmlItemName.InnerText = forms[index].dgv.Rows[i].Cells[1].Value.ToString();
                            XmlNode xmlItemRoute = xmlDoc.CreateNode(XmlNodeType.Element, "route", null);
                            xmlItemRoute.InnerText = forms[index].dgv.Rows[i].Cells[2].Value.ToString();
                            XmlNode xmlItemStyle = xmlDoc.CreateNode(XmlNodeType.Element, "upStyle", null);
                            xmlItemStyle.InnerText = forms[index].dgv.Rows[i].Cells[3].Value.ToString();
                            XmlNode xmlItemSize = xmlDoc.CreateNode(XmlNodeType.Element, "size", null);
                            xmlItemSize.InnerText = forms[index].dgv.Rows[i].Cells[4].Value.ToString();

                            root.AppendChild(xmlElement);
                            xmlElement.AppendChild(xmlItemName);
                            xmlElement.AppendChild(xmlItemRoute);
                            xmlElement.AppendChild(xmlItemStyle);
                            xmlElement.AppendChild(xmlItemSize);
                        }

                        XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                        xmlDoc.InsertBefore(declaration, xmlDoc.DocumentElement);
                        xmlDoc.Save(destPath + @"\" + forms[index].fname.Text.ToString() + ".xml");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    for (int i = 0; i < forms[index].getDtData().Rows.Count; i++)
                    {
                        string filepath = forms[index].getDtData().Rows[i]["文件路径"].ToString();
                        if (filepath != "")
                        {
                            string filename = filepath.Substring(filepath.LastIndexOf("\\"));
                            System.IO.File.Copy(filepath, destPath + filename, true);
                        }
                    }
                    MessageBox.Show("已生成新版本");
                }
            }
        }

        private void create_Click_1(object sender, EventArgs e)
        {
            TabPage tab = new TabPage();
            tab.Name = "bomo";
            tab.Text = "未命名";
            Form1 form = new Form1();
            form.newfileflag = true;
            this.tabControl1.SelectedTab = tab;
            forms.Add(form);

            form.TopLevel = false;     //设置为非顶级控件
            tab.Controls.Add(form);
            tabControl1.TabPages.Add(tab);
            form.Show();
        }

        private void 浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                String filepath = fileDialog.FileName;
                //检测是否已经打开
                foreach (Form1 f in forms)
                {
                    if (filepath == f.curr_filepath)
                        return;
                }
                TabPage tab = new TabPage();
                tab.Name = "bomo";
                tab.Text = System.IO.Path.GetFileNameWithoutExtension(fileDialog.FileName);//文件名没有扩展名
                Form1 form = new Form1(filepath);
                form.newfileflag = false;
                this.tabControl1.SelectedTab = tab;
                forms.Add(form);
                AddRecentFile(filepath); //添加到最近打开列表

                form.TopLevel = false;     //设置为非顶级控件
                tab.Controls.Add(form);
                tabControl1.TabPages.Add(tab);
                form.Show();
                AddRecentFile(filepath);
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidateFormat())
            {
                FolderBrowserDialog fDialog = new FolderBrowserDialog();
                fDialog.Description = "请选择配置文件保存路径";

                int index = this.tabControl1.SelectedIndex;

                if (fDialog.ShowDialog() == DialogResult.OK)
                {
                    string destPath = fDialog.SelectedPath;

                    forms[index].curr_filepath = destPath + @"\" + forms[index].fname.Text.ToString() + ".xml"; //保存当前打开文件完整路径

                    if (File.Exists(destPath + @"\" + forms[index].fname.Text.ToString() + ".xml"))
                    {
                        File.Create(destPath + @"\" + forms[index].fname.Text.ToString() + ".xml");
                    }
                    try
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "Books", null);
                        XmlAttribute xmlAttri = xmlDoc.CreateAttribute("versionNum");
                        xmlAttri.InnerText = forms[index].versionNum.Text.ToString();
                        root.Attributes.Append(xmlAttri);
                        xmlDoc.AppendChild(root);
                        for (int i = 0; i < forms[index].dgv.RowCount - 1; i++)
                        {
                            XmlNode xmlElement = xmlDoc.CreateNode(XmlNodeType.Element, "file", null);
                            XmlAttribute xmlAttribute = xmlDoc.CreateAttribute("id");
                            xmlAttribute.InnerText = i + 1.ToString();
                            xmlElement.Attributes.Append(xmlAttribute);
                            XmlNode xmlItemName = xmlDoc.CreateNode(XmlNodeType.Element, "name", null);
                            xmlItemName.InnerText = forms[index].dgv.Rows[i].Cells[1].Value.ToString();
                            XmlNode xmlItemRoute = xmlDoc.CreateNode(XmlNodeType.Element, "route", null);
                            xmlItemRoute.InnerText = forms[index].dgv.Rows[i].Cells[2].Value.ToString();
                            XmlNode xmlItemStyle = xmlDoc.CreateNode(XmlNodeType.Element, "upStyle", null);
                            xmlItemStyle.InnerText = forms[index].dgv.Rows[i].Cells[3].Value.ToString();
                            XmlNode xmlItemSize = xmlDoc.CreateNode(XmlNodeType.Element, "size", null);
                            xmlItemSize.InnerText = forms[index].dgv.Rows[i].Cells[4].Value.ToString();

                            root.AppendChild(xmlElement);
                            xmlElement.AppendChild(xmlItemName);
                            xmlElement.AppendChild(xmlItemRoute);
                            xmlElement.AppendChild(xmlItemStyle);
                            xmlElement.AppendChild(xmlItemSize);
                        }

                        XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                        xmlDoc.InsertBefore(declaration, xmlDoc.DocumentElement);
                        xmlDoc.Save(destPath + @"\" + forms[index].fname.Text.ToString() + ".xml");
                        tabControl1.SelectedTab.Text = forms[index].fname.Text.ToString();
                        forms[index].updateflag = false;
                        forms[index].newfileflag = false;

                        AddRecentFile(destPath + @"\" + forms[index].fname.Text.ToString() + ".xml"); //添加到最近打开列表
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = this.tabControl1.SelectedIndex;
            if (forms[index].newfileflag)  //如果是新建
            {
                this.另存为ToolStripMenuItem_Click(sender, e);
            }
            else
            {
                if (ValidateFormat())
                {
                    try
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "Books", null);
                        XmlAttribute xmlAttri = xmlDoc.CreateAttribute("versionNum");

                        xmlAttri.InnerText = forms[index].versionNum.Text.ToString();
                        root.Attributes.Append(xmlAttri);
                        xmlDoc.AppendChild(root);
                        for (int i = 0; i < forms[index].dgv.RowCount - 1; i++)
                        {
                            XmlNode xmlElement = xmlDoc.CreateNode(XmlNodeType.Element, "file", null);
                            XmlAttribute xmlAttribute = xmlDoc.CreateAttribute("id");
                            xmlAttribute.InnerText = i + 1.ToString();
                            xmlElement.Attributes.Append(xmlAttribute);
                            XmlNode xmlItemName = xmlDoc.CreateNode(XmlNodeType.Element, "name", null);
                            xmlItemName.InnerText = forms[index].dgv.Rows[i].Cells[1].Value.ToString();
                            XmlNode xmlItemRoute = xmlDoc.CreateNode(XmlNodeType.Element, "route", null);
                            xmlItemRoute.InnerText = forms[index].dgv.Rows[i].Cells[2].Value.ToString();
                            XmlNode xmlItemStyle = xmlDoc.CreateNode(XmlNodeType.Element, "upStyle", null);
                            xmlItemStyle.InnerText = forms[index].dgv.Rows[i].Cells[3].Value.ToString();
                            XmlNode xmlItemSize = xmlDoc.CreateNode(XmlNodeType.Element, "size", null);
                            xmlItemSize.InnerText = forms[index].dgv.Rows[i].Cells[4].Value.ToString();

                            root.AppendChild(xmlElement);
                            xmlElement.AppendChild(xmlItemName);
                            xmlElement.AppendChild(xmlItemRoute);
                            xmlElement.AppendChild(xmlItemStyle);
                            xmlElement.AppendChild(xmlItemSize);
                        }

                        XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                        xmlDoc.InsertBefore(declaration, xmlDoc.DocumentElement);
                        xmlDoc.Save(forms[index].curr_filepath);
                        tabControl1.SelectedTab.Text = forms[index].fname.Text.ToString();

                        AddRecentFile(forms[index].curr_filepath); //添加到最近打开列表
                        forms[index].newfileflag = false;
                        forms[index].updateflag = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private bool ValidateFormat()
        {
            int index = this.tabControl1.SelectedIndex; //当前页面的索引
            if (String.IsNullOrEmpty(forms[index].versionNum.Text.ToString()) || (forms[index].versionNum.Text.ToString())[0] < 48 || (forms[index].versionNum.Text.ToString()[0]) > 59)
            {
                MessageBox.Show("请输入正确的版本号");
                return false;
            }
            else if (1 == forms[index].dgv.RowCount)
            {
                MessageBox.Show("请添加至少一个文件");
                return false;
            }
            else if (string.IsNullOrEmpty(forms[index].fname.Text.ToString()))
            {
                MessageBox.Show("文件名不合法");
                return false;
            }
            else
            {
                for (int i = 0; i < forms[index].dgv.RowCount - 1; i++)
                {
                    if (string.IsNullOrEmpty(forms[index].dgv.Rows[i].Cells[3].Value.ToString()))
                    {
                        MessageBox.Show("有的文件更新方式未选择");
                        return false;
                    }
                }
            }
            return true;
        }

        //画X号
        private void tabControl1_DrawItem_1(object sender, DrawItemEventArgs e)
        {
            try
            {
                Rectangle myTabRect = this.tabControl1.GetTabRect(e.Index);

                //先添加TabPage属性     
                e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text
                , this.Font, SystemBrushes.ControlText, myTabRect.X + 2, myTabRect.Y + 2);

                //再画一个矩形框  
                using (Pen p = new Pen(Color.White))
                {
                    myTabRect.Offset(myTabRect.Width - (10 + 3), 2);
                    myTabRect.Width = 10;
                    myTabRect.Height = 10;
                    e.Graphics.DrawRectangle(p, myTabRect);

                }

                //填充矩形框  
                Color recColor = e.State == DrawItemState.Selected ? Color.White : Color.White;
                using (Brush b = new SolidBrush(recColor))
                {
                    e.Graphics.FillRectangle(b, myTabRect);
                }

                //画关闭符号  
                using (Pen objpen = new Pen(Color.Black))
                {
                    //"\"线  
                    Point p1 = new Point(myTabRect.X + 3, myTabRect.Y + 3);
                    Point p2 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + myTabRect.Height - 3);
                    e.Graphics.DrawLine(objpen, p1, p2);

                    //"/"线  
                    Point p3 = new Point(myTabRect.X + 3, myTabRect.Y + myTabRect.Height - 3);
                    Point p4 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + 3);
                    e.Graphics.DrawLine(objpen, p3, p4);
                }

                e.Graphics.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //点击X则关闭当前页面
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            int index = this.tabControl1.SelectedIndex;
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X, y = e.Y;

                //计算关闭区域     
                Rectangle myTabRect = this.tabControl1.GetTabRect(this.tabControl1.SelectedIndex);

                myTabRect.Offset(myTabRect.Width - (10 + 3), 2);
                myTabRect.Width = 10;
                myTabRect.Height = 10;

                //如果鼠标在区域内就关闭选项卡     
                bool isClose = x > myTabRect.X && x < myTabRect.Right
                 && y > myTabRect.Y && y < myTabRect.Bottom;
                //关闭前先检测保存
                if (isClose == true)
                {
                    if (forms[index].updateflag)
                    {
                        DialogResult dr = MessageBox.Show("该配置文件有修改是否保存", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            if (!ValidateFormat())
                                return;
                            this.保存ToolStripMenuItem_Click(sender, e);
                        }
                    }
                    //关闭
                    forms.RemoveAt(index);
                    this.tabControl1.TabPages.Remove(this.tabControl1.SelectedTab);

                }
            }

        }
    }
}
