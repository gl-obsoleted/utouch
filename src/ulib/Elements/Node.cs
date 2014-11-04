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
        #region 所有的序列化存取字段

        /// 这里用到的 Attribute 的含义：
        ///     [Category("")]      是指该属性在 AdvPropertyGrid 内所属的分类
        ///     [Description("")]   是指该属性在 AdvPropertyGrid 内选中时的描述
        ///     [JsonIgnore]        是指该属性不存储到文件里（暗含的意思是，在读取并重建对象后，这个字段是需要被恢复的）
        ///     [Browsable(false)]  是指该属性不显示在属性编辑的列表中，即 AdvPropertyGrid 控件里
        ///     [ReadOnly(true)]    是指该属性为只读属性，不可编辑

        [Category("Node")]
        [DisplayName("名字")]
        [Description("名字 (可指定，但需要在当前路径下唯一)")]
        public string Name { get; set; }
        [Category("Node")]
        [DisplayName("位置")]
        [Description("相对父节点的位置 (影响所有子节点)")]
        public Point Position { get; set; }
        [Category("Node")]
        [DisplayName("尺寸")]
        [Description("尺寸")]
        public Size Size { get { return m_size; } set { InternalSetSize(value); } }
        [Category("Node")]
        [DisplayName("逻辑尺寸")]
        [Description("逻辑尺寸（默认为零，当小于等于 Size 时认为二者相同，当逻辑尺寸 X 或 Y 大于可视尺寸 Size 时，对应维度转为可滑动）")]
        public Size LogicalSize { get; set; }
        [Category("Node")]
        [DisplayName("是否可见")]
        [Description("是否可见 (影响所有子节点)")]
        public bool Visible { get; set; }
        [Category("Node")]
        [DisplayName("停靠")]
        [Description("外部 Dock，用于描述与父节点的关系，是节点通用属性")]
        public DockType Dock { get; set; }
        [Category("Node")]
        [DisplayName("水平对齐")]
        [Description("水平方向上的内部对齐（非一般对齐用途，目前仅用在 TextNode 的内部对齐上）")]
        public AlignHori AlignH { get; set; }
        [Category("Node")]
        [DisplayName("垂直对齐")]
        [Description("垂直方向上的内部对齐（非一般对齐用途，目前仅用在 TextNode 的内部对齐上）")]
        public AlignVert AlignV { get; set; }
        [Category("Node")]
        [DisplayName("外部间距")]
        [Description("外部间距")]
        public int Margin { get; set; }
        [Category("Node")]
        [DisplayName("标记")]
        [Description("标记 (可任意起，不影响正常逻辑)")]
        public string Tag { get; set; }
        [Category("Node")]
        [DisplayName("是否锁定")]
        [Description("是否锁定，锁定的控件无法选中，移动和缩放")]
        public bool Locked { get; set; }

        // 以下为编辑器内不可见的字段，这些字段不出现在编辑器里，所以不需要 Category 和 Description
        [Browsable(false)]
        public object UserData { get; set; }
        [Browsable(false)]
        [ReadOnly(true)]
        public string __type_info__ { get { return GetType().Name; } }
        [Browsable(false)]
        public virtual List<Node> Children { get { return m_children; } }

        #endregion


        #region 公开的逻辑属性，如果不希望被序列化，可放到这里并加上 [JsonIgnore]；如果不需要在编辑器里看到，可加上 [Browsable(false)]

        [JsonIgnore]
        [Browsable(false)]
        public Node Parent { get { return m_parent; } }
        /// <summary>
        /// 对于复合型控件，这个属性能够避免子节点位置，尺寸 Dock 等被修改
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public virtual bool LockChildrenLayoutRecursively { get; set; }
        /// <summary>
        /// 正常情况下为 0，当这个节点可卷动时，这个值则表现为当前卷动的偏移量
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public virtual Point CurrentScrollOffset { get; set; }
        [JsonIgnore]
        [Browsable(false)]
        public virtual bool HasScrolled { get { return !CurrentScrollOffset.IsEmpty; } }

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
            Locked = false;
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

        public void SetScrollOffsetClamped(Point offset)
        {
            Point newOffset = new Point();
            newOffset.X = ucore.EzMath.Clamp(offset.X, 0, LogicalSize.Width - Size.Width);
            newOffset.Y = ucore.EzMath.Clamp(offset.Y, 0, LogicalSize.Height - Size.Height);
            CurrentScrollOffset = newOffset;
        }

        public virtual void Attach(Node n)
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
                if (p.HasScrolled)
                {
                    loc.X -= p.CurrentScrollOffset.X;
                    loc.Y -= p.CurrentScrollOffset.Y;
                }

                Point parentLoc = p.GetDockedPos();
                loc.X += parentLoc.X;
                loc.Y += parentLoc.Y;
                p = p.Parent;
            }
            return loc;
        }

        public Point WorldToLocal(Point pt, bool logical)
        {
            Point ret = pt;
            ret -= (Size)GetWorldPosition();

            if (logical)
            {
                ret += (Size)CurrentScrollOffset;
            }
            return ret;
        }

        public Point LocalToWorld(Point pt, bool logical)
        {
            Point ret = pt;
            ret += (Size)GetWorldPosition();

            if (logical)
            {
                ret -= (Size)CurrentScrollOffset;
            }
            return ret;
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

        public Rectangle GetChildrenWorldBounds()
        {
            Rectangle rect = ucore.Const.INVALID_RECT;
            foreach (Node child in Children)
            {
                if (ucore.EzMath.IsInvalid(rect))
                {
                    rect = child.GetWorldBounds();
                }
                else
                {
                    rect = Rectangle.Union(rect, child.GetWorldBounds());
                }
            }
            return rect;
        }

        public bool IsScrollable()
        {
            return IsScrollableH() || IsScrollableV();
        }

        public bool IsScrollableH()
        {
            return LogicalSize.Width > Size.Width;
        }

        public bool IsScrollableV()
        {
            return LogicalSize.Height > Size.Height;
        }

        protected Node m_parent;
        protected List<Node> m_children = new List<Node>();

        protected Size m_size;

        protected void InternalSetSize(Size newSize)
        {
            m_size = newSize;
            
            Rectangle childrenBounds = GetChildrenWorldBounds();
            Point currentTranslate = GetWorldPosition();
            Size minimumSize = new Size(childrenBounds.Right - currentTranslate.X, childrenBounds.Bottom - currentTranslate.Y);

            Size newLogicalSize = LogicalSize;
            if (newSize.Width < minimumSize.Width && LogicalSize.Width < minimumSize.Width)
            {
                newLogicalSize.Width = minimumSize.Width;
            }
            if (newSize.Height < minimumSize.Height && LogicalSize.Height < minimumSize.Height)
            {
                newLogicalSize.Height = minimumSize.Height;
            }
            LogicalSize = newLogicalSize;

            RefitScrollOffset();
        }

        protected void RefitScrollOffset()
        {
            if (IsScrollable())
            {
                if (!CurrentScrollOffset.IsEmpty)
                {
                    Point newOffset = CurrentScrollOffset;
                    if (newOffset.X > LogicalSize.Width - Size.Width)
                        newOffset.X = LogicalSize.Width - Size.Width;
                    if (newOffset.Y > LogicalSize.Height - Size.Height)
                        newOffset.Y = LogicalSize.Height - Size.Height;
                    SetScrollOffsetClamped(newOffset);
                }
            }
            else
            {
                CurrentScrollOffset = Point.Empty;
            }
        }

        public virtual bool IsResizable()
        {
            return !Locked;
        }

        public virtual Size GetExpectedResourceSize()
        {
            return ucore.Const.ZERO_SIZE;            
        }

        public void ResizeToResSize()
        {
            Size size = GetExpectedResourceSize();
            if (size != ucore.Const.ZERO_SIZE)
            {
                Size = size;
                LogicalSize = ucore.Const.ZERO_SIZE;
            }
        }
    }
}
