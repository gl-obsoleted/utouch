using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ucore;
using ulib;

namespace udesign
{
    public class GwenRenderContext : RenderContext
    {
        public GwenRenderContext(Gwen.Control.Canvas canvas, Gwen.Renderer.Tao renderer)
        {
            Canvas = canvas;
            Renderer = renderer;
            Font = new Gwen.Font(renderer);
        }

        public Gwen.Control.Canvas Canvas { get; set; }
        public Gwen.Renderer.Tao Renderer { get; set; }
        public Gwen.Font Font { get; set; }
        public Point CurrentMousePos { get; set; }
        public OrthoTransform CurrentOrthoTransform { get; set; }
    }
}
