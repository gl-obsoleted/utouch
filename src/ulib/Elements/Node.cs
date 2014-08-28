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
            LockChildrenLayoutRecursively = false;
            Dock = DockType.None;
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
            return new Rectangle(GetDockedPos(), Size);
        }

        public Point GetWorldPosition()
        {
            Point loc = GetDockedPos();
            Node p = Parent;
            while (p != null)
            {
                Point parentLoc = p.GetDockedPos();
                loc.X += parentLoc.X;
                loc.Y += parentLoc.Y;
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


        public Point GetDockedPos()
        {
            if (Parent == null)
                return Position;

            Point dockedPos = Position;
            switch (Dock)
            {
                case Base.DockType.Left:
                    dockedPos.X = 0;
                    break;
                case Base.DockType.Right:
                    dockedPos.X = Parent.Size.Width - Size.Width;
                    break;
                case Base.DockType.Top:
                    dockedPos.Y = 0;
                    break;
                case Base.DockType.Bottom:
                    dockedPos.Y = Parent.Size.Height - Size.Height;
                    break;
                case Base.DockType.Center:
                    dockedPos.X = Parent.Size.Width / 2 - Size.Width / 2;
                    dockedPos.Y = Parent.Size.Height / 2 - Size.Height / 2;
                    break;

                case Base.DockType.None:
                default:
                    break;
            }

            return dockedPos;
        }

        protected Node m_parent;
        protected List<Node> m_children = new List<Node>();
    }
}
