using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_lib.Elements
{
    public class Node
    {
        /// <summary>
        /// 常用属性
        /// </summary>
        public Vec2 Position { get; set; }
        public Size Size { get; set; }
        public bool Visible { get; set; }
        public Node Parent { get { return m_parent; } }

        /// <summary>
        /// 构造一个默认有效的对象（无父节点）
        /// </summary>
        public Node()
        {
            Visible = true;

            Position = new Vec2(0, 0);
            Size = new Size(100, 100);
        }

        /// <summary>
        /// 我们显式地 SetParent 而不是 set 属性 Parent，语义上更明确.
        /// </summary>
        /// <param name="parent"></param>
        public void SetParent(Node parent)
        {
            m_parent = parent;
        }

        private Node m_parent;
    }
}
