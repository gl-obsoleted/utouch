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
                RenderSelectionBox(renderer, rect);
                renderer.DrawColor = Color.White;
                renderer.RenderText(ctx.m_font, new Point(rect.Right + 5, rect.Top), n.Name + " [" + n.GetType().Name + "]");
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

        static Color[] m_gradArray = { Color.Honeydew, Color.PaleGreen, Color.LightGreen, Color.LimeGreen, Color.ForestGreen };
        static void RenderSelectionBox(Gwen.Renderer.Tao renderer, Rectangle rect)
        {
            for (int i = 0; i < m_gradArray.Length; i++)
            {
                renderer.DrawColor = m_gradArray[i];
                renderer.DrawLinedRect(Rectangle.Inflate(rect, i + 1, i + 1));
            }
        }
    }
}
