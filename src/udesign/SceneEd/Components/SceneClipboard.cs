using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib.Elements;
using ulib;

namespace udesign
{
    public class SceneClipboard
    {
        public bool SetClippedContent(List<Node> nodes, bool isCutting)
        {
            // 确保节点列表有效
            if (nodes.Count == 0)
                return false;

            // 确保节点列表有共同的父节点
            Node parent = nodes[0].Parent;
            foreach (Node n in nodes)
            {
                if (parent != n.Parent)
                {
                    Session.Message("待操作的节点没有一个公共的父节点。");
                    return false;
                }
            }

            foreach (Node n in nodes)
            {
                string str = NodeJsonUtil.NodeToString(n);
                if (str.Length == 0)
                {
                    Session.Message("SetClippedContent() 时发现无效节点，操作失败。");
                    return false;
                }
                m_clippedContent.Add(str);
                m_clippedNodes.Add(n);
            }

            m_isCutting = isCutting;
            return true;
        }

        public List<Node> AttachTo(Node targetParent)
        {
            Node actualParent = targetParent;
            if (m_clippedNodes.Contains(targetParent))
            {
                actualParent = targetParent.Parent;
            }

            if (actualParent == null)
            {
                Session.Message("SceneClipboard.AttachTo() 时发现目标父节点无效，操作失败。");
                return new List<Node>();
            }

            List<Node> newlyCreated = new List<Node>();

            foreach (string str in m_clippedContent)
            {
                Node n = NodeJsonUtil.StringToNode(str);
                if (n == null)
                {
                    Session.Message("SceneClipboard.AttachTo() 时发现无效节点，操作失败。");
                    return new List<Node>();
                }

                n.Position = n.Position + new System.Drawing.Size(15, 15);

                actualParent.Attach(n);
                newlyCreated.Add(n);
            }

            if (m_isCutting)
            {
                foreach (var node in m_clippedNodes)
                {
                    if (node.Parent != null)
                    {
                        node.Parent.Detach(node);
                    }
                }
            }

            m_clippedNodes.Clear();
            m_clippedContent.Clear();
            return newlyCreated;
        }

        public bool IsInUse { get { return m_clippedContent.Count != 0 && m_clippedNodes.Count != 0; } }

        private List<Node> m_clippedNodes = new List<Node>();
        private List<string> m_clippedContent = new List<string>();
        bool m_isCutting = false;
    }
}
