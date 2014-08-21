using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ulib
{
    public class Session
    {
        public static string SessionFolder;
        public static TextWriter LogFile;

        public static bool Init(string sessionFolder, string logFilename)
        {
            if (!Directory.Exists(sessionFolder))
                return false;

            try
            {
                LogFile = new StreamWriter(Path.Combine(sessionFolder, logFilename));
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
            LogFile.Close();
        }

        public static void Log(string format, params object[] args) 
        {
            if (LogFile == null)
                return;

            string content = string.Format(format, args);
            string fulltime = DateTime.Now.ToString("HH-mm-ss ");
            Session.LogFile.WriteLine(fulltime + content);
            System.Diagnostics.Debug.WriteLine(fulltime + content);
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
            return MessageBox.Show(msg);
        }

        static int ExceptionCounter = 0;
    }
}
