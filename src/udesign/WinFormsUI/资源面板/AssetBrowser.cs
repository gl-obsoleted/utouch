using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.AdvTree;
using System.IO;
using ucore;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;

namespace udesign
{
    public partial class AssetBrowser : UserControl
    {
        public AssetBrowser()
        {
            InitializeComponent();

            m_metroTilePanel.Items.Clear();
        }

        public string AssetRoot { get { return m_assetRoot; } set { m_assetRoot = value; } }
        private string m_assetRoot;

        public string SelectedAsset { get { return m_selectedAsset; } }
        private string m_selectedAsset;
        private MetroTileItem m_selectedTile;

        public event SysPost.StdMulticastDelegation AssetSelected;

        public void ClearSelectedAsset()
        {
            m_selectedAsset = "";

            if (m_selectedTile != null)
            {
                m_selectedTile.Checked = false;
                m_selectedTile = null;
            }
        }

        private void AssetBrowser_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_assetRoot))
                return;

            m_assetFolderTree.BeginUpdate();
            m_assetFolderTree.Nodes.Clear();

            Node root = ResFormUtil.CreateNode(new DirectoryInfo(m_assetRoot));
            m_assetFolderTree.Nodes.Add(root);

            // load all subnodes recursively manually
            LoadDirectories(root, (DirectoryInfo)root.Tag, true);

            m_assetFolderTree.EndUpdate();

            // load last opened dir 
            string prevPath = Properties.Settings.Default.RecentAccessedAssetDir;
            if (!string.IsNullOrEmpty(prevPath) && Directory.Exists(prevPath))
            {
                DirectoryInfo prevDI = new DirectoryInfo(prevPath);
                TryExpandAndSelectNode(m_assetFolderTree.Nodes[0], prevPath);
            }
        }

        private bool TryExpandAndSelectNode(Node n, string path)
        {
            string nodePath = ((DirectoryInfo)n.Tag).FullName;
            if (path.IndexOf(nodePath) == 0)
            {
                if (((DirectoryInfo)n.Tag).GetDirectories().Length != 0)
                {
                    n.Expand();
                    foreach (Node child in n.Nodes)
                    {
                        if (TryExpandAndSelectNode(child, path))
                            return true;
                    }
                }
                else
                {
                    if (path == nodePath)
                    {
                        m_assetFolderTree.SelectedNode = n;
                        return true;
                    }
                }
            }

            return false;
        }

        private void m_assetFolderTree_BeforeExpand(object sender, AdvTreeNodeCancelEventArgs e)
        {
            Node parent = e.Node;
            if (parent.Nodes.Count > 0) 
                return;

            if (parent.Tag is DirectoryInfo)
            {
                LoadDirectories(parent, (DirectoryInfo)parent.Tag, false);
            }
        }

        private void LoadDirectories(Node parent, DirectoryInfo directoryInfo, bool recursively)
        {
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            foreach (DirectoryInfo dir in directories)
            {
                if ((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) 
                    continue;

                Node node = ResFormUtil.CreateNode(dir);
                parent.Nodes.Add(node);

                if (recursively)
                {
                    LoadDirectories(node, dir, recursively);
                }
            }
        }

        private void m_assetFolderTree_SelectionChanged(object sender, EventArgs e)
        {
            m_metroTilePanel.Items.Clear();

            Node node = m_assetFolderTree.SelectedNode;
            DirectoryInfo dir = node.Tag as DirectoryInfo;
            if (dir == null)
                return;

            ItemContainer ic = new ItemContainer();
            ic.MultiLine = true;
            ic.TitleText = dir.FullName;
            ic.Name = dir.Name;
            ic.Tag = dir;

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                MetroTileItem tile = new MetroTileItem(file.Name, file.Name);
                tile.TitleText = file.Name;
                tile.Tag = file.FullName;
                tile.Image = ImageUtil.ScaleImageAsClamped(Image.FromFile(file.FullName), tile.TileSize);
                tile.Click += Tile_Clicked;
                tile.DoubleClick += Tile_DoubleClicked;
                tile.ContainerControl = ic;
                ic.SubItems.Add(tile);
            }
            m_metroTilePanel.Items.Add(ic);
            m_metroTilePanel.Refresh();

            Properties.Settings.Default.RecentAccessedAssetDir = dir.FullName;
            Properties.Settings.Default.Save();
            Logging.Instance.Log("RecentAccessedAssetDir refreshed: {0}", dir.FullName);
        }

        private void Tile_Clicked(object sender, EventArgs e)
        {
            MetroTileItem ti = sender as MetroTileItem;
            if (ti == null)
                return;

            ti.Checked = true;
            if (m_selectedTile != null)
                m_selectedTile.Checked = false;

            m_selectedTile = ti;
            m_selectedAsset = SysUtil.GetRelativePath((string)ti.Tag, m_assetRoot);
            SysPost.InvokeMulticast(this, AssetSelected);
        }

        private void Tile_DoubleClicked(object sender, EventArgs e)
        {
            Tile_Clicked(sender, e);

        }
    }
}
