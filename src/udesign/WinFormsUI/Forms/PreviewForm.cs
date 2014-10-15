using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ulib;

namespace udesign
{
    public partial class PreviewForm : Form
    {
        public PreviewForm(Scene scn, SceneEd scnEd)
        {
            InitializeComponent();

            m_glCtrl = new Controls.UITaoRenderBuffer();
            m_glCtrl.Dock = DockStyle.Fill;
            m_glCtrl.SetScene(scn);
            m_glCtrl.SetSceneEd(scnEd);
            this.splitContainer1.Panel2.Controls.Add(m_glCtrl);

            ftime = new List<long>();
            stopwatch = new Stopwatch();

            m_defaultResolution = null;
            MoonSharp.Interpreter.Table t = LuaRuntime.Instance.BootstrapScript.Globals["Resolutions"] as MoonSharp.Interpreter.Table;
            foreach (var res in t.Values)
            {
                if (res.Type == MoonSharp.Interpreter.DataType.Table)
                {
                    Resolution resolution = new Resolution();
                    resolution.width = Convert.ToInt32(res.Table["w"]);
                    resolution.height = Convert.ToInt32(res.Table["h"]);
                    resolution.category = Convert.ToInt32(res.Table["cat"]);
                    resolution.tag = (string)res.Table["tag"];

                    Control ctrl = FindButtonByCategory(resolution.category);
                    if (ctrl != null)
                    {
                        ToolStripMenuItem tsi = new ToolStripMenuItem();
                        if (string.IsNullOrEmpty(resolution.tag))
                        {
                            tsi.Text = string.Format("{0}x{1}", resolution.width, resolution.height);
                        }
                        else 
                        {
                            tsi.Text = string.Format("{0}x{1} ({2})", resolution.width, resolution.height, resolution.tag);
                        }
                        tsi.Tag = resolution;
                        tsi.Click += m_resolutionMenuItemClicked;
                        ctrl.ContextMenuStrip.Items.Add(tsi);

                        if (Convert.ToBoolean(res.Table["default"]))
                        {
                            m_defaultResolution = resolution;
                            m_defaultResolutionMenuItem = tsi;
                            m_defaultResolutionButton = ctrl as ButtonX;
                        }
                    }
                }
            }

            // 如果没有在脚本中正确地设置默认分辨率，这里选择使用桌面分辨率的第一个作为默认分辨率
            if (m_defaultResolution == null)
            {
                ucore.SysPost.AssertException(m_btResDesktop.ContextMenuStrip.Items.Count > 0, "The context menu of 'Desktop' button is not empty.");
                ToolStripMenuItem mi = m_btResDesktop.ContextMenuStrip.Items[0] as ToolStripMenuItem;
                if (mi != null)
                {
                    Resolution res = m_btResDesktop.ContextMenuStrip.Items[0].Tag as Resolution;
                    if (res != null)
                    {
                        m_defaultResolution = res;
                        m_defaultResolutionMenuItem = mi;
                        m_defaultResolutionButton = m_btResDesktop;
                    }
                }
            }

            ucore.SysPost.AssertException(m_defaultResolution != null, "The default exception is not set properly.");
            SelectResolution(m_defaultResolution, m_defaultResolutionMenuItem, m_defaultResolutionButton);
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

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            if (ftime.Count == fps_frames)
                ftime.RemoveAt(0);

            ftime.Add(stopwatch.ElapsedMilliseconds - lastTime);
            lastTime = stopwatch.ElapsedMilliseconds;

            if (stopwatch.ElapsedMilliseconds > 1000)
            {
                Gwen.Renderer.Tao renderer = m_glCtrl.GetRenderer();

                //test.Note = String.Format("String Cache size: {0}", renderer.TextCacheSize);
                //test.Fps = 1000f * ftime.Count / ftime.Sum();
                stopwatch.Restart();

                if (renderer.TextCacheSize > 1000) // each cached string is an allocated texture, flush the cache once in a while in your real project
                    renderer.FlushTextCache();
            }
        }

        private Controls.UITaoRenderBuffer m_glCtrl;
        const int fps_frames = 50;
        private readonly List<long> ftime;
        private readonly Stopwatch stopwatch;
        private long lastTime;

        private void GwenUnitTestForm_Load(object sender, EventArgs e)
        {
            // 构造函数内 Canvas 还没初始化，所以挪到这里做
            //test = new Gwen.UnitTest.UnitTest(m_glCtrl.GetCanvas());

            m_glCtrl.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_Paint);

            stopwatch.Restart();
            lastTime = 0;
        }

        private void m_btRefresh_Click(object sender, EventArgs e)
        {

        }

        private void m_btResDesktop_Click(object sender, EventArgs e)
        {
            CtrlUtil.ShowContextMenu(m_btResDesktop);
        }

        private void m_btResIOS_Click(object sender, EventArgs e)
        {
            CtrlUtil.ShowContextMenu(m_btResIOS);
        }

        private void m_btResAndroid_Click(object sender, EventArgs e)
        {
            CtrlUtil.ShowContextMenu(m_btResAndroid);
        }

        private void m_btResCustom_Click(object sender, EventArgs e)
        {
            CtrlUtil.ShowContextMenu(m_btResCustom);
        }

        private void m_resolutionMenuItemClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = sender as ToolStripMenuItem;
            if (mi != null)
            {
                Resolution res = mi.Tag as Resolution;
                Control ctrl = FindButtonByCategory(res.category);
                SelectResolution(res, mi, ctrl as ButtonX);
            }            
        }
    }
}
