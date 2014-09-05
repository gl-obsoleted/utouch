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
        Action_Move m_dragAction;
        Point m_beginDragPos = new Point(0, 0);

        public bool IsDraggingLeft() { return m_dragAction != null; }

        public void DragLeft_Begin(Point location)
        {
            m_beginDragPos = location;
            m_dragAction = new Action_Move(m_selectionList.Selection);
            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
        }

        public void DragLeft_Updated(Point location)
        {
            if (m_dragAction != null)
            {
                m_dragAction.UpdatePosition(location - (Size)(m_beginDragPos));
            }

            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_Rendering | RefreshSceneOpt.Refresh_Properties);
        }

        public void DragLeft_End(Point location)
        {
            m_dragAction.EndUpdatePosition(location - (Size)(m_beginDragPos));
            m_operHistory.PushAction(m_dragAction);
            m_dragAction = null;
        }
    }
}
