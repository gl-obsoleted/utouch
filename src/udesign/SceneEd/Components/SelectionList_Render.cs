using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib.Base;
using ulib.Elements;

namespace udesign
{
    public partial class SelectionList
    {
        private void RenderInternal(GwenRenderContext ctx)
        {
            if (m_resizeCtrl != null && m_resizeCtrl.IsVisible)
            {
                if (GwenUtil.GetWorldBound(m_resizeCtrl).Contains(ctx.CurrentMousePos))
                {
                    m_resizeCtrl.RenderBound(ctx.Renderer);
                }
            }

            foreach (Node n in m_selection)
            {
                Rectangle rect = n.GetWorldBounds();
                Color c = ctx.Renderer.DrawColor;
                RenderSelectionBox(ctx.Renderer, rect);

                Point textSize = ctx.Renderer.MeasureText(ctx.Font, BaseUtil.PointToString(n.Position));

                ctx.Renderer.DrawColor = SceneEdConstants.SelectionCoordBgColor;
                ctx.Renderer.DrawFilledRect(new Rectangle(rect.X, rect.Y - textSize.Y - 10, textSize.X, textSize.Y));
                ctx.Renderer.DrawColor = SceneEdConstants.SelectionCoordTextColor;
                ctx.Renderer.RenderText(ctx.Font, new Point(rect.X, rect.Y - textSize.Y - 10), BaseUtil.PointToString(n.Position));

                if (rect.Contains(ctx.CurrentMousePos) || IsScrolling)
                {
                    RenderHoveringSelectedNode(ctx.Renderer, ctx, n);
                }

                ctx.Renderer.DrawColor = c;
            }
        }

        void RenderSelectionBox(Gwen.Renderer.Tao renderer, Rectangle rect)
        {
            for (int i = 0; i < SceneEdConstants.SelectionBoxGradientColors.Length; i++)
            {
                renderer.DrawColor = SceneEdConstants.SelectionBoxGradientColors[i];
                renderer.DrawLinedRect(Rectangle.Inflate(rect, i + 1, i + 1));
            }
        }

        void RenderHoveringSelectedNode(Gwen.Renderer.Tao renderer, GwenRenderContext ctx, Node hoveringNode)
        {
            renderer.DrawColor = SceneEdConstants.SelectionDescTextColor;
            Rectangle rect = hoveringNode.GetWorldBounds();
            int interval = SceneEdConstants.TextLineInterval;

            {
                string title = string.Format("{0} [{1}]", hoveringNode.Name, hoveringNode.GetType().Name);
                Point titleSize = renderer.MeasureText(ctx.Font, title);
                Point widthTextSize = renderer.MeasureText(ctx.Font, rect.Width.ToString());

                int occupiedHeight = titleSize.Y + widthTextSize.Y + interval * 2;
                int titleX = rect.X + rect.Width / 2 - titleSize.X / 2;
                int titleY = rect.Top < occupiedHeight ? rect.Top + occupiedHeight - widthTextSize.Y : rect.Top - occupiedHeight;
                int widthTextX = rect.X + rect.Width / 2 - widthTextSize.X / 2;
                int widthTextY = rect.Top < occupiedHeight ? rect.Top + interval : rect.Top - interval - widthTextSize.Y;
                
                renderer.RenderText(ctx.Font, new Point(titleX, titleY), title);
                renderer.RenderText(ctx.Font, new Point(widthTextX, widthTextY), rect.Width.ToString());
                renderer.DrawFilledRect(new Rectangle(rect.X, widthTextY + widthTextSize.Y / 2, widthTextX - rect.X, 1));
                renderer.DrawFilledRect(new Rectangle(widthTextX + widthTextSize.X, widthTextY + widthTextSize.Y / 2, widthTextX - rect.X, 1));
            }

            {
                Point heightTextSize = renderer.MeasureText(ctx.Font, rect.Height.ToString());
                int heightTextX = rect.Left < heightTextSize.X ? rect.Left : rect.Left - heightTextSize.X;
                int heightTextY = rect.Top + rect.Height / 2 - heightTextSize.Y / 2;

                renderer.RenderText(ctx.Font, new Point(heightTextX, heightTextY), rect.Height.ToString());
                renderer.DrawFilledRect(new Rectangle(rect.X - heightTextSize.X / 2, rect.Top, 1, rect.Height / 2 - heightTextSize.Y / 2));
                renderer.DrawFilledRect(new Rectangle(rect.X - heightTextSize.X / 2, heightTextY + heightTextSize.Y, 1, rect.Height / 2 - heightTextSize.Y / 2));
            }

            int thickness = SceneEdConstants.SlideBarThickness;
            if (hoveringNode.IsScrollableH())
            {
                float range = (float)(hoveringNode.LogicalSize.Width - hoveringNode.Size.Width);
                int slideWidth = (int)((float)rect.Width * (float)(hoveringNode.Size.Width) / (float)(hoveringNode.LogicalSize.Width));
                int slideOffset = (int)((float)(rect.Width - slideWidth) * (float)hoveringNode.CurrentScrollOffset.X / range);

                renderer.DrawColor = SceneEdConstants.SlideBarBackground;
                renderer.DrawFilledRect(new Rectangle(rect.X, rect.Bottom, rect.Width, thickness));
                renderer.DrawColor = IsScrolling ? SceneEdConstants.SelectionHighlightColor : SceneEdConstants.SlideBarForeground;
                renderer.DrawFilledRect(new Rectangle(rect.X + slideOffset, rect.Bottom, slideWidth, thickness));
            }
            if (hoveringNode.IsScrollableV())
            {
                float range = (float)(hoveringNode.LogicalSize.Height - hoveringNode.Size.Height);
                int slideHeight = (int)((float)rect.Height * (float)(hoveringNode.Size.Height) / (float)(hoveringNode.LogicalSize.Height));
                int slideOffset = (int)((float)(rect.Height - slideHeight) * (float)hoveringNode.CurrentScrollOffset.Y / range);

                renderer.DrawColor = SceneEdConstants.SlideBarBackground;
                renderer.DrawFilledRect(new Rectangle(rect.Right, rect.Top, thickness, rect.Height));
                renderer.DrawColor = IsScrolling ? SceneEdConstants.SelectionHighlightColor : SceneEdConstants.SlideBarForeground;
                renderer.DrawFilledRect(new Rectangle(rect.Right, rect.Top + slideOffset, thickness, slideHeight));
            }
        }
    }
}
