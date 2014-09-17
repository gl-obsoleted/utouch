using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib.Elements;

namespace udesign
{
    public class Action_Scroll : Action
    {
        public Action_Scroll(Node scrollingNode)
        {
            m_scrollingNode = scrollingNode;
            m_scrollBeginOffset = m_scrollingNode.CurrentScrollOffset;
        }

        public void UpdateScrollOffset(Point movingOffset)
        {
            Point newScrollOffset = m_scrollBeginOffset + (Size)movingOffset;
            m_scrollingNode.SetScrollOffsetClamped(newScrollOffset);
        }

        public void EndScrollOffset(Point movingOffset)
        {
            UpdateScrollOffset(movingOffset);
            m_scrollEndOffset = m_scrollingNode.CurrentScrollOffset;
        }

        public override void Undo()
        {
            m_scrollingNode.SetScrollOffsetClamped(m_scrollBeginOffset);
        }

        public override void Redo()
        {
            m_scrollingNode.SetScrollOffsetClamped(m_scrollEndOffset);
        }

        Node m_scrollingNode;
        Point m_scrollBeginOffset;
        Point m_scrollEndOffset;        
    }
}
