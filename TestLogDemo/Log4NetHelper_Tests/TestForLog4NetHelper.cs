using System.IO;
using System.Linq;
using CommonLibrary;
using log4net;
using log4net.Appender;
using NUnit.Framework;
using Shouldly;

namespace Log4NetHelper_Tests
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

        private void InitialQueryFileAppender()
        {
            //LogManager.GetCurrentLoggers().Length.ShouldBe(1);

            var appender = LogManager.GetRepository().GetAppenders();
            appender.Length.ShouldBe(1);

            var fApp = (FileAppender) appender.First();
            fApp.ShouldNotBeNull();
            fApp.File.ShouldNotBeNull();

            var directory = Path.GetDirectoryName(fApp.File);
            var fileName = Path.GetFileName(fApp.File);
            directory.ShouldNotBeNull();
            fileName.ShouldNotBeNull();
            fileName.ShouldContain("[");
            fileName.ShouldContain("]");

            lastFAppDirectoryName = directory.Split(Path.DirectorySeparatorChar).Last();
            var fAppDirectoryName = directory.Split(Path.DirectorySeparatorChar).ToList();

            fAppDirectoryName.RemoveAt(fAppDirectoryName.Count - 1);
            lastbutOneFAppDirectoryName = fAppDirectoryName.Last();
        }

        [Test]
        public void CheckLogWriterFolder()
        {
            LogHelper.LogWriterFolder("aa", "info1", "folder1", "path1");

            InitialQueryFileAppender();

            lastFAppDirectoryName.ShouldBe("folder1");
            lastbutOneFAppDirectoryName.ShouldBe("path1");
        }


        [Test]
        public void CheckLogWriterFolderAlongWithLogWriter()
        {
            LogHelper.LogWriterFolder("aa", "info1", "folder1", "path1");
            InitialQueryFileAppender();

            lastFAppDirectoryName.ShouldBe("folder1");
            lastbutOneFAppDirectoryName.ShouldBe("path1");

            LogHelper.LogWriter("aa", "info1", "path1");

            InitialQueryFileAppender();

            lastFAppDirectoryName.ShouldBe("path1");
            lastbutOneFAppDirectoryName.ShouldBe(AppLogsPath);
        }

        [Test]
        public void CheckLogWriterFolderAlongWithLogWriterAnother()
        {
            LogHelper.LogWriterFolder("aa", "info1", "folder1", "path1");
            InitialQueryFileAppender();

            lastFAppDirectoryName.ShouldBe("folder1");
            lastbutOneFAppDirectoryName.ShouldBe("path1");

            LogHelper.LogWriterFolder("aa", "info1", "folder2", "path2");
            InitialQueryFileAppender();

            lastFAppDirectoryName.ShouldBe("folder2");
            lastbutOneFAppDirectoryName.ShouldBe("path2");

            LogHelper.LogWriter("aa", "info1", "path1");
            InitialQueryFileAppender();

            lastFAppDirectoryName.ShouldBe("path1");
            lastbutOneFAppDirectoryName.ShouldBe(AppLogsPath);

            LogHelper.LogWriter("aa", "info1");
            InitialQueryFileAppender();

            lastFAppDirectoryName.ShouldBe(AppLogsPath);
        }

        [Test]
        public void CheckLogWriterWithoutPath()
        {
            LogHelper.LogWriter("aa", "info1");
            InitialQueryFileAppender();

            lastFAppDirectoryName.ShouldBe(AppLogsPath);
            lastbutOneFAppDirectoryName.ShouldBe("Debug");
        }

        [Test]
        public void CheckLogWriterWithPath()
        {
            LogHelper.LogWriter("aa", "info1", "path1");

            InitialQueryFileAppender();

            lastFAppDirectoryName.ShouldBe("path1");
            lastbutOneFAppDirectoryName.ShouldBe(AppLogsPath);
        }
    }
}