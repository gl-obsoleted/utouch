using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Elements;

namespace ui_designer
{
    public partial class Scene
    {
        public Scene()
        {
            m_root = new Node();
            m_root.Position = new Point(300, 300);

            m_tc = new TransformContext();
        }

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

            m_root.TraverseChildren((Node child) => { RenderNodeRecursively(child, rc, rs, tc); });
        }

        private Node m_root;
        private TransformContext m_tc;
    }
}
