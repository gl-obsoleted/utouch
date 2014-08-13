using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_lib.Base
{
    public class ResUtil
    {
        public static bool ExtractTextureInfo(string url, out string filePath, out string tileName)
        {
            filePath = "";
            tileName = "";
            if (!url.StartsWith(Constants.ResourceProtocol))
                return false;

            string[] parts = url.Substring(Constants.ResourceProtocol.Length).Split(Constants.ResourceDelimeter);
            if (parts.Length != 2)
                return false;

            filePath = parts[0];
            tileName = parts[1];
            return true;            
        }
    }
}
