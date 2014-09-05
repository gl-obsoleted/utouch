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
        public SelectionList Selection { get { return m_selectionList; } }

        public void Render(Gwen.Renderer.Tao renderer, GwenRenderContext ctx)
        {
            Node dragTarget = PossibleDraggingTarget;
            if (dragTarget != null)
            {
                Rectangle rect = dragTarget.GetWorldBounds();
                rect.Inflate(5, 5);

                Color c = renderer.DrawColor;
                renderer.DrawColor = Color.HotPink;
                renderer.DrawLinedRect(rect);
                renderer.RenderText(ctx.m_font,
                    new Point(rect.Left, rect.Top - 18),
                    "[目标节点] " + dragTarget.Name);
                renderer.DrawColor = c;
            }

            m_selectionList.Render(renderer, ctx);
        }

        bool m_isLeftDown = false;
        Point m_beginDragPos = new Point(0, 0);
        Action_Move m_dragAction;
        public void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_isLeftDown = true;

                Node n = Scene.Instance.Pick(e.Location);
                if (m_selectionList.Selection.Contains(n) && m_selectionList.IsSelectionDraggable())
                {
                    // 到这里触发拖拽
                    m_beginDragPos = e.Location;
                    m_dragAction = new Action_Move(m_selectionList.Selection);
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
                m_selectionList.Selection.Clear();
            }
            if (n != null)
            {
                m_selectionList.Selection.Add(n);
                SceneEdEventNotifier.Instance.Emit_SelectNode(n, this);
            }
            else
            {
                SceneEdEventNotifier.Instance.Emit_SelectNode(null, this);
            }

            m_selectionList.OnSelectionChanged();
            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_Rendering);
        }

        public void DeleteSelected()
        {
            if (m_selectionList.HasSelectedRoot())
            {
                Session.Message("无法删除根节点。");                
            }
            else
            {
                m_operHistory.PushAction(new Action_Delete(m_selectionList.Selection));
                m_selectionList.ClearSelection();
            }
        }

        private OperationHistory m_operHistory = new OperationHistory();
        private SelectionList m_selectionList = new SelectionList();

        internal void InitSelectionContainer(Gwen.Control.Canvas canvas)
        {
            m_selectionList.Init(canvas);
            m_selectionList.Resizer.Resized += Resizer_Resized;
            for (int i = 0; i < 10; i++)
            {
                Resizer r = m_selectionList.Resizer.GetInternalResizer(i);
                if (r == null)
                    continue;

                r.BeginDrag += Resizer_Begin;
                r.EndDrag += Resizer_End;
            }
        }

        Action_Resize m_resizeAction;

        void Resizer_Begin(Gwen.Control.Base sender, EventArgs arguments)
        {
            if (m_selectionList.Resizer.IsHoveringResizers() && m_selectionList.Selection.Count == 1)
            {
                m_resizeAction = new Action_Resize(m_selectionList.Selection[0]);
            }
        }

        void Resizer_End(Gwen.Control.Base sender, EventArgs arguments)
        {
            if (m_resizeAction != null)
            {
                m_resizeAction.EndResizing(m_selectionList.Resizer.Bounds);
                m_operHistory.PushAction(m_resizeAction);
                m_resizeAction = null;
            }
        }

        void Resizer_Resized(Gwen.Control.Base sender, EventArgs arguments)
        {
            if (m_resizeAction != null)
            {
                m_resizeAction.UpdateResizing(m_selectionList.Resizer.Bounds);
            }
        }
    }
}
