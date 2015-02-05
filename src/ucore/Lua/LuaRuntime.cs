using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ucore
{
    public class LuaRuntime
    {
        public static LuaRuntime Instance;

        public bool Init(String bootstrapFilename)
        {
            m_bootstrap = new Script();

            DynValue ret;
            if (!RunScript(bootstrapFilename, out ret) || !ret.IsNil())
            {
                Logging.Printf("Lua 引导脚本执行异常（返回非 nil 值）.");
                return false;
            }

            return true;
        }

        public static bool RunScript(string scriptFileName)
        {
            DynValue ret;
            return RunScript(scriptFileName, out ret);
        }

        public static bool RunScript(string scriptFileName, out DynValue ret)
        {
            try
            {
                ret = Instance.BootstrapScript.DoFile(scriptFileName);
                return true;
            }
            catch (MoonSharp.Interpreter.InterpreterException e)
            {
                Logging.PrintException(e, e.DecoratedMessage);

                ret = DynValue.Nil;
                return false;
            }
        }

        public static string GetGlobalString(string varName)
        {
            string varValue = Instance.BootstrapScript.Globals[varName] as string;
            if (varValue == null)
                Logging.Printf("Error: failed to get global var '{0}' from lua.", varName);
            return varValue;
        }

        public static List<string> GetGlobalStringArray(string varName)
        {
            List<string> ret = new List<string>();
            MoonSharp.Interpreter.Table t = Instance.BootstrapScript.Globals[varName] as MoonSharp.Interpreter.Table;
            foreach (var res in t.Values)
            {
                ret.Add(res.CastToString());
            }

            return ret;
        }

        public Script BootstrapScript { get { return m_bootstrap; } }

        private Script m_bootstrap;
    }
}
