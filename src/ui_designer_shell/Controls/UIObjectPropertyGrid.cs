using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ui_lib.Elements;

namespace ui_designer_shell.Controls
{
    public delegate void OnPropertyValueChanged();
    public delegate bool ValidateNewNodeName(Node ndoe, string newName);

    public partial class UIObjectPropertyGrid : UserControl
    {
        public event OnPropertyValueChanged PropertyValueChanged;
        public event ValidateNewNodeName ValidateNodeName;

        public UIObjectPropertyGrid()
        {
            InitializeComponent();
        }

        public void OnLayoutTreeSelectionChange(Node n)
        {
            m_propertyGrid.SelectedObject = n;
        }

        public DevComponents.DotNetBar.AdvPropertyGrid GetGridCtrl()
        {
            return m_propertyGrid;
        }

        private void m_propertyGrid_PropertyValueChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyValueChanged != null)
                PropertyValueChanged();
        }

        private void m_propertyGrid_PropertyValueChanging(object sender, DevComponents.DotNetBar.PropertyValueChangingEventArgs e)
        {
            if (e.PropertyName == "Name" && ValidateNodeName != null)
            {
                // 如果验证失败就撤销更改
                if (!ValidateNodeName(m_propertyGrid.SelectedObject as Node, e.NewValue.ToString()))
                {
                    e.Handled = true;
                }
            }
        }

        private void m_propertyGrid_ProvidePropertyValueList(object sender, DevComponents.DotNetBar.PropertyValueListEventArgs e)
        {

        }
    }
}
