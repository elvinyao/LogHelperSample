using System.Diagnostics;
using System.IO;
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
            ChangeFilePath(LogFileAppender, PathCombine(logPath));
            ILog logInfoNew = LogManager.GetLogger(InfoLogging);
            logInfoNew.InfoFormat(LogMessageCombine(methodName, logInfo));
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
            ChangeFilePath(LogFileAppender, PathCombine(logPath, folderName));
            ILog logInfoNew = LogManager.GetLogger(InfoLogging);
            logInfoNew.InfoFormat(LogMessageCombine(methodName, logInfo));
        }

        private static void ChangeFilePath(string appenderNameStr, string newFileNameStr)
        {
            log4net.Repository.ILoggerRepository rootRep = LogManager.GetRepository();
            foreach (IAppender iApp in rootRep.GetAppenders())
            {
                string appenderName = iApp.Name;
                if (appenderName.CompareTo(appenderNameStr) == 0
                    && iApp is FileAppender)
                {
                    FileAppender fApp = (FileAppender) iApp;
                    fApp.File = newFileNameStr;
                    fApp.ActivateOptions();
                    // 找到对应Appender，修改FileName
                }
            }
            log4net.GlobalContext.Properties["pid"] = Process.GetCurrentProcess().Id;

            // 没有找到Appender
        }

        /// <summary>
        /// 方便日志路径，自动补全"\\"反斜杠
        /// </summary>
        /// <param name="pathOne"></param>
        /// <param name="pathTwo"></param>
        /// <returns></returns>
        private static string PathCombine(string pathOne, string pathTwo = "")
        {
            if (pathOne == "")
            {
                return AppLogsPath + "\\";
            }

            var returnPathValue = Path.Combine(AppLogsPath, pathOne, pathTwo);

            return returnPathValue + "\\";
        }

        /// <summary>
        /// 日志消息生成
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        private static string LogMessageCombine(string methodName, string logInfo)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}", methodName);
            sb.AppendLine();
            sb.AppendFormat("{0}", logInfo);
            return sb.ToString();
        }
    }
}