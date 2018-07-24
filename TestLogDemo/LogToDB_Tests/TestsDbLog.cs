using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogToDB;
using NUnit.Framework;

namespace LogToDB_Tests
{
    [TestFixture]
    public class TestsDbLog
    {
        private static readonly string AppLogsPath = "Logs";

        [SetUp]
        protected void Setup()
        {
        }


        [Test]
        public void FirstTestsOne()
        {
            LogHelper.LogWriter("aa", "asdasdasdasd1xx");
            LogHelper.LogWriter("aabb", "asdasdasdasd1xxx","aa");
            LogHelper.LogWriter("aabb", "asdasdasdasd1xxxx");
            LogHelper.LogWriter("aabb", "asdasdasdasd1xxxxx");
            LogHelper.LogWriterFolder("aabb", "asdasdasdasd1xxxxx","wenjj文件夹","asdpathlog");
        }
    }
}