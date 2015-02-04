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

        public SelectionList Selection { get { return m_selectionList; } }
        public bool HasSelection { get { return Selection.Selection.Count != 0; } }

        public ActionQueue OperHistory { get { return m_operHistory; } }
        public DragAndDropReceiver DragAndDrop { get { return m_dragAndDropReceiver; } }
        public SceneClipboard Clipboard { get { return m_clipboard; } }

        public void Render(GwenRenderContext ctx)
        {
            // 把当前的设计时分辨率画下来
            {
                int w = Scene.Instance.DesignTimeResolution.width;
                int h = Scene.Instance.DesignTimeResolution.height;
                Color c = ctx.Renderer.DrawColor;
                ctx.Renderer.DrawColor = Color.DarkGreen;
                ctx.Renderer.DrawLinedRect(new Rectangle(0, 0, w, h));
                ctx.Renderer.RenderText(ctx.Font, new Point(w + 10, h / 2), h.ToString());
                ctx.Renderer.RenderText(ctx.Font, new Point(w / 2 - 30, h), w.ToString());
                ctx.Renderer.DrawColor = c;
            }

            m_dragAndDropReceiver.Render(ctx);
            m_selectionList.Render(ctx);
        }

        public void MouseDown(MouseButtons mouseButton, Point mouseLocation)
        {
            Node pickedNode = Scene.Instance.Pick(mouseLocation);
            switch (mouseButton)
            {
                case MouseButtons.Left:
                    if (m_selectionList.Selection.Contains(pickedNode) && m_selectionList.IsSelectionDraggable())
                    {
                        DragLeft_BeginMoving(mouseLocation);
                    }
                    break;
                case MouseButtons.Right:
                    if (m_selectionList.Selection.Contains(pickedNode) && m_selectionList.Selection.Count == 1 && pickedNode.IsScrollable())
                    {
                        DragRight_BeginScrolling(mouseLocation);
                    }
                    break;
                default:
                    break;
            }
        }

        public void MouseMove(Point mouseLocation)
        {
            if (IsDraggingLeft())
            {
                DragLeft_UpdateMoving(mouseLocation);
            }
            else if (IsDragRightScrolling())
            {
                DragRight_UpdateScrolling(mouseLocation);
            }
        }

        public void MouseUp(MouseButtons mouseButton, Point mouseLocation)
        {
            switch (mouseButton)
            {
                case MouseButtons.Left:
                    if (IsDraggingLeft())
                    {
                        DragLeft_EndMoving(mouseLocation);
                    }
                    else
                    {
                        Node n = Scene.Instance.Pick(mouseLocation);
                        Select(n);
                    }
                    break;
                case MouseButtons.Right:
                    if (IsDragRightScrolling())
                    {
                        DragRight_EndScrolling(mouseLocation);
                    }
                    break;
                default:
                    break;
            }
            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
        }

        public void Select(Node n)
        {
            if (!HasModifierKeyDown(Keys.Control))
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

        public void Select(List<Node> nodes)
        {
            if (nodes.Count == 0)
                return;

            m_selectionList.Selection.Clear();
            foreach (var item in nodes)
            {
                m_selectionList.Selection.Add(item);
                SceneEdEventNotifier.Instance.Emit_SelectNode(item, this);
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
                ActionQueue.Instance.PushAction(new Action_Delete(m_selectionList.Selection));
                m_selectionList.ClearSelection();
            }
        }

        private ActionQueue m_operHistory = new ActionQueue();
        private SelectionList m_selectionList = new SelectionList();
        private DragAndDropReceiver m_dragAndDropReceiver = new DragAndDropReceiver();
        private SceneClipboard m_clipboard = new SceneClipboard();

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

        public void Cut()
        {
            if (HasSelection)
            {
                Clipboard.SetClippedContent(m_selectionList.Selection, true);
            }
        }

        public void Copy()
        {
            if (HasSelection)
            {
                Clipboard.SetClippedContent(m_selectionList.Selection, false);
            }
        }

        public void Paste()
        {
            if (HasSelection && Clipboard.IsInUse)
            {
                List<Node> newlyCreated = SceneEd.Instance.Clipboard.AttachTo(SceneEd.Instance.Selection.Selection[0]);
                SceneEd.Instance.Select(newlyCreated);
                SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
            }
        }

        public bool OnDirKeyPressed(Keys shortcutKey)
        {
            if (!m_selectionList.IsSelectionDraggable())
                return false;

            Point offset = ucore.Const.ZERO_POINT;
            switch (shortcutKey)
            {
                case Keys.Left:
                    offset = new Point(-1, 0);
                    break;
                case Keys.Right:
                    offset = new Point(1, 0);
                    break;
                case Keys.Up:
                    offset = new Point(0, -1);
                    break;
                case Keys.Down:
                    offset = new Point(0, 1);
                    break;
            }

            if (offset == ucore.Const.ZERO_POINT)
                return false;

            Action_Move act;
            act = new Action_Move(m_selectionList.Selection);
            act.EndUpdatePosition(offset);
            ActionQueue.Instance.PushAction(act);
            return true;
        }
    }
}
