using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ucore;
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

            ucore.UCoreStartParam p = new ucore.UCoreStartParam();
            p.SessionFolder = sessionFolder;
            p.LogFile = Properties.Settings.Default.LogFilename;
            p.LuaBootstrapFile = Properties.Settings.Default.LuaBootstrap;
            p.MsgBoxHandler = (title, msg) => { MessageBox.Show(msg, title); };
            p.ConfirmHandler = (title, msg) => { return MessageBox.Show(msg, title, MessageBoxButtons.OKCancel) == DialogResult.OK; };

            string errMsg;
            if (!ucore.UCoreStart.Init(p, out errMsg))
            {
                MessageBox.Show(string.Format("ucore init failed. \n\n  Detail: {0} \n\n  按 'OK' 退出程序。", errMsg));
                return false;
            }

            UserPreference.Instance.Init(Path.Combine(Properties.Settings.Default.TempFolderName, Properties.Settings.Default.UserPrefFile));

            Logging.Instance.Log("Log started. '{0}'", Logging.Instance.GetLogFilePath());
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
                UserPreference.Instance.Save();
                ucore.UCoreStart.Destroy();
            }
        }

        public string RootPath { get { return m_rootPath; } }

        private string m_rootPath;
    }
}
