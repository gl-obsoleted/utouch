using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ucore;
using ulib;
 

namespace udesign
{
    public class GwenTextureUtil
    {
        public static TextureRenderInfo BuildSingleTexture(Gwen.Renderer.Tao renderer, string url)
        {
            if (!ResProtocol.IsSingleTexture(url))
                return null;

            AssetDesc desc = Scene.Instance.GetAssetDesc(url);
            if (desc == null)
                return null;

            string fullpath = Path.Combine(GState.AssetRoot, SysUtil.ToWindowsPath(desc.Path));
            if (!File.Exists(fullpath))
                return null;

            Gwen.Texture t;
            t = new Gwen.Texture(renderer);
            if (t == null)
                return null;

            t.Load(fullpath);
            if (t.Failed)
                return null;

            TextureRenderInfo tri = new TextureRenderInfo();
            tri.u1 = 0;
            tri.v1 = 0;
            tri.u2 = 1;
            tri.v2 = 1;
            tri.texture = t;
            return tri;
        }
    }
}
