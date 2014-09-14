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
        public bool SetClippedContent(List<Node> nodes)
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

            m_clippedContent = new List<string>();
            m_clippedNodes = new List<Node>();
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

            return newlyCreated;
        }

        private List<Node> m_clippedNodes;
        private List<string> m_clippedContent;
    }
}
