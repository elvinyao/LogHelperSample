using System;
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
            ChangeFilePath(LogFileAppender, logPath);
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
            ChangeFilePath(LogFileAppender, string.Format(@"{0}\{1}", logPath, folderName));
            var logInfoNew = LogManager.GetLogger(InfoLogging);
            logInfoNew.Info(sb.ToString());
        }

        private static void ChangeFilePath(string appenderNameStr, string pathAndFolderName)
        {
            var rootRep = LogManager.GetRepository();

            foreach (var iApp in rootRep.GetAppenders())
            {
                var appenderName = iApp.Name;
                if (appenderName.CompareTo(appenderNameStr) == 0 && iApp is FileAppender)
                {
                    var fApp = (FileAppender) iApp;

                    var appDomainPath = AppDomain.CurrentDomain.BaseDirectory;
                    var rootDomainPath = Path.Combine(appDomainPath, AppLogsPath);

                    var directory = Path.GetDirectoryName(fApp.File);
                    var fileName = Path.GetFileName(fApp.File);

                    var lastFAppDirectoryName = directory.Split(Path.DirectorySeparatorChar).Last();
                    var fAppDirectoryName = directory.Split(Path.DirectorySeparatorChar).ToList();
                    fAppDirectoryName.RemoveAt(fAppDirectoryName.Count - 1);
                    var secondlastFAppDirectoryName = fAppDirectoryName.Last();

                    if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(directory))
                    {
                        var processId = fileName.Split('-').First();

                        var updatedFileNameStr = Path.Combine(rootDomainPath, pathAndFolderName, processId);
                        fApp.File = updatedFileNameStr;
                    }

                    fApp.ActivateOptions();
                }
            }
        }
        
    }
}