using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApplication2
{
    public class Detect
    {
        private string LocalXml = "D:\\softwaredesigningfiles\\C#\\PROJECT\\QQPCmgr\\Documents\\Visual Studio 2015\\Projects\\WindowsFormsApplication2\\bin\\Debug";
        private string OnlineXml = "D:\\softwaredesigningfiles\\C#\\PROJECT\\网站";
        private string filename = "\\files.xml";
        
        public void getLocalXml(string xml)
        {
            this.LocalXml = xml;
        }

        public void getOnlineXml(string xml)
        {
            this.OnlineXml = xml;
        }

        public void getXmlName(string filename)
        {
            this.filename = "\\"+filename;
        }

        public int GetUpdate()
        {
            string LocalUrl = this.LocalXml;
            string OnlineUrl = this.OnlineXml;
            string Localxml = @LocalUrl + this.filename;
            string Updatexml = @OnlineUrl + this.filename;
            string Localxmlpath = LocalUrl + this.filename;
            string Updatexmlpath = OnlineUrl + this.filename;

            try
            {
                //计算第一个文件的哈希值
                var hash = System.Security.Cryptography.HashAlgorithm.Create();
                var stream_1 = new System.IO.FileStream(Localxml, System.IO.FileMode.Open);
                byte[] hashByte_1 = hash.ComputeHash(stream_1);
                stream_1.Close();
                //计算第二个文件的哈希值
                var stream_2 = new System.IO.FileStream(Updatexml, System.IO.FileMode.Open);
                byte[] hashByte_2 = hash.ComputeHash(stream_2);
                stream_2.Close();

                //比较两个哈希值
                if (BitConverter.ToString(hashByte_1) == BitConverter.ToString(hashByte_2))
                    return 1;
                else
                    return 2;
            }
            catch(Exception ex)
            {
                Console.WriteLine("错误类型：" + ex.GetType().ToString());
                return 3;
            }
        }
    }
}
