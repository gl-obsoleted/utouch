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
                rect.Inflate(5, 5);

                Color c = renderer.DrawColor;
                if (NodeSGUtil.HasLockedLayoutParent(n))
                {
                    renderer.DrawColor = Color.Red;
                }
                else
                {
                    renderer.DrawColor = Color.RosyBrown;
                }

                renderer.DrawLinedRect(rect);
                renderer.RenderText(ctx.m_font, new Point(rect.Left, rect.Top - 18), n.Name + " [" + n.GetType().Name + "]");
                
                if (NodeSGUtil.HasLockedLayoutParent(n))
                {
                    renderer.RenderText(ctx.m_font, new Point(rect.Left, rect.Top - 36), "(不可拖动)");
                }
                
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
