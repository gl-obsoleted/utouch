using System;
using System.Collections.Generic;
using System.IO;
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

    public class ResFormUtil
    {
        static public DevComponents.AdvTree.Node CreateNode(DirectoryInfo dir)
        {
            DevComponents.AdvTree.Node node = new DevComponents.AdvTree.Node();
            node.Tag = dir;
            node.Text = string.Format("{0} ({1})", dir.Name, dir.GetFiles().Length);
            node.Image = Properties.Resources.FolderClosed;
            node.ImageExpanded = Properties.Resources.FolderOpen;
            node.ExpandVisibility = dir.GetDirectories().Length != 0 ? DevComponents.AdvTree.eNodeExpandVisibility.Visible : DevComponents.AdvTree.eNodeExpandVisibility.Hidden;
            return node;
        }
    }
}
