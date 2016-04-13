using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ulib.Elements;
using DevComponents.DotNetBar;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace udesign.Controls
{
    public delegate void OnPropertyValueChanged();

    public partial class UIObjectPropertyGrid : UserControl
    {
        public event OnPropertyValueChanged PropertyValueChanged;

        string[] m_resProperties = { "Res", "Res_Normal", "Res_Pressed", "Res_On", "Res_Off", "Res_Background", "Res_Progress" };

        public UIObjectPropertyGrid()
        {
            InitializeComponent();

            foreach (var prop in m_resProperties)
            {
                PropertySettings propertySetting = new PropertySettings(prop);
                propertySetting.UITypeEditor = new ImageResourceEditor();
                m_propertyGrid.PropertySettings.Add(propertySetting);
            }
        }

        public void OnSelectSceneNode(Node n, object sender)
        {
            m_propertyGrid.SelectedObject = n;
        }

        public DevComponents.DotNetBar.AdvPropertyGrid GetGridCtrl()
        {
            return m_propertyGrid;
        }

        Action_PropertyChange m_actChangeProperty;
        private void m_propertyGrid_PropertyValueChanging(object sender, DevComponents.DotNetBar.PropertyValueChangingEventArgs e)
        {
            Node n = m_propertyGrid.SelectedObject as Node;
            if (n == null)
                return;

            m_actChangeProperty = new Action_PropertyChange(n);
        }

        private void m_propertyGrid_PropertyValueChanged(object sender, PropertyChangedEventArgs e)
        {
            if (m_actChangeProperty != null)
            {
                m_actChangeProperty.ChangeCommitted();
                ActionQueue.Instance.PushAction(m_actChangeProperty);
                m_actChangeProperty = null;
            }

            if (PropertyValueChanged != null)
                PropertyValueChanged();
        }

        private void m_propertyGrid_ValidatePropertyValue(object sender, ValidatePropertyValueEventArgs e)
        {
            if (e.PropertyName == "Name")
            {
                if (e.NewValue == null || NodeNameUtil.HasNameCollisionWithSiblings(m_propertyGrid.SelectedObject as Node, e.NewValue.ToString()))
                {
                    // 未通过验证就撤销更改
                    e.Cancel = true;
                    e.Message = "该命名与当前父节点下的其他节点有冲突，请重新命名。";
                }
            }
        }
    }

    public class ImageResourceEditor : System.Drawing.Design.UITypeEditor
    {
        public ImageResourceEditor()
        {
        }

        // Indicates whether the UITypeEditor provides a form-based (modal) dialog,  
        // drop down dialog, or no UI outside of the properties window. 
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        // Displays the UI for value selection. 
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            // Return the value if the value is not of type Int32, Double and Single. 
            if (value.GetType() != typeof(string))
                return value;

            // Uses the IWindowsFormsEditorService to display a  
            // drop-down UI in the Properties window.
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                ResForm form = new ResForm();
                if (edSvc.ShowDialog(form) == System.Windows.Forms.DialogResult.OK)
                {
                    return form.SelectedResourceURL;
                }
            }
            return value;
        }

        // Indicates whether the UITypeEditor supports painting a  
        // representation of a property's value. 
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
