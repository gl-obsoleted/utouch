using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ui_designer;
using ui_lib.Elements;

namespace ui_designer_shell
{
    public class SceneEd
    {
        public static SceneEd Instance = new SceneEd();

        public List<Node> Selection { get { return m_selection; } }

        public void ResetScene(DesginerScene scene)
        {
            m_scene = scene;
            m_actionHistory.Clear();
        }

        bool m_isLeftDown = false;
        Point m_beginDragPos = new Point(0, 0);
        Action_Move m_dragAction;
        public void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_isLeftDown = true;

                Node n = m_scene.Pick(e.Location);
                if (m_selection.Contains(n))
                {
                    // 到这里是拖拽
                    m_beginDragPos = e.Location;
                    m_dragAction = new Action_Move(m_selection);
                }
            }
            SceneEdEventNotifier.Instance.Emit_RefreshScene();
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (m_isLeftDown && m_dragAction != null)
            {
                m_dragAction.UpdatePosition(e.Location - (Size)(m_beginDragPos));
            }
            SceneEdEventNotifier.Instance.Emit_RefreshScene();
        }

        public void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_isLeftDown = false;

                if (m_dragAction != null)
                {
                    m_dragAction.EndUpdatePosition(e.Location - (Size)(m_beginDragPos));
                    AddAction(m_dragAction);
                    m_dragAction = null;
                }
                else 
                {
                    Node n = m_scene.Pick(e.Location);
                    SceneEdEventNotifier.Instance.Emit_SelectNode(n, this);

                    m_selection.Clear();
                    if (n != null)
                    {
                        m_selection.Add(n);
                    }
                }
            }
            SceneEdEventNotifier.Instance.Emit_RefreshScene();
        }

        public void AddAction(Action a)
        {
            m_actionHistory.Add(a);
        }

        public void Undo()
        {
            if (m_actionHistory.Count == 0)
                return;

            int last = m_actionHistory.Count - 1;
            Action act = m_actionHistory.ElementAt(last);
            if (act != null)
            {
                act.Undo();
            }

            m_actionHistory.RemoveAt(last);
            m_redoQueue.Add(act);
            SceneEdEventNotifier.Instance.Emit_RefreshScene();
        }

        public void Redo()
        {
            if (m_redoQueue.Count == 0)
                return;

            int last = m_redoQueue.Count - 1;
            Action act = m_redoQueue.ElementAt(last);
            if (act != null)
            {
                act.Redo();
            }

            m_redoQueue.RemoveAt(last);
            m_actionHistory.Add(act);
            SceneEdEventNotifier.Instance.Emit_RefreshScene();
        }

        private DesginerScene m_scene;
        private List<Node> m_selection = new List<Node>();
        private List<Action> m_actionHistory = new List<Action>();
        private List<Action> m_redoQueue = new List<Action>();
    }
}
