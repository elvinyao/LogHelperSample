﻿using System.Text;
using log4net;

namespace LogToDB
{
    public class LogHelper
    {
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
            sb.AppendFormat("{0}", logInfo);

            var logInfoNew = LogManager.GetLogger(InfoLogging);
            LogicalThreadContext.Properties["methodname"] = methodName ?? "";
            LogicalThreadContext.Properties["pathname"] = logPath ?? "";
            LogicalThreadContext.Properties["foldername"] = "";
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
            sb.AppendFormat("{0}", logInfo);

            var logInfoNew = LogManager.GetLogger(InfoLogging);
            LogicalThreadContext.Properties["methodname"] = methodName ?? "";
            LogicalThreadContext.Properties["pathname"] = logPath ?? "";
            LogicalThreadContext.Properties["foldername"] = folderName ?? "";
            logInfoNew.Info(sb.ToString());
        }
    }
}