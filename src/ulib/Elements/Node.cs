using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using ulib.Base;

namespace ulib.Elements
{
    public delegate void OnIteration(Node n);

    public partial class Node
    {
        #region 公开的逻辑属性，如果不希望被序列化，可放到这里并加上 [JsonIgnore]
        
        [JsonIgnore]
        [Browsable(false)]
        public Node Parent { get { return m_parent; } }

        #endregion


        /// <summary>
        /// 构造一个无父节点的默认有效的对象
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
        }

        public void SetPositionClamped(Point pos)
        {
            Position = pos;
            if (Parent != null)
            {
                NodeSGUtil.ClampBounds(this);
            }
        }

        public void SetSizeClamped(Size size)
        {
            Size = size;
            if (Parent != null)
            {
                NodeSGUtil.ClampBounds(this);
            }
        }

        public void Attach(Node n)
        {
            if (!m_children.Contains(n))
            {
                n.Name = NodeNameUtil.GenerateUniqueChildName(this, n);
                m_children.Add(n);
                n.m_parent = this;
                NodeSGUtil.ClampBounds(n);
            }
        }

        public void Detach(Node n)
        {
            if (m_children.Contains(n))
            {
                m_children.Remove(n);
                n.m_parent = null;
            }
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

        public Point GetWorldPosition()
        {
            Point loc = Position;
            Node p = Parent;
            while (p != null)
            {
                loc.X += p.Position.X;
                loc.Y += p.Position.Y;
                p = p.Parent;
            }
            return loc;
        }

        public Rectangle GetWorldBounds()
        {
            return new Rectangle(GetWorldPosition(), Size);
        }

        public void UnifyParentOfChildren()
        {
            foreach (Node n in m_children)
                n.m_parent = this;
        }

        protected Node m_parent;
        protected List<Node> m_children = new List<Node>();
    }
}
