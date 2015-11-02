using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ucore;

namespace ulib
{
    public enum AssetType
    {
        Unknown,
        PNG,
    }

    public class AssetDesc
    {
        public string Name;
        public AssetType Type;
        public string Path;
        public string Digest;  // the digest of file content
        public DateTime LinkTime;
    }

    public class AssetUtil
    {
        public static AssetDesc CreateDesc(string assetPath)
        {
            string unixStyle = SysUtil.ToUnixPath(SysUtil.TrimHeadTailSeparators(assetPath));
            string filename = Path.GetFileNameWithoutExtension(unixStyle);
            string ext = Path.GetExtension(unixStyle);
            if (string.IsNullOrEmpty(filename))
	        {
                Logging.Instance.Log("CreateDesc() failed (invalid asset path): {0}.", assetPath);
                return null;		 
	        }

            string fullpath = Path.Combine(GState.AssetRoot, SysUtil.ToWindowsPath(unixStyle));
            if (!File.Exists(fullpath))
            {
                Logging.Instance.Log("CreateDesc() failed (file not found): {0}.", assetPath);
                return null;
            }

            string digest = SysUtil.GetFileMD5AsString(fullpath);
            if (string.IsNullOrEmpty(digest))
            {
                Logging.Instance.Log("CreateDesc() failed (md5 calculating failed): {0}.", fullpath);
                return null;
            }

            AssetType type = AssetType.Unknown;
            if (ext.ToLower() == "png")
            {
                type = AssetType.PNG;
            }

            AssetDesc desc = new AssetDesc();
            desc.LinkTime = DateTime.Now;
            desc.Name = filename + "_" + desc.LinkTime.ToString("yyyyMMdd_HHmmss");
            desc.Type = type;
            desc.Path = unixStyle;
            desc.Digest = digest;
            return desc;
        }

        public static Size GetImageSize(string assetPath)
        {
            string fullpath = Path.Combine(GState.AssetRoot, SysUtil.ToWindowsPath(assetPath));
            if (!File.Exists(fullpath))
            {
                Logging.Instance.Log("CreateDesc() failed (file not found): {0}.", assetPath);
                return Size.Empty;
            }

            try
            {
                return Image.FromFile(fullpath).Size;
            }
            catch (Exception)
            {
                return Const.ZERO_SIZE;
            }
        }
    }

    public class AssetLut
    {
        // this should be a relative path of current layout file
        public string AssetRoot { get; set; }

        public List<AssetDesc> Assets { get { return m_assets; } }
        private List<AssetDesc> m_assets = new List<AssetDesc>();

        public AssetDesc GetAssetDesc(string assetName)
        {
            foreach (var item in Assets)
            {
                if (item.Name == assetName)
                {
                    return item;
                }
            }

            return null;
        }

        public string AppendIfNotExist(AssetDesc desc)
        {
            foreach (var item in Assets)
            {
                if (item.Path == desc.Path)
                {
                    return item.Name;
                }
            }

            Assets.Add(desc);
            return desc.Name;            
        }
    }
}
