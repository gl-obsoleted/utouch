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

            Node sceneRoot = m_scene.GetRootNode();
            TreeNode treeRoot = m_layoutTreeView.Nodes[rootKey];
            treeRoot.Name = sceneRoot.Name;
            treeRoot.Text = GenerateNodeLabel(sceneRoot);
            treeRoot.Tag = sceneRoot;

            PopulateNodeRecursively(treeRoot);
        }

        public void PopulateNodeRecursively(TreeNode treeNode)
        {
            if (treeNode == null)
                return;

            treeNode.Nodes.Clear();
            Node sceneNode = treeNode.Tag as Node;
            if (sceneNode == null)
                return;

            sceneNode.TraverseChildren((Node n) =>
            {
                TreeNode tn = treeNode.Nodes.Add(n.Name, GenerateNodeLabel(n));
                tn.Tag = n;
            });

            foreach (TreeNode tn in treeNode.Nodes)
            {
                PopulateNodeRecursively(tn);
            }
        }

        private string GenerateNodeLabel(Node sceneNode)
        {
            return string.Format("{0} - [{1}]", sceneNode.Name, sceneNode.GetType().Name);
        }

        private DesginerScene m_scene;
    }
}
