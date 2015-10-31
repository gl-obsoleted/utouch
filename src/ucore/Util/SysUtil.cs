using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ucore
{
    public class SysUtil
    {
        // 归一化系统路径 (需要传入绝对路径)
        public static string NormalizePath(string path)
        {
            return new Uri(path).LocalPath;
        }

        // 去掉两端的分隔符 - 头尾的 '/' 和 '\\'
        public static string TrimHeadTailSeparators(string path)
        {
            return path.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        // 把路径中的 '\\' 转为 '/'
        public static string ToUnixPath(string path)
        {
            return path.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        // 把路径中的 '/' 转为 '\\'
        public static string ToWindowsPath(string path)
        {
            return path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
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

        public static byte[] GetFileMD5(string filepath)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filepath))
                    {
                        return md5.ComputeHash(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Instance.Log("GetFileMD5() failed. ({0}) ({1})", filepath, ex.Message);
                return null;
            }
        }

        public static string GetFileMD5AsString(string filepath)
        {
            byte[] md5 = GetFileMD5(filepath);
            if (md5 == null)
                return "";

            try
            {
                return BitConverter.ToString(md5).Replace("-", "").ToLower();
            }
            catch (Exception ex)
            {
                Logging.Instance.Log("GetFileMD5AsString() failed. ({0}) ({1})", filepath, ex.Message);
                return "";
            }
        }
    }
}
