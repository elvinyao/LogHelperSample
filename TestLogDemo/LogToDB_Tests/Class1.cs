using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogToDB;
using NUnit.Framework;

namespace LogToDB_Tests
{
    [TestFixture]
    public class TestForLog4NetHelper
    {
        private static readonly string AppLogsPath = "Logs";

        [SetUp]
        protected void Setup()
        {
        }

        private string lastbutOneFAppDirectoryName;
        private string lastFAppDirectoryName;
        private string lastbutTwoFAppDirectoryName;
        private string lastbutThreeFAppDirectoryName;

        [Test]
        public void testhaha()
        {
            LogHelper.LogWriter("aa", "asdasdasdasd");
        }
    }
}