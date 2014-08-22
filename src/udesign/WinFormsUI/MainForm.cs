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
using ulib.Elements;

namespace udesign
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            m_glCtrl = new Controls.UIRenderBuffer_GL_Tao();
            m_glCtrl.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Add(m_glCtrl);

            // for debugging purpose only
            m_tabControl.SelectedIndex = 1;
        }

        public bool Init()
        {
            if (!ResetScene(""))
                return false;

            // connect the layout tree and the property grid
            m_uiPropertyGrid.PropertyValueChanged += () => { m_glCtrl.Refresh(); };
            m_uiPropertyGrid.ValidateNodeName += (node, newName) => { return !NodeNameUtil.HasNameCollisionWithSiblings(node, newName); };

            SceneEdEventNotifier.Instance.SelectNode += m_uiLayoutTree.OnSelectSceneNode;
            SceneEdEventNotifier.Instance.SelectNode += m_uiPropertyGrid.OnSelectSceneNode;

            SceneEdEventNotifier.Instance.RefreshScene += (opts) => 
            {
                if (opts.HasFlag(RefreshSceneOpt.Refresh_Layout))
                {
                    m_uiLayoutTree.PopulateLayout();
                }
                if (opts.HasFlag(RefreshSceneOpt.Refresh_Properties))
                {
                    m_uiPropertyGrid.GetGridCtrl().RefreshPropertyValues();
                }

                // Rendering 排在后面是为了反映前两者的变化的结果
                if (opts.HasFlag(RefreshSceneOpt.Refresh_Rendering))
                {
                    m_glCtrl.Refresh();
                }
            };
            return true;
        }

        private void menuItemGwenUnitTest_Click(object sender, EventArgs e)
        {
            GwenUnitTestForm tf = new GwenUnitTestForm();
            tf.Show();
        }

        private Gwen.Control.Button m_testButton;

        private Controls.UIRenderBuffer_GL_Tao m_glCtrl;

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_testButton = new Gwen.Control.Button(m_glCtrl.GetCanvas());
        }

        private ResForm m_resForm;

        private void m_menuSave_Click(object sender, EventArgs e)
        {
            Scene.Instance.Save(@"testdata\test" + Constants.LayoutPostfix);
        }

        private void m_menuOpen_Click(object sender, EventArgs e)
        {
            // 这里重置前，应先提示用户保存
            if (!ResetScene(@"testdata\test" + Constants.LayoutPostfix))
                Session.Message("打开文件失败。");
        }

        private void m_menuNew_Click(object sender, EventArgs e)
        {
            ResetScene("");
        }

        private bool ResetScene(string sceneName)
        {
            SceneEd.Instance.Selection.Clear();
            SceneEd.Instance.OperHistory.Clear();

            BootParams bp = new BootParams { 
                ReourceImages = ConfigTypical.Instance.ReourceImages,
                ScenePath = sceneName 
            };
            if (!Bootstrap.Instance.Init(bp))
                return false;

            m_glCtrl.Refresh();
            m_uiLayoutTree.PopulateLayout();
            return true;
        }

        private void m_menuResForm_Click(object sender, EventArgs e)
        {
            m_resForm = new ResForm();
            m_resForm.ApplyImage += m_resForm_ApplyImage;
            m_resForm.Show(this);

            string loc = ConfigUserPref.Instance.GetValue("forms.res_form", "location");
            string size = ConfigUserPref.Instance.GetValue("forms.res_form", "size");
            if (loc.Length != 0 && size.Length != 0)
            {
                Point pt = BaseUtil.StringToPoint(loc);
                Size sz = BaseUtil.StringToSize(size);
                m_resForm.SetDesktopBounds(pt.X, pt.Y, sz.Width, sz.Height);
            }
        }

        void m_resForm_ApplyImage(string atlasFileName, string imageName)
        {
            if (SceneEd.Instance.Selection.Count == 1)
            {
                ImageNode sel = SceneEd.Instance.Selection.First() as ImageNode;
                if (sel != null)
                {
                    string newLoc = BaseUtil.ComposeResURL(atlasFileName, imageName);
                    Session.Log("ImageNode '{0}' URL changed. (old: {1}, new: {2})", sel.Name, sel.Res, newLoc);
                    sel.Res = newLoc;
                    SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_Rendering | RefreshSceneOpt.Refresh_Properties);
                }
                else
                {
                    Session.Message("现在暂不支持设置到非 ImageNode 节点.");
                }
            }
            else
            {
                Session.Message("请选中单个的 ImageNode 节点后再试 (现有 {0} 个节点被选中).", SceneEd.Instance.Selection.Count);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_resForm != null)
            {
                Point pt = PointToScreen(m_resForm.Location);
                ConfigUserPref.Instance.SetValue("forms.res_form", "location", BaseUtil.PointToString(pt));
                ConfigUserPref.Instance.SetValue("forms.res_form", "size", BaseUtil.SizeToString(m_resForm.Size));
            }
        }

        private void m_menuDelete_Click(object sender, EventArgs e)
        {
            SceneEd.Instance.DeleteSelected();
        }

        private void m_menuUndo_Click(object sender, EventArgs e)
        {
            SceneEd.Instance.Undo();
        }

        private void m_menuRedo_Click(object sender, EventArgs e)
        {
            SceneEd.Instance.Redo();
        }
    }
}
