using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace udesign
{
    public class AssetApplyingArgs : EventArgs
    {
        public AssetApplyingArgs(string asset)
        {
            m_selectedAsset = asset;
        }

        public string m_selectedAsset;
    }

}
