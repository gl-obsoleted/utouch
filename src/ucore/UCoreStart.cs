using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ucore
{
    public class UCoreStartParam
    {
        public string SessionFolder;
        public string LogFile;
        public string LuaBootstrapFile;

        // handlers
        public MessageBoxHandler MsgBoxHandler;
        public ConfirmBoxHandler ConfirmHandler;
    }

    public class UCoreStart
    {
        public static UCoreStart Instance;

        public static bool Init(UCoreStartParam param, out string errMsg)
        {
            Instance = new UCoreStart();

            if (!Directory.Exists(param.SessionFolder))
            {
                errMsg = string.Format("session directory ('{0}') not found.", param.SessionFolder);
                return false;
            }

            string logFilePath = Path.Combine(param.SessionFolder, param.LogFile);
            Logging.Instance = new Logging();
            if (!Logging.Instance.Init(logFilePath, param.MsgBoxHandler, param.ConfirmHandler))
            {
                errMsg = string.Format("logging init failed ('file={0}').", logFilePath);
                return false;
            }

            LuaRuntime.Instance = new LuaRuntime();
            if (!LuaRuntime.Instance.Init(param.LuaBootstrapFile))
            {
                errMsg = string.Format("lua init failed ('lua_bootstrap_file={0}').", param.LuaBootstrapFile);
                return false;
            }

            Instance.SessionFolder = param.SessionFolder;
            errMsg = "";
            return true;
        }

        public static void Destroy()
        {
            LuaRuntime.Instance = null;

            Logging.Instance.Dispose();
            Logging.Instance = null;
        }

        private string SessionFolder;
    }
}
