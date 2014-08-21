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
                foreach (var pair in ResourceManager.Instance.ResGroups)
                {
                    ItemContainer ic = new ItemContainer();
                    ic.MultiLine = true;
                    ic.TitleText = pair.Key;
                    ic.Name = pair.Key;
                    ImageResourceGroup rg = pair.Value;
                    foreach (var res in rg.ResLut)
                    {
                        MetroTileItem tile = new MetroTileItem(res.Key, res.Key);
                        tile.TitleText = res.Key;
                        //tile.Image = 
                        tile.DoubleClick += Tile_DoubleClicked;
                        ic.SubItems.Add(tile);
                    }
                    m_metroTilePanel.Items.Add(ic);
                }
            }
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
