using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib.Base;

namespace udesign
{
    public class LuaHelpers
    {
        public static ResolutionV2 GetDefaultResolution()
        {
            MoonSharp.Interpreter.Table t = LuaRuntime.Instance.BootstrapScript.Globals["Resolutions"] as MoonSharp.Interpreter.Table;
            foreach (var res in t.Values)
            {
                if (res.Type == MoonSharp.Interpreter.DataType.Table && Convert.ToBoolean(res.Table["default"]))
                {
                    ResolutionV2 resolution = new ResolutionV2();
                    resolution.width = Convert.ToInt32(res.Table["w"]);
                    resolution.height = Convert.ToInt32(res.Table["h"]);
                    resolution.category = Convert.ToInt32(res.Table["cat"]);
                    resolution.tag = (string)res.Table["tag"];
                    return resolution;
                }
            }

            return null;
        }
    }
}
