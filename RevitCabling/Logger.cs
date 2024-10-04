using log4net;
using log4net.Appender;
using log4net.Config;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace RevitCabling
{
    public static class Logger
    {
        private static ILog mainlogger;

        public static void InitMainLogger(string name)
        {
            //string name = type.ToString();
            var repository = LogManager.CreateRepository(name);
            mainlogger = LogManager.GetLogger(name, typeof(Application));
            string LogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), 
                "RevitCablingPlugin", 
                "Version_" +
#if v22
                2022 +
#endif
#if v23
                2023 +
#endif
#if v24
                2024 +
#endif
                "_Log", "RevitCabling.log");
            RollingFileAppender LogFile = new RollingFileAppender();
            LogFile.File = LogFilePath;
            LogFile.MaxSizeRollBackups = 10;
            LogFile.RollingStyle = RollingFileAppender.RollingMode.Size;
            LogFile.DatePattern = "_dd-MM-yyyy";
            LogFile.MaximumFileSize = "10MB";
            LogFile.ActivateOptions();
            LogFile.AppendToFile = true;
            LogFile.Encoding = Encoding.UTF8;
            LogFile.Layout = new log4net.Layout.SimpleLayout();
            LogFile.ActivateOptions();
            BasicConfigurator.Configure(repository, LogFile);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            return sf.GetMethod().Name;
        }

        public static void Log(Exception ex)
        {
            mainlogger?.Error("Error", ex);
        }

        public static void Log(string text, Exception ex)
        {
            mainlogger?.Error(text, ex);
        }

        public static void Log(string text)
        {
            mainlogger?.Info(text);
        }

        public static void Error(string text)
        {
            mainlogger?.Error(text);
        }

        public static void Warn(string text)
        {
            mainlogger?.Warn(text);
        }

        public static void Warn(string text, Exception ex)
        {
            mainlogger?.Warn(text, ex);
        }

        public static void Info(string text)
        {
            mainlogger?.Info(text);
        }
    }
}
