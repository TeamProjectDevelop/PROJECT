using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace WindowsFormsApplication2
{
    public class Update
    {
        private string LocalUrl = "";
        private string OnlineUrl = "";
        private string UpdateFileName = "";
        private string UpdateExeName = "";
        private int upcount = 0;
        private Dictionary<string, string> ReStartCoverage = new Dictionary<string, string>();

        public void SetLocalUrl(string LocalUrl)
        {
            this.LocalUrl = LocalUrl;
        }

        public void SetOnlineUrl(string OnlineUrl)
        {
            this.OnlineUrl = OnlineUrl;
        }

        public void SetUpdateFileName(string FileName)
        {
            this.UpdateFileName = FileName;
        }

        public void SetUpdateExeName(string ExeName)
        {
            this.UpdateExeName = ExeName;
        }
        public bool Readxml()
        {
            string OnlineDirPath = this.OnlineUrl;
            string LocalDirPath = this.LocalUrl;
            string updatefilename = this.UpdateFileName;
            try
            {
                String routeBuf = OnlineDirPath + "\\" + updatefilename;
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
                    UpdateFile(filename, filepath, fileupstyle, OnlineDirPath, LocalDirPath);
                }
                File.Copy(routeBuf, LocalDirPath + "\\" + updatefilename, true);
                if (ReStartCoverage.Count == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void UpdateFile(string filename, string filepath, string fileupstyle, string OnlinePath, string LocalDirPath)
        {
            if (fileupstyle == "新增")
            {
                File.Copy(Path.Combine(OnlinePath, filename), Path.Combine(LocalDirPath, filename));
                upcount++;
            }
            else if (fileupstyle == "替换")
            {
                File.Copy(Path.Combine(OnlinePath, filename), Path.Combine(LocalDirPath, filename), true);
                upcount++;
            }
            else if (fileupstyle == "删除")
            {
                File.Delete(Path.Combine(LocalDirPath, filename));
                upcount++;
            }
            else if (fileupstyle == "重启覆盖")
            {
                ReStartCoverage.Add(filename, fileupstyle);
            }
            else if (fileupstyle == "执行")
            {
                ReStartCoverage.Add(filename, fileupstyle);
            }
        }

        public void RestartUpdate()
        {
            if (ReStartCoverage.Count != 0)
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    string upexepath = this.LocalUrl + "\\ReStartUpdate.exe";
                    startInfo.FileName = upexepath;
                    startInfo.Arguments = this.OnlineUrl+"\\"+this.UpdateFileName + " " + this.UpdateExeName;
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
