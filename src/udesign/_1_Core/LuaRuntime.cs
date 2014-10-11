using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib;

namespace udesign
{
    public class LuaRuntime
    {
        public static LuaRuntime Instance;

        public bool Init()
        {
            Script script = new Script();

            String bootstrapFilename = Properties.Settings.Default.LuaBootstrap;
            try
            {
                DynValue val = script.DoFile(bootstrapFilename);
                if (!val.IsNil())
                {
                    Session.Log("Lua 引导脚本执行异常（返回非 nil 值）.");
                    return false;
                }
            }
            catch (Exception e)
            {
                Session.LogException(e, bootstrapFilename);
                return false;
            }

            m_bootstrap = script;
            Table t = BootstrapScript.Globals["Resolutions"] as Table;
            foreach (var res in t.Values)
            {
                if (res.Type == DataType.Table)
                {
                    int w = Convert.ToInt32(res.Table["w"]);
                    int h = Convert.ToInt32(res.Table["h"]);
                    int cat = Convert.ToInt32(res.Table["cat"]);
                    string tag = (string)res.Table["tag"];
                }
            }

            return true;
        }

        public Script BootstrapScript { get { return m_bootstrap; } }

        private Script m_bootstrap;
    }
}
