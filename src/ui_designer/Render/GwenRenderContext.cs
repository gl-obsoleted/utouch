using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_designer
{
    public class GwenRenderContext : RenderContext
    {
        public GwenRenderContext(Gwen.Control.Canvas canvas, Gwen.Renderer.Tao renderer)
        {
            m_canvas = canvas;
            m_renderer = renderer;
            m_font = new Gwen.Font(renderer);
        }

        public Gwen.Control.Canvas m_canvas;
        public Gwen.Renderer.Tao m_renderer;
        public Gwen.Font m_font;
    }
}
