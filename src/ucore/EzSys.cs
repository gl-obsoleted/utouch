using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ucore
{
    public class EzSys
    {
        public static string NormalizePath(string path)
        {
            return new Uri(path).LocalPath;
        }
    }
}
