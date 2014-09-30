using Gwen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using udesign.Render;
using ulib;
using ulib.Controls;
using ulib.Elements;

namespace udesign
{
    public class GwenRenderDevice : RenderDevice
    {
        public void RenderNode(Node node, RenderContext rc)
        {
            GwenRenderContext grc = rc as GwenRenderContext;
            if (grc == null)
                return;

            Rectangle worldBounds = node.GetWorldBounds();
            if (node is ImageNode)
            {
                RenderImageNode(node as ImageNode, grc);
            }
            else if (node is TextNode)
            {
                RenderTextNode(node as TextNode, grc);
            }
            else if (node is Button)
            {
                RenderButton(node as Button, grc);
            }
            else if (node is CheckBox)
            {
                RenderCheckBox(node as CheckBox, grc);
            }
            else
            {
                // defualt method for rendering an unknown node
                grc.Renderer.DrawLinedRect(worldBounds);
            }
        }

        public Rectangle GetCurrentClip(RenderContext rc)
        {
            GwenRenderContext grc = rc as GwenRenderContext;
            if (grc == null)
                return ulib.Base.Constants.INVALID_RECT;

            return grc.Renderer.ClipRegion;
        }

        public void SetCurrentClip(RenderContext rc, Rectangle clip)
        {
            GwenRenderContext grc = rc as GwenRenderContext;
            if (grc == null)
                return;

            grc.Renderer.ClipRegion = clip;
            if (grc.Renderer.ClipRegion != ulib.Base.Constants.INVALID_RECT)
            {
                grc.Renderer.StartClip();
            }
            else
            {
                grc.Renderer.EndClip();
            }
        }

        private void RenderImageNode(ImageNode imageNode, GwenRenderContext grc)
        {
            DrawImage(grc, new Rectangle(grc.GetAccumulatedDockedScrolledTranslate(), imageNode.Size), imageNode.Res);
        }

        private void RenderTextNode(TextNode textNode, GwenRenderContext grc)
        {
            // 当需要的时候，先更新 TextNode 的尺寸
            Point textSize = grc.Renderer.MeasureText(grc.Font, textNode.Text);
            if (textNode.RequestedSizeRefreshing)
            {
                textNode.Size = new Size(
                    Math.Max(textNode.Size.Width, textSize.X),
                    Math.Max(textNode.Size.Height, textSize.Y));
                NodeSGUtil.ClampBounds(textNode);
                textNode.RequestedSizeRefreshing = false;
            }

            Point loc = grc.GetAccumulatedDockedScrolledTranslate();

            // 理论上这里我们不应当每帧对每段 Text 都调用 MeasureText()
            // 不过考察 Gwen.Renderer.Tao.MeasureText() 后我们发现 
            // 其内部已经 Cache 了一份，把尺寸再缓存一份意义不大
            Point internalOffset = TextNodeUtil.CalculateInternalTextOffset(textNode, textSize);
            loc.Offset(internalOffset);

            Color c = grc.Renderer.DrawColor;
            grc.Renderer.DrawColor = textNode.TextColor;
            grc.Renderer.RenderText(grc.Font, loc, textNode.Text);
            grc.Renderer.DrawColor = c;
        }

        private void RenderButton(Button bt, GwenRenderContext grc)
        {
            DrawImage(grc, new Rectangle(grc.GetAccumulatedDockedScrolledTranslate(), bt.Size), bt.Res_Background);
        }

        private void RenderCheckBox(CheckBox cb, GwenRenderContext grc)
        {
            Rectangle rect = new Rectangle(grc.GetAccumulatedDockedScrolledTranslate(), cb.MarkSize);
            DrawImage(grc, rect, cb.Res_Background);
            DrawImage(grc, rect, cb.Res_Mark);
        }

        private void DrawImage(GwenRenderContext grc, Rectangle rect, string url)
        {
            TextureRenderInfo tri = GwenTextureProvider.Instance.GetTextureRenderInfo(grc.Renderer, url);
            if (tri != null) // 找不到贴图的话，正常的处理应该用一个显眼的错误图案，这里暂时先忽略，待补充
            {
                grc.Renderer.DrawTexturedRect(tri.texture, rect,
                    tri.u1,
                    tri.v1,
                    tri.u2,
                    tri.v2);
            }
        }
    }
}
