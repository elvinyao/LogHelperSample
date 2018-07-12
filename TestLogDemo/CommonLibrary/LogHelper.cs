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
        public static readonly string AppLogsPath = "App_Logs";
        public static readonly string LogFileAppender = "LogFileAppender";
        public static readonly string InfoLogging = "InfoLogging";

        public static void LogWriter(string methodName, string logInfo, string logPath = "")
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}", methodName);
            sb.AppendLine();
            sb.AppendFormat("{0}", logInfo);
            ChangeLogFileName(LogFileAppender, logPath);
            var logInfoNew = LogManager.GetLogger(InfoLogging);
            logInfoNew.Info(sb.ToString());
        }

        public static void LogWriterFolder(string methodName, string logInfo, string folderName, string logPath = "")
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}", methodName);
            sb.AppendLine();
            sb.AppendFormat("{0}", logInfo);
            ChangeLogFileName(LogFileAppender,
                string.Format(@"{0}\{1}", logPath, folderName));
            var logInfoNew = LogManager.GetLogger(InfoLogging);
            logInfoNew.Info(sb.ToString());
        }

        private static void ChangeLogFileName(string appenderNameStr, string newFileNameStr)
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

                        var updatedFileNameStr = Path.Combine(rootDomainPath, newFileNameStr, processId);
                        fApp.File = updatedFileNameStr;
                    }

                    fApp.ActivateOptions();
                }
            }
        }

        //private static bool ChangeLogFileName(string appenderNameStr, string newFileNameStr)
        //{
        //    if (string.IsNullOrEmpty(newFileNameStr))
        //    {
        //        return true;
        //    }

        //    log4net.Repository.ILoggerRepository rootRep = LogManager.GetRepository();


        //    foreach (IAppender iApp in rootRep.GetAppenders())
        //    {
        //        string appenderName = iApp.Name;
        //        if (appenderName.CompareTo(appenderNameStr) == 0
        //            && iApp is FileAppender)
        //        {
        //            FileAppender fApp = (FileAppender)iApp;

        //            var directory = Path.GetDirectoryName(fApp.File);
        //            var fileName = Path.GetFileName(fApp.File);
        //            var lastFAppDirectoryName = directory.Split(Path.DirectorySeparatorChar).Last();
        //            var fAppDirectoryName = directory.Split(Path.DirectorySeparatorChar).ToList();
        //            fAppDirectoryName.RemoveAt(fAppDirectoryName.Count - 1);
        //            var secondlastFAppDirectoryName = fAppDirectoryName.Last();
        //            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(directory))
        //            {
        //                var processId = fileName.Split('-').First();
        //                string updatedFileNameStr;
        //                if (newFileNameStr.Contains(@"\"))
        //                {
        //                    var firstPart = newFileNameStr.Split(Path.DirectorySeparatorChar).First();
        //                    var secondPart = newFileNameStr.Split(Path.DirectorySeparatorChar).Last();
        //                    if (firstPart == lastFAppDirectoryName)
        //                    {
        //                        if (secondPart == secondlastFAppDirectoryName)
        //                        {
        //                            var trimNewFileNameStr = newFileNameStr.Split('\\').Last();
        //                            updatedFileNameStr = Path.Combine(directory, trimNewFileNameStr, processId);
        //                            fApp.File = updatedFileNameStr;
        //                        }

        //                    }
        //                    else
        //                    {
        //                        updatedFileNameStr = Path.Combine(directory, newFileNameStr, processId);
        //                        fApp.File = updatedFileNameStr;
        //                    }
        //                }
        //                else
        //                {
        //                    updatedFileNameStr = Path.Combine(directory, newFileNameStr, processId);
        //                    fApp.File = updatedFileNameStr;
        //                }
        //            }

        //            fApp.ActivateOptions();
        //            return true; // 找到对应Appender，修改FileName
        //        }
        //    }

        //    return false; // 没有找到Appender
        //}
    }
}