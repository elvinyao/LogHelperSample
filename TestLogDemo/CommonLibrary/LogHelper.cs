using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using log4net;
using log4net.Appender;

namespace CommonLibrary
{
    public static class LogHelper
    {
        private static readonly string AppLogsPath = "Logs";
        private static readonly string LogFileAppender = "LogFileAppender";
        private static readonly string InfoLogging = "InfoLogging";

        /// <summary>
        /// 日志记录方法A，包含logPath
        /// </summary>
        /// <param name="methodName">日志消息头</param>
        /// <param name="logInfo">日志消息正文</param>
        /// <param name="logPath">路径</param>
        public static void LogWriter(string methodName, string logInfo, string logPath = "")
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}", methodName);
            sb.AppendLine();
            sb.AppendFormat("{0}", logInfo);
            ChangeFilePath(LogFileAppender, PathCombineForPath(logPath));
            var logInfoNew = LogManager.GetLogger(InfoLogging);
            logInfoNew.Info(sb.ToString());
        }

        /// <summary>
        /// 日志记录方法B，包含logPath和子文件夹FolderName
        /// </summary>
        /// <param name="methodName">日志消息头</param>
        /// <param name="logInfo">日志消息正文</param>
        /// <param name="folderName">文件夹名</param>
        /// <param name="logPath">日志路径</param>
        public static void LogWriterFolder(string methodName, string logInfo, string folderName, string logPath = "")
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}", methodName);
            sb.AppendLine();
            sb.AppendFormat("{0}", logInfo);
            ChangeFilePath(LogFileAppender, PathCombineForFolderAndPath(folderName, logPath));
            var logInfoNew = LogManager.GetLogger(InfoLogging);
            logInfoNew.Info(sb.ToString());
        }

        private static void ChangeFilePath(string appenderNameStr, string pathAndFolderName)
        {
            var rootRep = LogManager.GetRepository();

            foreach (var iApp in rootRep.GetAppenders())
            {
                var appenderName = iApp.Name;
                if (String.Compare(appenderName, appenderNameStr, StringComparison.CurrentCulture) == 0 &&
                    iApp is FileAppender)
                {
                    var fApp = (FileAppender) iApp;

                    var appDomainPath = AppDomain.CurrentDomain.BaseDirectory;
                    var rootDomainPath = Path.Combine(appDomainPath, AppLogsPath);

                    var directory = Path.GetDirectoryName(fApp.File);
                    var fileName = Path.GetFileName(fApp.File);

                    if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(directory))
                    {
                        var processId = fileName.Split('-').First();

                        var updatedFileNameStr = Path.Combine(rootDomainPath, pathAndFolderName, processId);
                        fApp.File = updatedFileNameStr;
                    }

                    fApp.ActivateOptions();
                }
            }

            GlobalContext.Properties["pid"] = Process.GetCurrentProcess().Id;
        }

        /// <summary>
        /// 方便日志路径，自动补全"\\"反斜杠
        /// </summary>
        /// <param name="pathName"></param>
        /// <returns></returns>
        private static string PathCombineForPath(string pathName = "")
        {
            if (string.IsNullOrEmpty(pathName))
            {
                return "";
            }

            var returnPathValue = Path.Combine(pathName);

            return returnPathValue + "\\";
        }

        /// <summary>
        /// 方便日志路径，自动补全"\\"反斜杠
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="pathName"></param>
        /// <returns></returns>
        private static string PathCombineForFolderAndPath(string folderName = "", string pathName = "")
        {
            var returnPathValue = Path.Combine(pathName, folderName);

            return returnPathValue + "\\";
        }
    }

    public class CustomFileAppender : RollingFileAppender
    {
        private bool isFirstTime = true;

        protected override void OpenFile(string fileName, bool append)
        {
            if (isFirstTime)
            {
                isFirstTime = false;
                return;
            }

            base.OpenFile(fileName, append);
        }
    }
}