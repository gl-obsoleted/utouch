using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Elements;

namespace ui_designer
{
    public class GwenRenderContext : IRenderContext
    {
        public GwenRenderContext(Gwen.Control.Canvas canvas, Gwen.Renderer.Tao renderer) 
        {
            m_canvas = canvas;
            m_renderer = renderer;
        }

        public Gwen.Control.Canvas m_canvas;
        public Gwen.Renderer.Tao m_renderer;
    }

    public class GwenRenderSystem : IRenderSystem
    {
        public void RenderNode(Node node, IRenderContext rc, TransformContext tc)
        {
            GwenRenderContext grc = rc as GwenRenderContext;
            if (grc == null)
                return;

            grc.m_renderer.DrawLinedRect(node.GetBounds());
        }
    }
}
