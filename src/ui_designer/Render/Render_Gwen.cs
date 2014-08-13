using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Elements;

namespace ui_designer
{
    public class GwenRenderContext : RenderContext
    {
        public GwenRenderContext(Gwen.Control.Canvas canvas, Gwen.Renderer.Tao renderer) 
        {
            m_canvas = canvas;
            m_renderer = renderer;
        }

        public Gwen.Control.Canvas m_canvas;
        public Gwen.Renderer.Tao m_renderer;
    }

    public class GwenRenderDevice : RenderDevice
    {
        public void RenderNode(Node node, RenderContext rc)
        {
            GwenRenderContext grc = rc as GwenRenderContext;
            if (grc == null)
                return;

            Rectangle rect = node.GetBounds();
            rect.Offset(grc.m_accumTranslate);
            grc.m_renderer.DrawLinedRect(rect);
        }
    }
}
