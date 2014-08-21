using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using udesign;
using ulib;

namespace udesign
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
            string sessionFolder = Path.Combine(Properties.Settings.Default.TempFolderName, date, fulltime);
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

            using (Session.LogFile = new StreamWriter(Path.Combine(Session.SessionFolder, Properties.Settings.Default.LogFilename)))
            {
                Session.Log("Log started. '{0}'", Session.GetLogFilePath());

                if (!ConfigTypical.Instance.LoadTypical())
                {
                    MessageBox.Show(string.Format("配置文件初始化失败。 \n\n按 'OK' 退出程序。"));
                    return;
                }

                ConfigUserPref.Instance.Init();

                TypeRegistry.Init();

                try
                {
                    MainForm mainForm = new MainForm();
                    if (!mainForm.Init())
                    {
                        MessageBox.Show(string.Format("主界面初始化失败。 \n\n按 'OK' 退出程序。"));
                        return;
                    }

                    Application.Run(mainForm);
                }
                catch (Exception e)
                {
                    Session.LogExceptionDetail(e);
                    MessageBox.Show(string.Format("程序遇到了未预料的异常。\n\n{0} - {1}\n\n细节请查看 log 文件 '{2}'，按 'OK' 退出程序。", e.GetType().Name, e.Message, Session.GetLogFilePath()));
                }

                ConfigUserPref.Instance.Save();
            }
        }
    }
}
