using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Browsable(false)]
        public Node Parent { get { return m_parent; } }
        public List<Node> Children { get { return m_children; } }

        /// <summary>
        /// 常用属性（可读可写）
        /// </summary>
        [Category("Node")]
        [Description("相对父节点的位置 (影响所有子节点)")]
        public Point Position { get; set; }
        [Category("Node")]
        [Description("尺寸")]
        public Size Size { get; set; }
        [Category("Node")]
        [Description("是否可见 (影响所有子节点)")]
        public bool Visible { get; set; }
        [Category("Node")]
        [Description("水平方向上的对齐")]
        public AlignHori AlignH { get; set; }
        [Category("Node")]
        [Description("垂直方向上的对齐")]
        public AlignVert AlignV { get; set; }
        [Category("Node")]
        [Description("外部间距")]
        public int Margin { get; set; }
        [Category("Node")]
        [Description("名字 (可指定，但需要在当前路径下唯一)")]
        public string Name { get; set; }
        [Category("Node")]
        [Description("标记 (可任意起，不影响正常逻辑)")]
        public string Tag { get; set; }

        /// <summary>
        /// 构造一个默认有效的对象（无父节点）
        /// </summary>
        public Node()
        {
            Name = this.GetType().Name;
            Position = new Point(0, 0);
            Size = new Size(100, 100);
            Visible = true;
            AlignH = AlignHori.Center;
            AlignV = AlignVert.Middle;
            Margin = 3;
            m_children = new List<Node>();
        }

        public void Attach(Node n)
        {
            n.Name = NodeUtil.GenerateUniqueChildName(this, n);
            m_children.Add(n);
            n.m_parent = this;
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

        protected Node m_parent;
        protected List<Node> m_children = new List<Node>();
    }
}
