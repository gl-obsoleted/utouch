using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ui_designer;
using ui_lib.Elements;

namespace ui_designer_shell.Controls
{
    public partial class UILayoutTree : UserControl
    {
        public UILayoutTree()
        {
            InitializeComponent();

            m_layoutTreeView.Nodes.Clear();
            m_layoutTreeView.Nodes.Add(ShellConstants.UILayoutTree_RootName, "<unspecified>");
        }

        public void SetScene(DesginerScene scene)
        {
            m_scene = scene;
            PopulateLayout();
        }

        public void PopulateLayout()
        {
            string rootKey = ShellConstants.UILayoutTree_RootName;
            bool ret = m_layoutTreeView.Nodes.ContainsKey(rootKey);
            System.Diagnostics.Debug.Assert(ret);
              
            PopulateNodeRecursively(m_scene.GetRootNode(), m_layoutTreeView.Nodes[rootKey]);
        }

        public void PopulateNodeRecursively(Node sceneNode, TreeNode treeNode)
        {
            System.Diagnostics.Debug.Assert(sceneNode != null);
            System.Diagnostics.Debug.Assert(treeNode != null);

            treeNode.Name = sceneNode.Name;
            treeNode.Text = string.Format("{0} - [{1}]", sceneNode.Name, sceneNode.GetType().Name);            
        }

        private DesginerScene m_scene;
    }
}
