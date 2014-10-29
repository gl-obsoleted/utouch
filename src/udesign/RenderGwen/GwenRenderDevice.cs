using Gwen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ucore;
using udesign;
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
                return ucore.Const.INVALID_RECT;

            return grc.Renderer.ClipRegion;
        }

        public void SetCurrentClip(RenderContext rc, Rectangle clip)
        {
            GwenRenderContext grc = rc as GwenRenderContext;
            if (grc == null)
                return;

            grc.Renderer.ClipRegion = clip;
            if (grc.Renderer.ClipRegion != ucore.Const.INVALID_RECT)
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
            if (tri == null) // 找不到贴图的话，正常的处理应该用一个显眼的错误图案，这里暂时先忽略，待补充
                return;

            // 处理非九宫格图像渲染
            if (tri.uv9Grid == null)
            {
                grc.Renderer.DrawTexturedRect(tri.texture, rect, tri.u1, tri.v1, tri.u2, tri.v2);
                return;
            }

            // 九宫格渲染
            bool hori = tri.uv9Grid.IsHoriStreched();
            bool vert = tri.uv9Grid.IsVertStreched();
            if (hori && vert)
            {
                // ========== 完全九宫格 ===========

                // 先画四个角
                Rectangle upperLeft = rect;
                upperLeft.Width = tri.uv9Grid.GetLeft(rect.Width);
                upperLeft.Height = tri.uv9Grid.GetTop(rect.Height);
                grc.Renderer.DrawTexturedRect(tri.texture, upperLeft, tri.u1, tri.v1, tri.uv9Grid.uLeft, tri.uv9Grid.vTop);
                Rectangle upperRight = rect;
                upperRight.Width = tri.uv9Grid.GetRight(rect.Width);
                upperRight.Height = tri.uv9Grid.GetTop(rect.Height);
                upperRight.X = rect.Right - upperRight.Width;
                grc.Renderer.DrawTexturedRect(tri.texture, upperRight, tri.uv9Grid.uRight, tri.v1, tri.u2, tri.uv9Grid.vTop);

                Rectangle lowerLeft = rect;
                lowerLeft.Width = tri.uv9Grid.GetLeft(rect.Width);
                lowerLeft.Height = tri.uv9Grid.GetBottom(rect.Height);
                lowerLeft.Y = rect.Bottom - lowerLeft.Height;
                grc.Renderer.DrawTexturedRect(tri.texture, lowerLeft, tri.u1, tri.uv9Grid.vBottom, tri.uv9Grid.uLeft, tri.v2);
                Rectangle lowerRight = rect;
                lowerRight.Width = tri.uv9Grid.GetRight(rect.Width);
                lowerRight.Height = tri.uv9Grid.GetBottom(rect.Height);
                lowerRight.X = rect.Right - lowerRight.Width;
                lowerRight.Y = rect.Bottom - lowerRight.Height;
                grc.Renderer.DrawTexturedRect(tri.texture, lowerRight, tri.uv9Grid.uRight, tri.uv9Grid.vBottom, tri.u2, tri.v2);

                // 再画四个方向上的拉伸部分
                if (tri.uv9Grid.HasHoriStrechArea(rect.Width))
                {
                    grc.Renderer.DrawTexturedRect(tri.texture,
                        new Rectangle(rect.X + upperLeft.Width, rect.Y, rect.Width - upperLeft.Width - upperRight.Width, tri.uv9Grid.GetTop(rect.Height)),
                        tri.uv9Grid.uLeft, tri.v1, tri.uv9Grid.uRight, tri.uv9Grid.vTop);
                    grc.Renderer.DrawTexturedRect(tri.texture,
                        new Rectangle(rect.X + upperLeft.Width, rect.Bottom - lowerRight.Height, rect.Width - upperLeft.Width - upperRight.Width, lowerRight.Height),
                        tri.uv9Grid.uLeft, tri.uv9Grid.vBottom, tri.uv9Grid.uRight, tri.v2);
                }

                if (tri.uv9Grid.HasVertStrechArea(rect.Height))
                {
                    grc.Renderer.DrawTexturedRect(tri.texture,
                        new Rectangle(rect.X, rect.Y + upperLeft.Height, upperLeft.Width, rect.Height - upperLeft.Height - lowerLeft.Height),
                        tri.u1, tri.uv9Grid.vTop, tri.uv9Grid.uLeft, tri.uv9Grid.vBottom);
                    grc.Renderer.DrawTexturedRect(tri.texture,
                        new Rectangle(rect.Right - lowerRight.Width, rect.Y + upperLeft.Height, lowerRight.Width, rect.Height - upperRight.Height - lowerRight.Height),
                        tri.uv9Grid.uRight, tri.uv9Grid.vTop, tri.u2, tri.uv9Grid.vBottom);
                }
                
                // 最后画中央区域拉伸部分
                if (tri.uv9Grid.HasHoriStrechArea(rect.Width) && tri.uv9Grid.HasVertStrechArea(rect.Height))
                {
                    grc.Renderer.DrawTexturedRect(tri.texture,
                        new Rectangle(rect.X + upperLeft.Width,
                            rect.Y + upperLeft.Height,
                            rect.Width - upperLeft.Width - upperRight.Width, 
                            rect.Height - upperRight.Height - lowerRight.Height),
                        tri.uv9Grid.uLeft, tri.uv9Grid.vTop, tri.uv9Grid.uRight, tri.uv9Grid.vBottom);
                }
            }
            else if (hori)
            {
                // ========== 横向九宫格 ===========

                // 先画左右两端
                Rectangle leftPart = rect;
                leftPart.Width = tri.uv9Grid.GetLeft(rect.Width);
                grc.Renderer.DrawTexturedRect(tri.texture, leftPart, tri.u1, tri.v1, tri.uv9Grid.uLeft, tri.v2);
                Rectangle rightPart = rect;
                rightPart.Width = tri.uv9Grid.GetRight(rect.Width);
                rightPart.X = rect.Right - rightPart.Width;
                grc.Renderer.DrawTexturedRect(tri.texture, rightPart, tri.uv9Grid.uRight, tri.v1, tri.u2, tri.v2);

                // 再画中间拉伸部分
                if (tri.uv9Grid.HasHoriStrechArea(rect.Width))
                {
                    Rectangle middlePart = rect;
                    middlePart.Width = rect.Width - leftPart.Width - rightPart.Width;
                    middlePart.X = rect.X + leftPart.Width;
                    grc.Renderer.DrawTexturedRect(tri.texture, middlePart, tri.uv9Grid.uLeft, tri.v1, tri.uv9Grid.uRight, tri.v2);
                }
            }
            else if (vert)
            {
                // ========== 纵向九宫格 ===========

                // 先画上下两端
                Rectangle topPart = rect;
                topPart.Height = tri.uv9Grid.GetTop(rect.Height);
                grc.Renderer.DrawTexturedRect(tri.texture, topPart, tri.u1, tri.v1, tri.u2, tri.uv9Grid.vTop);
                Rectangle bottomPart = rect;
                bottomPart.Height = tri.uv9Grid.GetBottom(rect.Height);
                bottomPart.Y = rect.Bottom - bottomPart.Height;
                grc.Renderer.DrawTexturedRect(tri.texture, bottomPart, tri.u1, tri.uv9Grid.vBottom, tri.u2, tri.v2);

                // 再画中间拉伸部分
                if (tri.uv9Grid.HasVertStrechArea(rect.Height))
                {
                    Rectangle middlePart = rect;
                    middlePart.Height = rect.Height - topPart.Height - bottomPart.Height;
                    middlePart.Y = rect.Y + topPart.Height;
                    grc.Renderer.DrawTexturedRect(tri.texture, middlePart, tri.u1, tri.uv9Grid.vTop, tri.u2, tri.uv9Grid.vBottom);
                }
            }
        }
    }
}
