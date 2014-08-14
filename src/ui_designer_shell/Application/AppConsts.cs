using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Base;

namespace ui_designer
{
    public class AppConsts
    {
        public static readonly string TempFolderName = "temp";
        public static string UserPrefFilePath = Path.Combine(AppConsts.TempFolderName, "user_pref.json");

        public const string ConfigFileDefault = "cfg_default.json";
        public const string ConfigFileUser = "cfg_user.json";

        public static readonly string UILayoutTree_RootName = RootNodeConstants.Default_Name;
        public static readonly string LogFilename = "ui_designer.log";
    }
}
