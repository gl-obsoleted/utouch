using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ucore
{
    public class BaseUtil
    {
        public static bool Implements(Type sourceClass, Type targetInterface)
        {
            if (!sourceClass.IsClass || !targetInterface.IsInterface)
                return false;

            return sourceClass.GetInterface(targetInterface.FullName) != null;
        }

        public static string PointToString(Point pt)
        {
            return string.Format("{0},{1}", pt.X, pt.Y);
        }

        public static string SizeToString(Size pt)
        {
            return string.Format("{0},{1}", pt.Width, pt.Height);
        }

        public static Point StringToPoint(string ptStr)
        {
            string[] coords = ptStr.Split(',');
            return new Point(int.Parse(coords[0]), int.Parse(coords[1]));
        }

        public static Size StringToSize(string szStr)
        {
            string[] coords = szStr.Split(',');
            return new Size(int.Parse(coords[0]), int.Parse(coords[1]));
        }
    }
}
