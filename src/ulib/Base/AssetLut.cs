using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulib
{
    public class Asset
    {
        public string Name;
        public string Path;
        public string MD5;  // the MD5 digest of file content 
    }

    public class AssetLut
    {
        // this should be a relative path of current layout file
        public string AssetRoot { get; set; }

        public List<Asset> Assets { get { return m_assets; } }
        private List<Asset> m_assets = new List<Asset>();
    }
}
