using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ulib.Base
{
    public class Math
    {
        public static int Clamp(int x, int min, int max)
        {
            return x < min ? min : (x > max ? max : x);
        }

        public static Point Clamp(Point pt, Rectangle rect)
        {
            return new Point(
                Clamp(pt.X, rect.Left, rect.Right),
                Clamp(pt.Y, rect.Top, rect.Bottom));
        }
    }

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

        public static string ComposeResURL(string atlasFileName, string atlasTileName)
        {
            return string.Format("{0}{1}:{2}", Constants.ResProtocol, atlasFileName, atlasTileName);
        }
    }

    public class JsonUtil
    {
        public static JObject LoadJObject(string filepath)
        {
            try
            {
                // read JSON directly from a file
                using (StreamReader file = File.OpenText(filepath))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    return (JObject)JToken.ReadFrom(reader);
                }
            }
            catch (Exception e)
            {
                Session.LogExceptionDetail(e);
                return null;
            }
        }
    }

    public class ResUtil
    {
        public static bool ExtractTextureInfo(string url, out string filePath, out string tileName)
        {
            filePath = "";
            tileName = "";
            if (!url.StartsWith(Constants.ResProtocol))
                return false;

            string[] parts = url.Substring(Constants.ResProtocol.Length).Split(Constants.ResDelimeter);
            if (parts.Length != 2)
                return false;

            filePath = parts[0];
            tileName = parts[1];
            return true;
        }
    }
}
