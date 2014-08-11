using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_designer_shell
{
    class Session
    {
        public static string SessionFolder;

        public static TextWriter LogFile;

        public static void Log(string format, params object[] args) 
        {
            string content = string.Format(format, args);
            string fulltime = DateTime.Now.ToString("HH-mm-ss ");
            Session.LogFile.WriteLine(fulltime + content);
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
    }
}
