using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ui_lib.Base;

namespace ui_lib
{
    public class ArchiveConstants
    {
        public static string[] FilePostfixes = new string[] {
            ".json",
        };
    }

    public class ArchiveUtil
    {
        public static bool RegisterCreator(ArchiveType type, Type t)
        {
            if (!BaseUtil.Implements(t, typeof(IArchive)))
                return false;

            // 这个类型已注册，如果一定要注册请先注销老的
            if (s_creatorRegistry.ContainsKey(type))
                return false;

            s_creatorRegistry[type] = t;
            return true;
        }

        public static IArchive CreateArchive(ArchiveType type)
        {
            Type t;
            if (!s_creatorRegistry.TryGetValue(type, out t))
                return null;

            IArchive arc = (IArchive)Activator.CreateInstance(t);
            return arc;
        }

        public static ArchiveType FindCompatibleArchiveType(string targetLocation)
        {
            for (int i = 0; i < ArchiveConstants.FilePostfixes.Length; ++i)
            {
                string postfix = ArchiveConstants.FilePostfixes[i];
                if (targetLocation.EndsWith(postfix, StringComparison.OrdinalIgnoreCase))
                {
                    if (i >= 0 && i < (int)ArchiveType.Num)
                    {
                        return (ArchiveType)i;

                    }
                }
            }

            return ArchiveType.None;
        }

        private static Dictionary<ArchiveType, Type> s_creatorRegistry = new Dictionary<ArchiveType,Type>();
    }
}
