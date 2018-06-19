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
    public class UpdateTests
    {
        private Update Up = new Update();
        [TestMethod()]
        public void ReadxmlTest()
        {
            Up.SetOnlineUrl("D:\\softwaredesigningfiles\\C#\\PROJECT\\网站");
            Up.SetLocalUrl("D:\\softwaredesigningfiles\\C#\\PROJECT\\QQPCmgr\\Documents\\Visual Studio 2015\\Projects\\WindowsFormsApplication2Tests\\bin\\Debug");
            Up.SetUpdateFileName("files.xml");
            Up.SetUpdateExeName("WindowsFormsApplication2.exe");
            Assert.AreEqual(Up.Readxml(),true);
        }
        [TestMethod()]
        public void RestartUpdateTest()
        {
            Up.RestartUpdate();
        }
    }
}