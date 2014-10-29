using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ucore;
using ulib;
using ulib.Base;

namespace udesign
{
    /*
     * 一个标准的九宫格如下所示
     *   
     * border 的上下左右含义如下所示
     * 当实际渲染时，会将 Left <-> Right 之间部分， Top <-> Bottom 之间部分拉伸
     * 
     *     ----------------------
     *     |      |          |  |
     *     |      |          |  |
     *     |--------------------| (Top)
     *     |      |          |  |
     *     |      |          |  |
     *     |--------------------| (Bottom)
     *     |      |          |  |
     *     |      |          |  |
     *     ----------------------
     *         (Left)       (Right)   
     */
    public class Texture9GridBorderInfo
    {
        public float uLeft = 0.0f;
        public float vTop = 0.0f;
        public float uRight = 0.0f;
        public float vBottom = 0.0f;

        public Size size = new Size(0, 0);
        public Rectangle border = Const.ZERO_RECT;

        public bool IsHoriStreched() { return !EzMath.IsZero(uLeft) && !EzMath.IsZero(uRight); }
        public bool IsVertStreched() { return !EzMath.IsZero(vTop) && !EzMath.IsZero(vBottom); }

        public bool HasHoriStrechArea(int drawnWidth) { return drawnWidth > GetWidthNoStretch(); }
        public bool HasVertStrechArea(int drawnHeight) { return drawnHeight > GetHeightNoStretch(); }

        public int GetWidthNoStretch() { return size.Width - border.Width; }
        public int GetHeightNoStretch() { return size.Height - border.Height; }

        public float GetHoriSize(int drawnWidth)
        {
            return (float)(HasHoriStrechArea(drawnWidth) ? (drawnWidth < size.Width ? drawnWidth : size.Width) : GetWidthNoStretch());
        }

        public float GetVertSize(int drawnHeight)
        {
            return (float)(HasVertStrechArea(drawnHeight) ? (drawnHeight < size.Height ? drawnHeight : size.Height) : GetHeightNoStretch());
        }

        public int GetLeft(int drawnWidth)
        {
            float sizeByRatio = (float)drawnWidth * (border.Left / GetHoriSize(drawnWidth));
            return EzMath.Clamp((int)(Math.Ceiling(sizeByRatio)), 0, border.Left);
        }

        public int GetRight(int drawnWidth)
        {
            float sizeByRatio = (float)drawnWidth * ((size.Width - border.Right) / GetHoriSize(drawnWidth));
            return EzMath.Clamp((int)(Math.Ceiling(sizeByRatio)), 0, size.Width - border.Right);
        }

        public int GetTop(int drawnTop)
        {
            float sizeByRatio = (float)drawnTop * (border.Top / GetVertSize(drawnTop));
            return EzMath.Clamp((int)(Math.Ceiling(sizeByRatio)), 0, border.Top);
        }

        public int GetBottom(int drawnBottom)
        {
            float sizeByRatio = (float)drawnBottom * ((size.Height - border.Bottom) / GetVertSize(drawnBottom));
            return EzMath.Clamp((int)(Math.Ceiling(sizeByRatio)), 0, size.Height - border.Bottom);
        }
    }

    public class TextureRenderInfo
    {
        public Gwen.Texture texture;
        public float u1;
        public float v1;
        public float u2;
        public float v2;
        public Texture9GridBorderInfo uv9Grid;
    }

    public class GwenTextureProvider
    {
        public static GwenTextureProvider Instance = new GwenTextureProvider();

        public TextureRenderInfo GetTextureRenderInfo(Gwen.Renderer.Tao renderer, string url)
        {
            TextureRenderInfo tri;
            if (m_lut.TryGetValue(url, out tri))
                return tri;

            string filePath;
            string tileName;
            if (!ResUtil.ExtractTextureInfo(url, out filePath, out tileName))
                return null;

            Atlas atlas = AtlasManager.Instance.GetAtlas(renderer, filePath);
            if (atlas == null)
                return null;

            ImageResource res = ResourceManager.Instance.GetResource(filePath, tileName);
            if (res == null)
                return null;

            Texture9GridBorderInfo uv9Grid = null;
            Rectangle border9Grid = atlas.GetTileBorderInfo(tileName);
            if (border9Grid != ucore.Const.ZERO_RECT)
            {
                uv9Grid = new Texture9GridBorderInfo();
                uv9Grid.border = border9Grid;
                uv9Grid.size = res.Size;
                if (border9Grid.Left != 0 && border9Grid.Right != 0)
                {
                    uv9Grid.uLeft = ((float)res.Position.X + (float)border9Grid.Left) / (float)atlas.Texture.Width;
                    uv9Grid.uRight = ((float)res.Position.X + (float)border9Grid.Right) / (float)atlas.Texture.Width;
                }

                if (border9Grid.Top != 0 && border9Grid.Bottom != 0)
                {
                    uv9Grid.vTop = ((float)res.Position.Y + (float)border9Grid.Top) / (float)atlas.Texture.Height;
                    uv9Grid.vBottom = ((float)res.Position.Y + (float)border9Grid.Bottom) / (float)atlas.Texture.Height;
                }
            }

            tri = new TextureRenderInfo();
            tri.u1 = (float)res.Position.X / (float)atlas.Texture.Width;
            tri.v1 = (float)res.Position.Y / (float)atlas.Texture.Height;
            tri.u2 = ((float)res.Position.X + (float)res.Size.Width) / (float)atlas.Texture.Width;
            tri.v2 = ((float)res.Position.Y + (float)res.Size.Height) / (float)atlas.Texture.Height;
            tri.texture = atlas.Texture;
            tri.uv9Grid = uv9Grid;
            m_lut[url] = tri;
            return tri;
        }

        Dictionary<string, TextureRenderInfo> m_lut = new Dictionary<string, TextureRenderInfo>(); 
    }
}
