using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib;

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

        public static Image GetAtlasThumbnail(Image sourceImage, ImageResource ir, Size targetClampSize)
        {
            // 创建 atlas 预览小图
            Bitmap bmp32bppArgb = new Bitmap(ir.Size.Width, ir.Size.Height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp32bppArgb))
            {
                g.DrawImage(sourceImage,
                    new Rectangle(0, 0, ir.Size.Width, ir.Size.Height),
                    new Rectangle(ir.Position.X, ir.Position.Y, ir.Size.Width, ir.Size.Height), GraphicsUnit.Pixel);
            }

            // 判断是否需要缩放
            float widthScale = (float)targetClampSize.Width / (float)bmp32bppArgb.Width;
            float heightScale = (float)targetClampSize.Height / (float)bmp32bppArgb.Height;
            if (widthScale >= 1.0f && heightScale >= 1.0f)
                return bmp32bppArgb;

            // 缩放
            float downScale = System.Math.Min(widthScale, heightScale);
            float scaledWidth = System.Math.Max((float)bmp32bppArgb.Width * downScale, 1.0f);
            float scaledHeight = System.Math.Max((float)bmp32bppArgb.Height * downScale, 1.0f);
            IntPtr ip = System.IntPtr.Zero;
            return bmp32bppArgb.GetThumbnailImage((int)scaledWidth, (int)scaledHeight, () => { return true; }, ip);
        }
    }
}
