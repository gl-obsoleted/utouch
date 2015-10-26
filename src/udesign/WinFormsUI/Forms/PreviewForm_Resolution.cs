using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ucore;
using ulib;
using ulib.Base;

namespace udesign
{
    partial class PreviewForm 
    {
        private void SelectResolution(ResolutionV2 resolution, ToolStripMenuItem resolutionMenuItem, ButtonX resolutionButton)
        {
            if (resolution == null || resolutionMenuItem == null || resolutionButton == null)
                throw new ArgumentNullException("resolution", "选择分辨率时传入了无效的参数.");

            // 先 deselect 之前选中的分辨率
            if (m_selectedResolutionMenuItem != null)
                m_selectedResolutionMenuItem.Checked = false;
            if (m_selectedResolutionButton != null)
                m_selectedResolutionButton.Checked = false;

            m_selectedResolution = resolution;
            m_resolutionLabel.Text = string.Format("当前分辨率 - {0}: {1}x{2}", m_resolutionCategories[m_selectedResolution.category], m_selectedResolution.width, m_selectedResolution.height);

            if (!string.IsNullOrEmpty(m_selectedResolution.tag))
            {
                m_resolutionLabel.Text += string.Format(" ({0})", m_selectedResolution.tag); ;
            }

            m_renderBuffer.ResWidth = m_selectedResolution.width;
            m_renderBuffer.ResHeight = m_selectedResolution.height;

            // 选中新的分辨率对应的菜单和按钮
            m_selectedResolutionMenuItem = resolutionMenuItem;
            m_selectedResolutionMenuItem.Checked = true;
            m_selectedResolutionButton = resolutionButton;
            m_selectedResolutionButton.Checked = true;
        }

        private Control FindButtonByCategory(int category)
        {
            Func<string, int> fnGetIntGlobal = (gname) => { return Convert.ToInt32(LuaRuntime.Instance.BootstrapScript.Globals[gname]); };

            if (category == fnGetIntGlobal("ResCat_Desktop"))
            {
                return m_btResDesktop;
            }
            else if (category == fnGetIntGlobal("ResCat_iOS"))
            {
                return m_btResIOS;
            }
            else if (category == fnGetIntGlobal("ResCat_Andriod"))
            {
                return m_btResAndroid;
            }
            else if (category == fnGetIntGlobal("ResCat_Custom"))
            {
                return m_btResCustom;
            }

            return null;
        }

        private string[] m_resolutionCategories = new string[] { "Desktop", "iOS", "Android", "Custom" };

        private ResolutionV2 m_defaultResolution;
        private ToolStripMenuItem m_defaultResolutionMenuItem;
        private ButtonX m_defaultResolutionButton;

        private ResolutionV2 m_selectedResolution;
        private ToolStripMenuItem m_selectedResolutionMenuItem;
        private ButtonX m_selectedResolutionButton;
    }
}
