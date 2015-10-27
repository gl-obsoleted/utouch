using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace udesign
{
    public class ImageUtil
    {
        public static Image ScaleImageAsClamped(Image sourceImage, Size targetClampSize)
        {
            // 判断是否需要缩放
            float widthScale = (float)targetClampSize.Width / (float)sourceImage.Size.Width;
            float heightScale = (float)targetClampSize.Height / (float)sourceImage.Size.Height;
            if (widthScale >= 1.0f && heightScale >= 1.0f)
                return sourceImage;

            // 缩放
            float downScale = System.Math.Min(widthScale, heightScale);
            float scaledWidth = System.Math.Max((float)sourceImage.Size.Width * downScale, 1.0f);
            float scaledHeight = System.Math.Max((float)sourceImage.Size.Height * downScale, 1.0f);
            return new Bitmap(sourceImage, new Size((int)scaledWidth, (int)scaledHeight));
        }
    }
}
