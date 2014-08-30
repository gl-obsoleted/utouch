using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib.Elements;

namespace udesign
{
    public class OverlayRendering
    {
        public static void Render(Gwen.Renderer.Tao renderer, GwenRenderContext ctx)
        {
            foreach (Node n in SceneEd.Instance.Selection)
            {
                Rectangle rect = n.GetWorldBounds();

                Color c = renderer.DrawColor;

                renderer.DrawColor = Color.ForestGreen;
                renderer.DrawLinedRect(Rectangle.Inflate(rect, 5, 5));
                renderer.DrawColor = Color.LimeGreen;
                renderer.DrawLinedRect(Rectangle.Inflate(rect, 4, 4));
                renderer.DrawColor = Color.LightGreen;
                renderer.DrawLinedRect(Rectangle.Inflate(rect, 3, 3));
                renderer.DrawColor = Color.PaleGreen;
                renderer.DrawLinedRect(Rectangle.Inflate(rect, 2, 2));
                renderer.DrawColor = Color.Honeydew;
                renderer.DrawLinedRect(Rectangle.Inflate(rect, 1, 1));

                renderer.DrawColor = Color.White;
                renderer.RenderText(ctx.m_font, new Point(rect.Left - 1, rect.Top - 25 - 1), n.Name + " [" + n.GetType().Name + "]");
                
                renderer.DrawColor = c;
            }

            Node dragTarget = SceneEd.Instance.PossibleDraggingTarget;
            if (dragTarget != null)
            {
                Rectangle rect = dragTarget.GetWorldBounds();
                rect.Inflate(5, 5);

                Color c = renderer.DrawColor;
                renderer.DrawColor = Color.HotPink;
                renderer.DrawLinedRect(rect);
                renderer.RenderText(ctx.m_font,
                    new Point(rect.Left, rect.Top - 18),
                    "[目标节点] " + dragTarget.Name);
                renderer.DrawColor = c;
            }
        }
    }
}
