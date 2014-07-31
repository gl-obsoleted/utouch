using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Elements;

namespace ui_designer
{
    public interface IRenderContext
    {
    }

    public interface IRenderSystem
    {
        void RenderNode(Node node, IRenderContext rc, TransformContext tc);
    }

    public class TransformContext
    {
        public void Reset()
        {
            m_accumTranslate.X = 0;
            m_accumTranslate.Y = 0;
        }

        public Point m_accumTranslate = new Point(0, 0);
    }
}
