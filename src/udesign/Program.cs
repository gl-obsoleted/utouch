using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using udesign;
using ulib;
using ulib.Elements;

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
            using (UDesignApp app = new UDesignApp())
            {
                if (!app.InitEnv())
                    return;
                if (!app.InitSession())
                    return;

                using (MainForm mainForm = new MainForm())
                {
                    if (!mainForm.Init())
                    {
                        MessageBox.Show(string.Format("主界面初始化失败。 \n\n按 'OK' 退出程序。"));
                        return;
                    }

                    if (Properties.Settings.Default.BuildTestScene)
                        TestScene.Run();

                    // 正常的运行阶段
#if DEBUG
                    Application.Run(mainForm);
#else
                    try
                    {
                        Application.Run(mainForm);
                    }
                    catch (Exception e)
                    {
                        Session.LogExceptionDetail(e);
                        MessageBox.Show(string.Format("程序遇到了未预料的异常。\n\n{0} - {1}\n\n细节请查看 log 文件 '{2}'，按 'OK' 退出程序。", e.GetType().Name, e.Message, Session.GetLogFilePath()));
                    }
#endif
                }
            }
        }
    }
}
