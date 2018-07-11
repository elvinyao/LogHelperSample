using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommonLibrary;
using log4net.Appender;
using NUnit.Framework;
using Shouldly;

namespace Log4NetHelper_Tests
{
    [TestFixture]
    public class TestForLog4NetHelper
    {
        private string secondlastFAppDirectoryName;
        private string lastFAppDirectoryName;

        [SetUp]
        protected void Setup()
        {
        }

        [Test]
        public void CheckOne()
        {
            LogHelper.LogWriter("aa", "info1");
            InitialQueryFileAppender();

            lastFAppDirectoryName.ShouldBe("App_Logs");
            secondlastFAppDirectoryName.ShouldBe("Debug");
        }

        [Test]
        public void CheckTwo()
        {
            LogHelper.LogWriter("aa", "info1", "path1");

            InitialQueryFileAppender();

            lastFAppDirectoryName.ShouldBe("path1");
            secondlastFAppDirectoryName.ShouldBe("App_Logs");
        }

        [Test]
        public void CheckThree()
        {
            LogHelper.LogWriterFolder("aa", "info1", "folder1", "path1");

            InitialQueryFileAppender();

            lastFAppDirectoryName.ShouldBe("folder1");
            secondlastFAppDirectoryName.ShouldBe("path1");
        }

        private void InitialQueryFileAppender()
        {
            log4net.LogManager.GetCurrentLoggers().Length.ShouldBe(1);

            var appender = log4net.LogManager.GetRepository().GetAppenders();
            appender.Length.ShouldBe(1);

            var fApp = (FileAppender) appender.First();


            var directory = Path.GetDirectoryName(fApp.File);
            var fileName = Path.GetFileName(fApp.File);
            lastFAppDirectoryName = directory.Split(Path.DirectorySeparatorChar).Last();
            var fAppDirectoryName = directory.Split(Path.DirectorySeparatorChar).ToList();
            fAppDirectoryName.RemoveAt(fAppDirectoryName.Count - 1);
            secondlastFAppDirectoryName = fAppDirectoryName.Last();
        }
    }
}