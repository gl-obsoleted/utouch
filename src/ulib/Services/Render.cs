using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ulib.Elements;

namespace ulib
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
            m_localPosDocked.X = 0;
            m_localPosDocked.Y = 0;
        }

        // 在渲染指定节点时，通常需要同时考虑累计变换和本地 docking 和本地 scrolling 的影响
        public Point GetAccumulatedDockedScrolledTranslate()
        {
            return m_accumTranslate + (Size)m_localPosDocked + (Size)m_localPosScrolled;
        }

        // 在渲染指定节点时，通常需要同时考虑累计变换和本地 docking 的影响
        public Point GetAccumulatedDockedTranslate()
        {
            return m_accumTranslate + (Size)m_localPosDocked;
        }

        // 保存着从根节点到上一级父节点的累计变换
        public Point m_accumTranslate = new Point(0, 0);

        // 考虑了 docking 的影响后，该控件相对父节点的位置
        public Point m_localPosDocked = new Point(0, 0);

        // 考虑了 scrolling 的影响后，该控件相对父节点的位置
        public Point m_localPosScrolled = new Point(0, 0);
    }

    /// <summary>
    /// 渲染器
    /// 用于提供各种控件的实际渲染逻辑。只有行为，没有状态。
    /// </summary>
    public interface RenderDevice
    {
        void RenderNode(Node node, RenderContext rc);

        Rectangle GetCurrentClip(RenderContext rc);
        void SetCurrentClip(RenderContext rc, Rectangle clip);
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

            // 两个说明：
            //  1) m_localPosDocked 并非一个累积量，因此不需在使用完之后重置
            //  2) m_localPosScrolled 并非一个累积量，因此不需在使用完之后重置
            //  3) m_accumTranslate 只需计算一次，结果可供整个 Children 列表共享使用
            //
            // 上面这三点决定了 m_localPosDocked 和 m_localPosScrolled 作用于当前节点
            // 而 m_accumTranslate 作用于当前节点的 Children 列表

            Point localPosDocked = n.GetDockedPos();
            rc.m_localPosDocked = localPosDocked;

            Point localScrolled = Point.Empty - (Size)n.CurrentScrollOffset;
            rc.m_localPosScrolled = localScrolled;

            rs.RenderNode(n, rc);

            rc.m_accumTranslate.X += (localPosDocked.X + localScrolled.X);
            rc.m_accumTranslate.Y += (localPosDocked.Y + localScrolled.Y);

            Rectangle oldClip = rs.GetCurrentClip(rc);
            if (n.HasScrolled)
            {
                rs.SetCurrentClip(rc, n.GetWorldBounds());
            }

            n.TraverseChildren((Node child) => { RenderNodeRecursively(child, rc, rs); });
            
            if (n.HasScrolled)
            {
                rs.SetCurrentClip(rc, oldClip);
            }

            rc.m_accumTranslate.X -= (localPosDocked.X + localScrolled.X);
            rc.m_accumTranslate.Y -= (localPosDocked.Y + localScrolled.Y);
        }
    }
}
