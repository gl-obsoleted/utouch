using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using udesign;
using ulib.Elements;
using ulib;
 

namespace udesign.Controls
{
    public partial class UILayoutTree : UserControl
    {
        public UILayoutTree()
        {
            InitializeComponent();

            m_layoutTreeView.Nodes.Clear();
            m_layoutTreeView.SelectedNode = m_layoutTreeView.Nodes.Add(RootNode.Default_Name, "<unspecified>");
        }

        public void PopulateLayout()
        {
            if (!m_layoutTreeView.Nodes.ContainsKey(RootNode.Default_Name))
                return;

            if (Scene.Instance == null)
                return;

            TreeNode treeRoot = m_layoutTreeView.Nodes[RootNode.Default_Name];
            treeRoot.Name = Scene.Instance.Root.Name;
            treeRoot.Text = GenerateNodeLabel(Scene.Instance.Root);
            treeRoot.Tag = Scene.Instance.Root;

            PopulateNodeRecursively(treeRoot);

            m_layoutTreeView.ExpandAll();
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

        public void OnSelectSceneNode(Node n, object sender)
        {
            if (n == null || !m_layoutTreeView.Nodes.ContainsKey(RootNode.Default_Name))
            {
                if (m_layoutTreeView.SelectedNode != null)
                {
                    m_layoutTreeView.SelectedNode = null;
                }
                return;
            }

            TreeNode treeRoot = m_layoutTreeView.Nodes[RootNode.Default_Name];
            TreeNode target = SelectTreeNodeRecursively(treeRoot, n);
            if (target != m_layoutTreeView.SelectedNode)
            {
                m_layoutTreeView.SelectedNode = target;
            }
        }

        public TreeNode SelectTreeNodeRecursively(TreeNode treeNode, Node n)
        {
            if (treeNode == null)
                return null;

            Node sceneNode = treeNode.Tag as Node;
            if (sceneNode == n)
                return treeNode;

            foreach (TreeNode tn in treeNode.Nodes)
            {
                TreeNode target = SelectTreeNodeRecursively(tn, n);
                if (target != null)
                    return target;
            }

            return null;
        }

        private string GenerateNodeLabel(Node sceneNode)
        {
            return string.Format("{0} - [{1}]", sceneNode.Name, sceneNode.GetType().Name);
        }

        private void m_layoutTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = e.Node;
            if (treeNode == null)
                return;

            Node sceneNode = treeNode.Tag as Node;
            if (sceneNode == null)
                return;

            SceneEd.Instance.Select(sceneNode);
        }
    }
}
