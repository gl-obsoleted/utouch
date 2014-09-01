using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using udesign;
using ulib;
using ulib.Base;

using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;

namespace udesign
{
    public delegate void ApplyImageHandler(string atlasFileName, string imageName);

    public partial class ResForm : Form
    {
        public ResForm()
        {
            InitializeComponent();
        }

        public event ApplyImageHandler ApplyImage;

        private void ResForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                m_metroTilePanel.Items.Clear();
                AddResGroup(ResourceManager.Instance.DefaultResGroup.ResFilePath, ResourceManager.Instance.DefaultResGroup);
                foreach (var pair in ResourceManager.Instance.ResGroups)
                {
                    AddResGroup(pair.Key, pair.Value);
                }
            }
        }

        private void AddResGroup(string name, ImageResourceGroup group)
        {
            ItemContainer ic = new ItemContainer();
            ic.MultiLine = true;
            ic.TitleText = name;
            ic.Name = name;
            foreach (var res in group.ResLut)
            {
                MetroTileItem tile = new MetroTileItem(res.Key, res.Key);
                tile.TitleText = res.Key;
                //tile.Image = 
                tile.DoubleClick += Tile_DoubleClicked;
                ic.SubItems.Add(tile);
            }
            m_metroTilePanel.Items.Add(ic);
        }

        private void Tile_DoubleClicked(object sender, EventArgs e)
        {
            if (ApplyImage != null)
            {
                MetroTileItem ti = sender as MetroTileItem;
                if (ti != null && ti.Parent != null)
                {
                    ItemContainer ic = ti.Parent as ItemContainer;
                    if (ic != null)
                    {
                        ApplyImage(ic.Name, ti.Name);
                    }
                }
            }
        }
    }
}
