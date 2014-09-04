using Gwen.ControlInternal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using udesign;
using ulib;
using ulib.Elements;

namespace udesign
{
    public partial class SceneEd
    {
        public static SceneEd Instance = new SceneEd();

        public bool IsHoldingCtrl { get; set; }

        public OperationHistory OperHistory { get { return m_operHistory; } }
        public SelectionContainer SelectionContainer { get { return m_selectionContainer; } }

        bool m_isLeftDown = false;
        Point m_beginDragPos = new Point(0, 0);
        Action_Move m_dragAction;
        public void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_isLeftDown = true;

                Node n = Scene.Instance.Pick(e.Location);
                if (m_selectionContainer.Selection.Contains(n) && m_selectionContainer.IsSelectionDraggable())
                {
                    // 到这里触发拖拽
                    m_beginDragPos = e.Location;
                    m_dragAction = new Action_Move(m_selectionContainer.Selection);
                    SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
                }
            }
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (m_isLeftDown)
            {
                if (m_dragAction != null)
                {
                    m_dragAction.UpdatePosition(e.Location - (Size)(m_beginDragPos));
                }

                SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_Rendering | RefreshSceneOpt.Refresh_Properties);
            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_isLeftDown = false;

                if (m_dragAction != null)
                {
                    m_dragAction.EndUpdatePosition(e.Location - (Size)(m_beginDragPos));
                    m_operHistory.PushAction(m_dragAction);
                    m_dragAction = null;
                }
                else 
                {
                    Node n = Scene.Instance.Pick(e.Location);
                    Select(n);
                }
            }
            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
        }

        public void Undo()
        {
            m_operHistory.Undo();
            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
        }

        public void Redo()
        {
            m_operHistory.Redo();
            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
        }

        public void Select(Node n)
        {
            if (!IsHoldingCtrl)
            {
                m_selectionContainer.Selection.Clear();
            }
            if (n != null)
            {
                m_selectionContainer.Selection.Add(n);
                SceneEdEventNotifier.Instance.Emit_SelectNode(n, this);
            }
            else
            {
                SceneEdEventNotifier.Instance.Emit_SelectNode(null, this);
            }

            m_selectionContainer.OnSelectionChanged();
            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_Rendering);
        }

        public void DeleteSelected()
        {
            if (m_selectionContainer.HasSelectedRoot())
            {
                Session.Message("无法删除根节点。");                
            }
            else
            {
                m_operHistory.PushAction(new Action_Delete(m_selectionContainer.Selection));
                m_selectionContainer.ClearSelection();
            }
        }

        private OperationHistory m_operHistory = new OperationHistory();
        private SelectionContainer m_selectionContainer = new SelectionContainer();

        internal void InitSelectionContainer(Gwen.Control.Canvas canvas)
        {
            m_selectionContainer.Init(canvas);
            m_selectionContainer.Resizer.Resized += Resizer_Resized;
            for (int i = 0; i < 10; i++)
            {
                Resizer r = m_selectionContainer.Resizer.GetInternalResizer(i);
                if (r == null)
                    continue;

                r.BeginDrag += Resizer_Begin;
                r.EndDrag += Resizer_End;
            }
        }

        Action_Resize m_resizeAction;

        void Resizer_Begin(Gwen.Control.Base sender, EventArgs arguments)
        {
            if (m_selectionContainer.Resizer.IsHoveringResizers() && m_selectionContainer.Selection.Count == 1)
            {
                m_resizeAction = new Action_Resize(m_selectionContainer.Selection[0]);
            }
        }

        void Resizer_End(Gwen.Control.Base sender, EventArgs arguments)
        {
            if (m_resizeAction != null)
            {
                m_resizeAction.EndResizing(m_selectionContainer.Resizer.Bounds);
                m_operHistory.PushAction(m_resizeAction);
                m_resizeAction = null;
            }
        }

        void Resizer_Resized(Gwen.Control.Base sender, EventArgs arguments)
        {
            if (m_resizeAction != null)
            {
                m_resizeAction.UpdateResizing(m_selectionContainer.Resizer.Bounds);
            }
        }
    }
}
