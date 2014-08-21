using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib;
using ui_lib.Elements;

namespace ui_designer_shell
{
    public partial class SceneEd
    {
        public Node PossibleDraggingTarget { get { return m_possibleTargetNode; } }

        public void DragUpdated(int curX, int curY)
        {
            m_draggingTargetPoint.X = curX;
            m_draggingTargetPoint.Y = curY;

            Node picked = m_scene.Pick(m_draggingTargetPoint);
            if (picked != m_possibleTargetNode)
            {
                m_possibleTargetNode = picked;
                SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_Rendering);
            }
        }

        public void DragDroppped(int curX, int curY, string draggingInfo)
        {
            DragUpdated(curX, curY);

            if (m_possibleTargetNode != null)
            {
                Type t = TypeRegistry.QueryType(draggingInfo);
                if (t != null)
                {
                    Node n = Activator.CreateInstance(t) as Node;
                    if (n != null)
                    {
                        n.SetPositionClamped(m_draggingTargetPoint - (Size)(m_possibleTargetNode.GetWorldPosition()));
                        m_possibleTargetNode.Attach(n);

                        Action act = new Action_Insert(m_possibleTargetNode, n, m_draggingTargetPoint);
                        SceneEd.Instance.OperHistory.PushAction(act);
                        SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
                    }
                }
            }

            DragLeft();
        }

        public void DragLeft()
        {
            m_draggingTargetPoint.X = 0;
            m_draggingTargetPoint.Y = 0;
            m_possibleTargetNode = null;
        }

        Point m_draggingTargetPoint = new Point();
        Node m_possibleTargetNode;
    }
}
