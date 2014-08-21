using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ui_lib.Elements;

namespace ui_lib
{
    /// <summary>
    /// 渲染上下文
    /// 渲染时每帧的状态信息，每帧开始时重置
    /// </summary>
    public class RenderContext
    {
        public virtual void Reset()
        {
            m_accumTranslate.X = 0;
            m_accumTranslate.Y = 0;
        }

        public Point m_accumTranslate = new Point(0, 0);
    }

    /// <summary>
    /// 渲染器
    /// 用于提供各种控件的实际渲染逻辑。只有行为，没有状态。
    /// </summary>
    public interface RenderDevice
    {
        void RenderNode(Node node, RenderContext rc);
    }

    public class RenderSystem
    {
        public void Render(Node root, RenderContext rc, RenderDevice rs)
        {
            rc.Reset();
            RenderNodeRecursively(root, rc, rs);
        }

        private void RenderNodeRecursively(Node n, RenderContext rc, RenderDevice rs)
        {
            if (n == null)
                return;

            if (!n.Visible) // 'Visible == false' would hide all children 
                return;

            rs.RenderNode(n, rc);
            rc.m_accumTranslate.X += n.Position.X;
            rc.m_accumTranslate.Y += n.Position.Y;
            n.TraverseChildren((Node child) => { RenderNodeRecursively(child, rc, rs); });
            rc.m_accumTranslate.X -= n.Position.X;
            rc.m_accumTranslate.Y -= n.Position.Y;
        }
    }
}
