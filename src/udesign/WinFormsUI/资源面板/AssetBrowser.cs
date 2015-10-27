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

        private void AssetBrowser_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_assetRoot))
                return;

            m_assetFolderTree.BeginUpdate();
            m_assetFolderTree.Nodes.Clear();
            DevComponents.AdvTree.Node node = new DevComponents.AdvTree.Node();
            node.Tag = new DirectoryInfo(m_assetRoot);
            node.Text = "Asset Root";
            node.Image = Properties.Resources.FolderClosed;
            m_assetFolderTree.Nodes.Add(node);
            // We will load drive content on demand
            node.ExpandVisibility = eNodeExpandVisibility.Visible;
            // Enable tree layout and display updates, performs any pending layout and display updates
            m_assetFolderTree.EndUpdate();
        }

        private void m_assetFolderTree_BeforeExpand(object sender, AdvTreeNodeCancelEventArgs e)
        {
            Node parent = e.Node;
            if (parent.Nodes.Count > 0) return;

            if (parent.Tag is DirectoryInfo)
            {
                LoadDirectories(parent, (DirectoryInfo)parent.Tag);
            }
        }

        private void LoadDirectories(Node parent, DirectoryInfo directoryInfo)
        {
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            foreach (DirectoryInfo dir in directories)
            {
                if ((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) 
                    continue;

                Node node = new Node();
                node.Tag = dir;
                node.Text = string.Format("{0} ({1})", dir.Name, dir.GetFiles().Length);
                node.Image = Properties.Resources.FolderClosed;
                node.ImageExpanded = Properties.Resources.FolderOpen;
                node.ExpandVisibility = dir.GetDirectories().Length != 0 ? eNodeExpandVisibility.Visible : eNodeExpandVisibility.Hidden;
                parent.Nodes.Add(node);
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
        }

        private void Tile_Clicked(object sender, EventArgs e)
        {
            MetroTileItem ti = sender as MetroTileItem;
            if (ti == null)
                return;
             
            m_selectedAsset = SysUtil.GetRelativePath((string)ti.Tag, m_assetRoot);
        }

        private void Tile_DoubleClicked(object sender, EventArgs e)
        {
            MetroTileItem ti = sender as MetroTileItem;
            if (ti == null)
                return;

            m_selectedAsset = SysUtil.GetRelativePath((string)ti.Tag, m_assetRoot);
        }
    }
}
