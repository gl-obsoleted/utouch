using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ui_designer;
using ui_lib.Base;
using ui_lib.Elements;

namespace ui_designer_shell
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

            ArchiveUtil.RegisterCreator(ArchiveType.Json, typeof(Archive_Json));

            ResetScene();

            // connect the layout tree and the property grid
            m_uiLayoutTree.SelectionChange += m_uiPropertyGrid.OnLayoutTreeSelectionChange;
            m_uiPropertyGrid.PropertyValueChanged += () => { m_glCtrl.Refresh(); };
            m_uiPropertyGrid.ValidateNodeName += (node, newName) => { return !NodeNameUtil.HasNameCollisionWithSiblings(node, newName); };
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

        private DesginerScene m_scene;

        private ResForm m_resForm = new ResForm();

        private void m_menuSave_Click(object sender, EventArgs e)
        {
            m_scene.Save(@"testdata\test.json");
        }

        private void m_menuOpen_Click(object sender, EventArgs e)
        {
            // 这里重置前，应先提示用户保存
            ResetScene();
            m_scene.Load(@"testdata\test.json");
            m_uiLayoutTree.PopulateLayout();
            m_glCtrl.Invalidate(true);
        }

        private void m_menuNew_Click(object sender, EventArgs e)
        {
            ResetScene();
        }

        private void ResetScene()
        {
            if (m_scene != null)
                m_scene.Dispose();

            NodeNameUtil.ResetIDAllocLut();

            m_scene = new DesginerScene();
            m_glCtrl.SetScene(m_scene);
            m_uiLayoutTree.SetScene(m_scene);
        }

        private void m_menuResForm_Click(object sender, EventArgs e)
        {
            m_resForm.Show(this);

            string loc = ConfigUserPref.Instance.GetValue("forms.res_form", "location");
            string size = ConfigUserPref.Instance.GetValue("forms.res_form", "size");
            if (loc.Length != 0 && size.Length != 0)
            {
                Point pt = Utilities.StringToPoint(loc);
                Size sz = Utilities.StringToSize(size);
                m_resForm.SetDesktopBounds(pt.X, pt.Y, sz.Width, sz.Height);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Point pt = PointToScreen(m_resForm.Location);
            ConfigUserPref.Instance.SetValue("forms.res_form", "location", Utilities.PointToString(pt));
            ConfigUserPref.Instance.SetValue("forms.res_form", "size", Utilities.SizeToString(m_resForm.Size));
        }
    }
}
