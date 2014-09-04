using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Rectangle childrenBounds = m_selection.GetChildrenWorldBounds();
            if (childrenBounds == Constants.INVALID_RECT)
            {
                m_selection.Position = m_selection.Parent != null ? m_selection.Parent.WorldToLocal(bounds.Location) : bounds.Location;
                m_selection.Size = bounds.Size;
            }
            else
            {
                int left = System.Math.Min(bounds.Left, childrenBounds.Left);
                int right = System.Math.Max(bounds.Right, childrenBounds.Right);
                int top = System.Math.Min(bounds.Top, childrenBounds.Top);
                int bottom = System.Math.Max(bounds.Bottom, childrenBounds.Bottom);
                m_selection.Position = new Point(left, top);
                m_selection.Size = new Size(right - left, bottom - top);
            }
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
