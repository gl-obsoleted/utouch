using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Base;

namespace ui_lib.Elements
{
    public delegate void OnIteration(Node n);

    public class Node
    {
        /// <summary>
        /// 常用属性（只读）
        /// </summary>
        public Node Parent { get { return m_parent; } }
        /// <summary>
        /// 常用属性（可读可写）
        /// </summary>
        public Point Position { get; set; }
        public Size Size { get; set; }
        public bool Visible { get; set; }
        public Alignment AlignH { get; set; }
        public Alignment AlignV { get; set; }
        public int Margin { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 构造一个默认有效的对象（无父节点）
        /// </summary>
        public Node()
        {
            Position = new Point(0, 0);
            Size = new Size(100, 100);
            Visible = true;
            AlignH = Alignment.Center;
            AlignV = Alignment.Center;
            Margin = 3;
            m_children = new List<Node>();
        }

        /// <summary>
        /// 我们显式地 SetParent 而不是 set 属性 Parent，语义上更明确.
        /// </summary>
        /// <param name="parent"></param>
        public virtual void SetParent(Node parent)
        {
            m_parent = parent;
        }

        public void TraverseChildren(OnIteration callback) 
        {
            foreach (Node n in m_children)
            {
                callback(n);
            }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(Position, Size);
        }

        private Node m_parent;
        private List<Node> m_children = new List<Node>();
    }
}
