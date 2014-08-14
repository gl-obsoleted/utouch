using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib;
using ui_lib.Base;

namespace ui_designer.Render
{
    public class TextureRenderInfo
    {
        public Gwen.Texture texture;
        public float u1;
        public float v1;
        public float u2;
        public float v2;
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

            // loading texture
            Gwen.Texture t;
            t = new Gwen.Texture(renderer);
            if (t == null)
                return null;
            t.Load(filePath + Constants.ResImageFilePostfix);
            if (t.Failed)
                return null;

            // getting tile info
            ImageResource res = ResourceManager.Instance.GetResource(filePath, tileName);
            if (res == null)
                return null;

            tri = new TextureRenderInfo();
            tri.u1 = (float)res.Position.X / (float)t.Width;
            tri.v1 = (float)res.Position.Y / (float)t.Height;
            tri.u2 = ((float)res.Position.X + (float)res.Size.Width) / (float)t.Width;
            tri.v2 = ((float)res.Position.Y + (float)res.Size.Height) / (float)t.Height;
            tri.texture = t;
            m_lut[url] = tri;
            return tri;
        }

        Dictionary<string, TextureRenderInfo> m_lut = new Dictionary<string, TextureRenderInfo>(); 
    }
}
