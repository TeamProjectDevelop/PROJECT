using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApplication2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2.Tests
{
    [TestClass()]
    public class DetectTests
    {
        [TestMethod()]
        public void GetUpdateTest()
        {
            Detect Det = new Detect();
            Det.getOnlineXml("D:\\softwaredesigningfiles\\C#\\PROJECT\\网站");
            Det.getLocalXml("D:\\softwaredesigningfiles\\C#\\PROJECT\\QQPCmgr\\Documents\\Visual Studio 2015\\Projects\\WindowsFormsApplication2\\bin\\Debug");
            Det.getXmlName("files.xml");
            Assert.AreEqual(Det.GetUpdate(),2);
        }
    }
}