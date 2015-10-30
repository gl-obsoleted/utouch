using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace ucore
{
    public delegate void MessageBoxHandler(string title, string message);
    public delegate bool ConfirmBoxHandler(string title, string message);

    public delegate void LoggingHandler(string format, params object[] args);
    public delegate void LoggingExceptionHandler(Exception e, string exMsg);

    public class Log
    {
        public static event LoggingHandler Receivers;
        public static event LoggingExceptionHandler ExceptionReceivers;

        public static void Printf(string format, params object[] args)
        {
            LoggingHandler h = Receivers;
            if (h != null)
                h(format, args);
        }

        public static void PrintException(Exception e, string exMsg)
        {
            LoggingExceptionHandler h = ExceptionReceivers;
            if (h != null)
                h(e, exMsg);
        }

        public static void DbgPrintf(string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(format, args));
        }
    }

    public class Logging : IDisposable
    {
        public static Logging Instance;

        private TextWriter LogFile;
        private MessageBoxHandler MsgBoxHandler;
        private ConfirmBoxHandler ConfirmHandler;

        public bool Init(string logFilePath, MessageBoxHandler mbh, ConfirmBoxHandler ch)
        {
            try
            {
                LogFile = new StreamWriter(logFilePath, false, Encoding.UTF8);
            }
            catch (Exception)
            {
                return false;
            }

            MsgBoxHandler = mbh;
            ConfirmHandler = ch;
            ucore.Log.Receivers += Log;
            ucore.Log.ExceptionReceivers += LogExceptionDetail;
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (LogFile != null)
                    LogFile.Close();
            }
        }

        public void FlushLog()
        {
            if (LogFile != null)
                LogFile.Flush();
        }

        public void Log(string format, params object[] args)
        {
            string fulltime = DateTime.Now.ToString("HH-mm-ss ");
            string content = fulltime + string.Format(format, args);
            if (LogFile != null)
                LogFile.WriteLine(content);
            System.Diagnostics.Debug.WriteLine(content);
        }

        public void LogException(Exception e, string additionalInfo)
        {
            Interlocked.Increment(ref ExceptionCounter);

            Log("===== Exception #{0} Begin =====", ExceptionCounter);
            Log(e.GetType().Name);
            Log(e.Message);
            if (!string.IsNullOrEmpty(additionalInfo))
            {
                Log("  Additional Info:");
                Log("    {0} ", additionalInfo);
            }
            Log("===== Exception #{0} End   =====", ExceptionCounter);
            FlushLog();
        }

        public void LogExceptionDetail(Exception e)
        {
            LogExceptionDetail(e, "");
        }

        public void LogExceptionDetail(Exception e, string exMsg)
        {
            Interlocked.Increment(ref ExceptionCounter);

            Log("===== Exception #{0} Begin =====", ExceptionCounter);
            Log(e.GetType().Name);
            Log(e.Message);
            Log(e.StackTrace);

            if (!string.IsNullOrEmpty(exMsg))
            {
                Log("  Additional Info:");
                Log(exMsg);
            }

            Log("===== Exception #{0} End   =====", ExceptionCounter);
        }

        public string GetLogFilePath()
        {
            if (LogFile == null)
                return "";

            StreamWriter sw = LogFile as StreamWriter;
            if (sw == null)
                return "";

            FileStream fs = sw.BaseStream as FileStream;
            if (fs == null)
                return "";

            return fs.Name;
        }

        public void Message(string format, params object[] args)
        {
            string msg = string.Format(format, args);
            Log("=MESSAGE SHOWN=: ", msg);
            if (MsgBoxHandler != null)
                MsgBoxHandler(ConstDefault.MessageBoxTitle, msg);
        }

        public bool Confirm(string format, params object[] args)
        {
            string msg = string.Format(format, args);
            Log("=MESSAGE SHOWN=: ", msg);
            return ConfirmHandler != null && ConfirmHandler(ConstDefault.MessageBoxTitle, msg);
        }

        static int ExceptionCounter = 0;
    }

}
