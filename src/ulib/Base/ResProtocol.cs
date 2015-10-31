using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ucore;
 

namespace ulib
{
    public class ResProtocol
    {
        public const string ProtocolPrefix = "uires://";
        public const string ResIndexFilePostfix = ".txt";
        public const string ResImageFilePostfix = ".png";
        public const string ResDescFilePostfix = "_desc.txt";
        public const char ResDelimeter = ':';

        public const string TileSingleTextureMarker = "<tex>";

        public static bool IsSingleTexture(string url)
        {
            if (!url.StartsWith(ResProtocol.ProtocolPrefix))
                return false;

            return url.EndsWith(ResDelimeter + TileSingleTextureMarker);
        }

        public static string ParseSingleTexture(string url)
        {
            if (!IsSingleTexture(url))
                return "";

            string[] parts = url.Substring(ResProtocol.ProtocolPrefix.Length).Split(ResProtocol.ResDelimeter);
            if (parts.Length != 2)
                return "";

            return parts[0];
        }

        public static string GetSingleTextureAssetName(string url)
        {
            string p = ParseSingleTexture(url);
            if (string.IsNullOrEmpty(p))
                return "";

            return p;
        }

        public static string ComposeURL(string atlasFileName, string atlasTileName)
        {
            return string.Format("{0}{1}:{2}", ResProtocol.ProtocolPrefix, atlasFileName, atlasTileName);
        }

        public static string ComposeSingleTextureURL(string singleTexturePath)
        {
            //      '\\abc\\foo\\' 
            //      会被转为
            //      'abc/foo'
            string unixStyle = SysUtil.ToUnixPath(SysUtil.TrimHeadTailSeparators(singleTexturePath));
            return ComposeURL(unixStyle, ResProtocol.TileSingleTextureMarker);
        }

        public static bool ParseURL(string url, out string filePath, out string tileName)
        {
            filePath = "";
            tileName = "";
            if (!url.StartsWith(ResProtocol.ProtocolPrefix))
                return false;

            string[] parts = url.Substring(ResProtocol.ProtocolPrefix.Length).Split(ResProtocol.ResDelimeter);
            if (parts.Length != 2)
                return false;

            filePath = parts[0];
            tileName = parts[1];
            return true;
        }
    }
}
