using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace udesign
{
    public partial class SceneEd
    {
        Action_Scroll m_scrollAction;
        Point m_beginDragRightScrollingPos = new Point(0, 0);

        public bool IsDragRightScrolling() { return m_scrollAction != null; }

        public void DragRight_BeginScrolling(Point location)
        {
            m_selectionList.IsScrolling = true;
            m_beginDragRightScrollingPos = location;
            m_scrollAction = new Action_Scroll(m_selectionList.Selection[0]);
            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
        }

        public void DragRight_UpdateScrolling(Point location)
        {
            if (m_scrollAction != null)
            {
                Point scrollOffset = m_beginDragRightScrollingPos - (Size)location;
                m_scrollAction.UpdateScrollOffset(scrollOffset);
                SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_Rendering | RefreshSceneOpt.Refresh_Properties);
            }
        }

        public void DragRight_EndScrolling(Point location)
        {
            if (m_scrollAction != null)
            {
                m_selectionList.IsScrolling = false;
                Point scrollOffset = m_beginDragRightScrollingPos - (Size)location;
                m_scrollAction.EndScrollOffset(scrollOffset);
                ActionQueue.Instance.PushAction(m_scrollAction);
                m_scrollAction = null;
            }
        }
    }
}
