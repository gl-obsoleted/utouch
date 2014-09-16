using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib;
using ulib.Base;
using ulib.Elements;

namespace udesign
{
    public class Action_Resize : Action
    {
        public Action_Resize(Node sel)
        {
            m_selection = sel;
            m_initialSizes = m_selection.GetWorldBounds();
        }

        public void UpdateResizing(Rectangle bounds)
        {
            // 当任意节点都支持自动转换为滑动面板之后，让父节点在缩放时保证其包围盒能包含所有子节点已经意义不大。
            // 所以下面的 clamping 代码已被注释掉。
            //Rectangle childrenBounds = m_selection.GetChildrenWorldBounds();
            //if (childrenBounds == Constants.INVALID_RECT)
            //{
            //    m_selection.Position = m_selection.Parent != null ? m_selection.Parent.WorldToLocal(bounds.Location) : bounds.Location;
            //    m_selection.Size = bounds.Size;
            //}
            //else
            //{
            //    int left = System.Math.Min(bounds.Left, childrenBounds.Left);
            //    int right = System.Math.Max(bounds.Right, childrenBounds.Right);
            //    int top = System.Math.Min(bounds.Top, childrenBounds.Top);
            //    int bottom = System.Math.Max(bounds.Bottom, childrenBounds.Bottom);
            //    m_selection.Position = new Point(left, top);
            //    m_selection.Size = new Size(right - left, bottom - top);
            //}

            Rectangle newBounds = bounds;

            // Resize 时做出了一些限制 - 缩放时不可以超出当前父节点的边界
            if (m_selection.Parent != null)
                newBounds = ulib.Base.Math.Clamp(bounds, m_selection.Parent.GetWorldBounds());

            // Resize 可能会影响到控件的位置，这里处理
            m_selection.Position = newBounds.Location;
            if (m_selection.Parent != null)
                m_selection.Position = m_selection.Parent.WorldToLocal(newBounds.Location, true);

            m_selection.Size = newBounds.Size;
        }

        public void EndResizing(Rectangle bounds)
        {
            UpdateResizing(bounds);

            m_finalSizes = m_selection.GetWorldBounds();
        }

        public Node m_selection;
        public Rectangle m_initialSizes;
        public Rectangle m_finalSizes;

        public override void Undo()
        {
            m_selection.Position = m_initialSizes.Location;
            m_selection.Size = m_initialSizes.Size;
        }

        public override void Redo()
        {
            m_selection.Position = m_finalSizes.Location;
            m_selection.Size = m_finalSizes.Size;
        }
    }
}
