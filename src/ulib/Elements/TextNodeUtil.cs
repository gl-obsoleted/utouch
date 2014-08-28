using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ulib.Elements
{
    public static class TextNodeUtil
    {
        /// <summary>
        /// 这个函数用于计算 TextNode 内部文字相对于控件原点的偏移
        /// 利用了 TextNode 的 Size，AlignH，AlignV 和使用对应 Renderer 测出的文本渲染所需占用的尺寸
        /// </summary>
        public static Point CalculateInternalTextOffset(TextNode textNode, Point measuredTextSize)
        {
            Point actualOffset = new Point(0, 0);

            if (textNode.Size == (Size)(measuredTextSize))
            {
                // the text just fits in the bound
                return actualOffset;
            }

            switch (textNode.AlignH)
            {
                case ulib.Base.AlignHori.Left:
                    actualOffset.X = 0;
                    break;
                case ulib.Base.AlignHori.Center:
                    actualOffset.X = textNode.Size.Width / 2 - measuredTextSize.X / 2;
                    break;
                case ulib.Base.AlignHori.Right:
                    actualOffset.X = textNode.Size.Width - measuredTextSize.X;
                    break;
                default:
                    break;
            }
            switch (textNode.AlignV)
            {
                case ulib.Base.AlignVert.Top:
                    actualOffset.Y = 0;
                    break;
                case ulib.Base.AlignVert.Middle:
                    actualOffset.Y = textNode.Size.Height / 2 - measuredTextSize.Y / 2;
                    break;
                case ulib.Base.AlignVert.Bottom:
                    actualOffset.Y = textNode.Size.Height - measuredTextSize.Y;
                    break;
                default:
                    break;
            }

            return actualOffset;
        }
    }
}
