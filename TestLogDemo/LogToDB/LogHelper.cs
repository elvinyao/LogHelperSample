using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace LogToDB
{
    public class LogHelper
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
            var logInfoNew = LogManager.GetLogger(InfoLogging);
            logInfoNew.Info(sb.ToString());
        }
    }
}
