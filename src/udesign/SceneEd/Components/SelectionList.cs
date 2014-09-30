using Gwen.ControlInternal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib.Elements;

namespace udesign
{
    public class ResizeBox : Gwen.Control.ResizableControl
    {
        public ResizeBox(Gwen.Control.Canvas canvas)
            : base(canvas)
        {
            // 这个设置仅仅让 ResizeBox 本身对鼠标透明，这样用户可以移动下面的控件， 而不影响下面的 10 个 resizer 响应鼠标消息
            MouseInputEnabled = false;
        }

        public Resizer GetInternalResizer(int i)
        {
            return GetResizer(i);
        }

        public void RenderBound(Gwen.Renderer.Tao renderer)
        {
            Color c = renderer.DrawColor;
            renderer.DrawColor = Color.White;
            GwenUtil.RenderWorldBound(renderer, this, false);

            for (int i = 0; i < 10; i++)
            {
                Resizer r = GetResizer(i);
                if (r == null)
                    continue;

                GwenUtil.RenderWorldBound(renderer, r, r.IsHovered);
            }

            renderer.DrawColor = c;
        }

        public bool IsHoveringResizers()
        {
            for (int i = 0; i < 10; i++)
            {
                Resizer r = GetResizer(i);
                if (r != null && r.IsHovered)
                    return true;
            }

            return false;
        }
    }

    public partial class SelectionList
    {
        public List<Node> Selection { get { return m_selection; } }

        public ResizeBox Resizer { get { return m_resizeCtrl; } }

        /// <summary>
        /// 是否正在操作滑动面板（为了一致性考虑，（作为例外），滑动操作时，即使鼠标在外部，仍渲染选中对象）
        /// </summary>
        public bool IsScrolling { get; set; }

        public void Init(Gwen.Control.Canvas canvas)
        {
            IsScrolling = false;

            m_resizeCtrl = new ResizeBox(canvas);
            m_resizeCtrl.Hide();

            SceneEdEventNotifier.Instance.RefreshScene += OnSceneRefreshed;

            OnSelectionChanged();
        }

        void OnSceneRefreshed(RefreshSceneOpt opts)
        {
            RefreshSelection();
        }

        public void Render(GwenRenderContext ctx)
        {
            // 把渲染细节移动到以 _Render 为后缀的一份单独的代码文件
            RenderInternal(ctx);
        }

        public void OnSelectionChanged()
        {
            if (m_resizeCtrl != null)
            {
                // 为了简化问题，目前仅支持单个控件的缩放
                if (m_selection.Count == 1)
                {
                    m_resizeCtrl.Show();
                    RefreshSelection();
                }
                else
                {
                    m_resizeCtrl.Hide();
                }
            }
        }

        public void ClearSelection()
        {
            m_selection.Clear();

            OnSelectionChanged();

            SceneEdEventNotifier.Instance.Emit_SelectNode(null, this);
            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
        }

        public bool HasSelectedRoot()
        {
            foreach (Node n in Selection)
            {
                if (n.Parent == null)
                    return true;
            }
            return false;
        }

        public bool IsSelectionDraggable()
        {
            foreach (var n in m_selection)
            {
                if (n is RootNode)
                {
                    RootNode r = n as RootNode;
                    if (r != null && r.IsFullscren)
                    {
                        return false;
                    }
                }
                else if (NodeSGUtil.HasLockedLayoutParent(n))
                {
                    return false;
                }
            }

            return true;
        }

        public void RefreshSelection()
        {
            if (m_resizeCtrl != null)
            {
                Rectangle rect = GetSelectionWorldBounds();
                m_resizeCtrl.SetPosition(rect.Left, rect.Top);
                m_resizeCtrl.SetSize(rect.Width, rect.Height);
            }
        }

        private Rectangle GetSelectionWorldBounds()
        {
            Rectangle rect = Rectangle.Empty;
            foreach (var n in m_selection)
            {
                if (rect.IsEmpty)
                {
                    rect = n.GetWorldBounds();
                }
                else
                {
                    rect = Rectangle.Union(rect, n.GetWorldBounds());
                }
            }
            return rect;
        }

        private List<Node> m_selection = new List<Node>();

        private ResizeBox m_resizeCtrl;
    }
}
