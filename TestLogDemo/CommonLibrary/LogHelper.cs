using log4net;
using log4net.Appender;

namespace CommonLibrary
{
    public class LogHelper
    {
        public static void LogWriter(string methodName, string logInfor, string logPath = "")
        {
            ChangeLogFileName("ErrorAppender", PathGenerator(logPath));
            ILog logInfoNew = LogManager.GetLogger("logerror");
            logInfoNew.InfoFormat("方法是：{0},消息是：{1}", methodName, logInfor);
        }

        public static void LogWriterFolder(string methodName, string logInfor, string folderName, string logPath = "")
        {
            ChangeLogFileName("ErrorAppender",
                string.Format(@"{0}{1}", PathGenerator(logPath), PathGenerator(folderName)));
            ILog logInfoNew = LogManager.GetLogger("logerror");
            logInfoNew.InfoFormat("方法是：{0},消息是：{1}", methodName, logInfor);
        }

        private static bool ChangeLogFileName(string appenderNameStr, string newFileNameStr)
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
                    return true; // 找到对应Appender，修改FileName
                }
            }

            return false; // 没有找到Appender
        }

        /// <summary>
        /// 方便日志路径，自动补全"\\"反斜杠
        /// </summary>
        /// <param name="pathStr"></param>
        /// <returns></returns>
        private static string PathGenerator(string pathStr)
        {
            if (string.IsNullOrEmpty(pathStr) || string.IsNullOrWhiteSpace(pathStr))
            {
                return "DefaultLog\\";
            }

            return pathStr + "\\";
        }
    }
}