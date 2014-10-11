using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ulib.Base;

namespace ulib
{
    public delegate void OutputHandler(string msg);

    public class Session
    {
        public static string SessionFolder;
        public static TextWriter LogFile;

        public static event OutputHandler OutputLog;

        public static bool Init(string sessionFolder, string logFilename)
        {
            if (!Directory.Exists(sessionFolder))
                return false;

            try
            {
                LogFile = new StreamWriter(Path.Combine(sessionFolder, logFilename), false, Encoding.UTF8);
                SessionFolder = sessionFolder;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static void Deinit()
        {
            OutputLog = null;
            LogFile.Close();
        }

        public static void Log(string format, params object[] args) 
        {
            string fulltime = DateTime.Now.ToString("HH-mm-ss ");
            string content = fulltime + string.Format(format, args);
            if (LogFile != null)
                LogFile.WriteLine(content);
            System.Diagnostics.Debug.WriteLine(content);

            if (OutputLog != null)
            {
                OutputLog(content);
            }
        }

        public static void LogException(Exception e, string additionalInfo)
        {
            Interlocked.Increment(ref ExceptionCounter);

            Log("===== Exception #{0} Begin =====", ExceptionCounter);
            Log(e.GetType().Name);
            Log(e.Message);
            Log("===== Exception #{0} End   =====", ExceptionCounter);
        }

        public static void LogExceptionDetail(Exception e)
        {
            Interlocked.Increment(ref ExceptionCounter);

            Log("===== Exception #{0} Begin =====", ExceptionCounter);
            Log(e.GetType().Name);
            Log(e.Message);
            Log(e.StackTrace);
            Log("===== Exception #{0} End   =====", ExceptionCounter);
        }

        public static string GetLogFilePath()
        {
            if (LogFile == null)
                return "";

            StreamWriter sw = Session.LogFile as StreamWriter;
            if (sw == null)
                return "";

            FileStream fs = sw.BaseStream as FileStream;
            if (fs == null)
                return "";

            return fs.Name;
        }

        public static DialogResult Message(string format, params object[] args)
        {
            string msg = string.Format(format, args);
            Log("=MESSAGE SHOWN=: ", msg);
            return MessageBox.Show(msg, Constants.LibName);
        }

        public static bool Confirm(string format, params object[] args)
        {
            string msg = string.Format(format, args);
            Log("=MESSAGE SHOWN=: ", msg);
            return MessageBox.Show(msg, Constants.LibName, MessageBoxButtons.OKCancel) == DialogResult.OK;
        }

        static int ExceptionCounter = 0;
    }
}
