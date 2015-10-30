using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ucore
{
    public class SysUtil
    {
        // 归一化系统路径
        public static string NormalizePath(string path)
        {
            return new Uri(path).LocalPath;
        }

        // 判断两个绝对路径是否在同一个分区（是否可以用相对路径表示）
        public static bool InTheSameDrive(string path1, string path2)
        {
            if (!Path.IsPathRooted(path1))
                return false;

            if (!Path.IsPathRooted(path2))
                return false;

            return Path.GetPathRoot(path1) == Path.GetPathRoot(path2);
        }

        // 获取 filespec 相对于 folder 的相对路径
        public static string GetRelativePath(string filespec, string folder)
        {
            Uri pathUri = new Uri(filespec);
            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folder += Path.DirectorySeparatorChar;
            }
            Uri folderUri = new Uri(folder);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }
    }
}
