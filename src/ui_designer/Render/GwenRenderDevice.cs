using Gwen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_designer.Render;
using ui_lib.Elements;

namespace ui_designer
{
    public class GwenRenderDevice : RenderDevice
    {
        public void RenderNode(Node node, RenderContext rc)
        {
            GwenRenderContext grc = rc as GwenRenderContext;
            if (grc == null)
                return;

            if (node is ImageNode)
            {
                RenderImageNode(node as ImageNode, grc);
            }
            else if (node is TextNode)
            {
                RenderTextNode(node as TextNode, grc);
            }
            else
            {
                Rectangle rect = node.GetBounds();
                rect.Offset(grc.m_accumTranslate);
                grc.m_renderer.DrawLinedRect(rect);
            }
        }

        private void RenderImageNode(ImageNode imageNode, GwenRenderContext grc)
        {
            TextureRenderInfo tri = GwenTextureProvider.Instance.GetTextureRenderInfo(grc.m_renderer, imageNode.ResLocation);
            if (tri != null) // 找不到贴图的话，正常的处理应该用一个显眼的错误图案，这里暂时先忽略，待补充
            {
                Rectangle rect = imageNode.GetBounds();
                rect.Offset(grc.m_accumTranslate);
                grc.m_renderer.DrawTexturedRect(tri.texture, rect,
                    tri.u1,
                    tri.v1,
                    tri.u2,
                    tri.v2);
            }
        }

        private void RenderTextNode(TextNode textNode, GwenRenderContext grc)
        {
            Point loc = textNode.Position;
            loc.Offset(grc.m_accumTranslate);

            Color c = grc.m_renderer.DrawColor;
            grc.m_renderer.DrawColor = textNode.Color;
            grc.m_renderer.RenderText(grc.m_font, loc, textNode.Text);
            grc.m_renderer.DrawColor = c;
        }
    }
}
