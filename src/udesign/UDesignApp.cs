using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ulib;

namespace udesign
{
    public class UDesignApp : IDisposable
    {
        public static UDesignApp Instance;  // 注意这个实例的生命期在 Main() 中控制

        public bool InitEnv()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string startupFolder = Path.GetFileName(Application.StartupPath);
            if (!string.Equals(startupFolder, "bin", StringComparison.OrdinalIgnoreCase))
            {
                string exeName = Path.GetFileName(Application.ExecutablePath);
                MessageBox.Show(string.Format("可执行文件 '{0}' 应该在 'bin' 目录里。\n不正常的版本，按 'OK' 退出程序。", exeName));
                return false;
            }

            m_rootPath = Application.StartupPath.Substring(0, Application.StartupPath.Length - startupFolder.Length - 1);
            Directory.SetCurrentDirectory(m_rootPath);
            return true;
        }

        public bool InitSession()
        {
            DateTime dt = DateTime.Now;
            string date = dt.ToString("yyyy-MM-dd");
            string fulltime = date + dt.ToString("-HH-mm-ss");
            string sessionFolder = Path.Combine(Properties.Settings.Default.TempFolderName, date, fulltime);
            try
            {
                if (!Directory.Exists(sessionFolder))
                    Directory.CreateDirectory(sessionFolder);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("临时目录('{0}')初始化失败。 \n\n'{1}'\n\n按 'OK' 退出程序。", sessionFolder, e.Message));
                return false;
            }

            if (!Session.Init(sessionFolder, Properties.Settings.Default.LogFilename, Properties.Settings.Default.LuaBootstrap))
            {
                MessageBox.Show(string.Format("临时目录('{0}')初始化失败。 \n\n按 'OK' 退出程序。", sessionFolder));
                return false;
            }

            UserPreference.Instance.Init(Path.Combine(Properties.Settings.Default.TempFolderName, Properties.Settings.Default.UserPrefFile));

            Session.Log("Log started. '{0}'", Session.GetLogFilePath());
            return true;
        }

        public void Dispose()
        {
            UserPreference.Instance.Save();

            Session.Deinit();
        }

        public string RootPath { get { return m_rootPath; } }

        private string m_rootPath;
    }
}
