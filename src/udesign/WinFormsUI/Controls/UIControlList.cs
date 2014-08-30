using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace udesign.WinFormsUI.Controls
{
    public partial class UIControlList : UserControl
    {
        public UIControlList()
        {
            InitializeComponent();

            m_icons.Images.Add(Properties.Resources.appbar_folder);
            m_icons.ImageSize = new Size(48, 48);

            m_controlList.View = View.LargeIcon;
            m_controlList.LargeImageList = m_icons;
            m_controlList.SmallImageList = m_icons;

            foreach (ListViewItem it in m_controlList.Items)
            {
                it.ImageIndex = 0;
            }
        }

        private void m_controlList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            m_controlList.DoDragDrop(((ListViewItem)e.Item).Text, DragDropEffects.Copy);
        }

        ImageList m_icons = new ImageList();
    }
}
