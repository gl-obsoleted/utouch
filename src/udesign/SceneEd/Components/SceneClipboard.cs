using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib.Elements;
using ulib;
using ucore;

namespace udesign
{
    public class SceneClipboard
    {
        public bool SetClippedContent(List<Node> nodes, bool isCutting)
        {
            ClearClippedContent();

            // 确保节点列表有效
            if (nodes.Count == 0)
                return false;

            // 确保节点列表有共同的父节点
            Node parent = nodes[0].Parent;
            foreach (Node n in nodes)
            {
                if (parent != n.Parent)
                {
                    Logging.Instance.Message("待操作的节点没有一个公共的父节点。");
                    return false;
                }
            }

            foreach (Node n in nodes)
            {
                string str = NodeJsonUtil.NodeToString(n);
                if (str.Length == 0)
                {
                    Logging.Instance.Message("SetClippedContent() 时发现无效节点，操作失败。");
                    return false;
                }
                m_clippedContent.Add(str);
                m_clippedNodes.Add(n);
            }

            ResetOffset();
            m_isCutting = isCutting;
            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
            return true;
        }

        public List<Node> AttachTo(Node targetParent)
        {
            List<Node> newlyCreated = new List<Node>();

            if (m_clippedNodes.Count == 0)
                return newlyCreated;

            Node actualParent = targetParent;
            if (m_clippedNodes.Contains(targetParent) || m_clippedNodes[0].Parent == targetParent.Parent)
            {
                actualParent = targetParent.Parent;
            }

            if (actualParent == null)
            {
                Logging.Instance.Message("SceneClipboard.AttachTo() 时发现目标父节点无效，操作失败。");
                return newlyCreated;
            }

            List<Node> tmpList = new List<Node>();
            foreach (string str in m_clippedContent)
            {
                Node n = NodeJsonUtil.StringToNode(str);
                if (n == null)
                {
                    Logging.Instance.Message("SceneClipboard.AttachTo() 时发现无效节点，操作失败。");
                    return newlyCreated;
                }

                n.Position = n.Position + new System.Drawing.Size(m_offset, m_offset);

                tmpList.Add(n);
            }

            foreach (var node in tmpList)
            {
                actualParent.Attach(node);
                newlyCreated.Add(node);
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

            IncrementOffset();
            return newlyCreated;
        }

        public bool IsInUse { get { return m_clippedContent.Count != 0 && m_clippedNodes.Count != 0; } }

        private List<Node> m_clippedNodes = new List<Node>();
        private List<string> m_clippedContent = new List<string>();
        bool m_isCutting = false;

        private void ClearClippedContent()
        {
            m_clippedNodes.Clear();
            m_clippedContent.Clear();
        }

        private void IncrementOffset()
        {
            m_offset += OffsetUnit;
        }

        private void ResetOffset()
        {
            m_offset = OffsetUnit;
        }

        int m_offset = OffsetUnit;

        private const int OffsetUnit = 25;
    }
}
