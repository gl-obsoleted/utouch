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

        public bool InitAssetRoot()
        {
            if (Directory.Exists(Properties.Settings.Default.Proj_AssetRoot))
            {
                Logging.Instance.Log("AssetRoot 有效 ('{0}').", Properties.Settings.Default.Proj_AssetRoot);
                return true;
            }

            // would keep asking for valid asset root to proceed
            while (true)
            {
                FolderBrowserDialog Fld = new FolderBrowserDialog();
                Fld.Description = "请选择资源目录 (Asset Root)：";
                Fld.SelectedPath = m_rootPath;
                Fld.ShowNewFolderButton = true;
                if (Fld.ShowDialog() == DialogResult.OK && Directory.Exists(Fld.SelectedPath))
                {
                    Properties.Settings.Default.Proj_AssetRoot = Fld.SelectedPath;
                    Properties.Settings.Default.Save();
                    break;
                }
                else
                {
                    string msg = "未选择资源目录，或选择的目录无效。\n选择 Retry 重试，选择 Cancel 退出程序。";
                    if (MessageBox.Show(msg, "utouch", MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                        return false;
                }
            }

            Logging.Instance.Log("AssetRoot 设置为 ('{0}').", Properties.Settings.Default.Proj_AssetRoot);
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
