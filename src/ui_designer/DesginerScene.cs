using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Base;
using ui_lib.Elements;
using ui_lib.Widgets;

namespace ui_designer
{
    public partial class DesginerScene
    {
        public DesginerScene()
        {
            m_root = new RootNode();
            Node m_child = new Node();
            m_child.Position = new Point(50, 50);
            m_child.Size = new Size(50, 50);
            Node m_child2 = new Node();
            m_child2.Position = new Point(150, 50);
            m_child2.Size = new Size(50, 50);
            m_root.Attach(m_child);
            m_root.Attach(m_child2);

            m_tc = new TransformContext();
        }

        public RootNode GetRootNode() { return m_root; }

        public void Render(IRenderContext rc, IRenderSystem rs)
        {
            m_tc.Reset();
            RenderNodeRecursively(m_root, rc, rs, m_tc);
        }

        private void RenderNodeRecursively(Node n, IRenderContext rc, IRenderSystem rs, TransformContext tc)
        {
            if (n == null)
                return;

            if (!n.Visible) // 'Visible == false' would hide all children 
                return;

            rs.RenderNode(n, rc, tc);
            tc.m_accumTranslate.X += n.Position.X;
            tc.m_accumTranslate.Y += n.Position.Y;
            n.TraverseChildren((Node child) => { RenderNodeRecursively(child, rc, rs, tc); });
            tc.m_accumTranslate.X -= n.Position.X;
            tc.m_accumTranslate.Y -= n.Position.Y;
        }

        private RootNode m_root;
        private TransformContext m_tc;
    }
}
