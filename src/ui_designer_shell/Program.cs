using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ui_designer;

namespace ui_designer_shell
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string startupFolder = Path.GetFileName(Application.StartupPath);
            if (!string.Equals(startupFolder, "bin", StringComparison.OrdinalIgnoreCase))
            {
                string exeName = Path.GetFileName(Application.ExecutablePath);
                MessageBox.Show(string.Format("可执行文件 '{0}' 应该在 'bin' 目录里。\n不正常的版本，按 'OK' 退出程序。", exeName));
                return;
            }

            string rootPath = Application.StartupPath.Substring(0, Application.StartupPath.Length - startupFolder.Length - 1);
            Directory.SetCurrentDirectory(rootPath);

            DateTime dt = DateTime.Now;
            string date = dt.ToString("yyyy-MM-dd");
            string fulltime = date + dt.ToString("-HH-mm-ss");
            string sessionFolder = Path.Combine(ShellConstants.TempFolderName, date, fulltime);
            try
            {
                if (!Directory.Exists(sessionFolder))
                    Directory.CreateDirectory(sessionFolder);

                Session.SessionFolder = sessionFolder;
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("临时目录('{0}')初始化失败。 \n\n'{1}'\n\n按 'OK' 退出程序。", sessionFolder, e.Message));
                return;
            }

            using (Session.LogFile = new StreamWriter(Path.Combine(Session.SessionFolder, ShellConstants.LogFilename)))
            {
                Session.Log("Log started. '{0}'", Session.GetLogFilePath());

                try
                {
                    Application.Run(new MainForm());
                }
                catch (Exception e)
                {
                    MessageBox.Show(string.Format("程序遇到了未预料的异常。\n\n{0}\n\n细节请查看 log 文件 '{1}'，按 'OK' 退出程序。", e.Message, Session.GetLogFilePath()));
                    return;
                }
            }
        }
    }
}
