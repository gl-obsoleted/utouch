using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib;
using ulib.Elements;

namespace udesign
{
    public class DragAndDropReceiver
    {
        public void NotifyUpdated(int curX, int curY)
        {
            m_targetPoint.X = curX;
            m_targetPoint.Y = curY;

            Node picked = Scene.Instance.Pick(m_targetPoint);
            if (picked != m_targetNode)
            {
                m_targetNode = picked;
                SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_Rendering);
            }
        }

        public void NotifyDroppped(int curX, int curY, string draggingInfo)
        {
            NotifyUpdated(curX, curY);

            if (m_targetNode != null)
            {
                Type t = TypeRegistry.Instance.QueryType(draggingInfo);
                if (t != null)
                {
                    Node n = Activator.CreateInstance(t) as Node;
                    if (n != null)
                    {
                        n.SetPositionClamped(m_targetPoint - (Size)(m_targetNode.GetWorldPosition()));
                        m_targetNode.Attach(n);

                        Action act = new Action_Insert(m_targetNode, n, m_targetPoint);
                        ActionQueue.Instance.PushAction(act);
                        SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
                    }
                }
            }

            NotifyLeft();
        }

        public void NotifyLeft()
        {
            m_targetPoint.X = 0;
            m_targetPoint.Y = 0;
            m_targetNode = null;
        }

        public void Render(Gwen.Renderer.Tao renderer, GwenRenderContext ctx)
        {
            if (m_targetNode != null)
            {
                Rectangle rect = m_targetNode.GetWorldBounds();
                rect.Inflate(5, 5);

                Color c = renderer.DrawColor;
                renderer.DrawColor = Color.HotPink;
                renderer.DrawLinedRect(rect);
                renderer.RenderText(ctx.m_font, new Point(rect.Left, rect.Top - 18), "[目标节点] " + m_targetNode.Name);
                renderer.DrawColor = c;
            }
        }

        private Point m_targetPoint = new Point();
        private Node m_targetNode;
    }
}
