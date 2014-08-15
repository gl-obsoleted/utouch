using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ui_designer_shell.WinFormsUI.Controls
{
    public partial class UIControlList : UserControl
    {
        public UIControlList()
        {
            InitializeComponent();
        }

        private void m_controlList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            m_controlList.DoDragDrop(((ListViewItem)e.Item).Text, DragDropEffects.Copy);
        }
    }
}
