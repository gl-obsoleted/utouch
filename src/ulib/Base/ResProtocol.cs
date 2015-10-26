﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulib
{
    public class ResProtocol
    {
        public const string ProtocolPrefix = "uires://";
        public const string ResIndexFilePostfix = ".txt";
        public const string ResImageFilePostfix = ".png";
        public const string ResDescFilePostfix = "_desc.txt";
        public const char ResDelimeter = ':';

        public static string ComposeURL(string atlasFileName, string atlasTileName)
        {
            return string.Format("{0}{1}:{2}", ResProtocol.ProtocolPrefix, atlasFileName, atlasTileName);
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
