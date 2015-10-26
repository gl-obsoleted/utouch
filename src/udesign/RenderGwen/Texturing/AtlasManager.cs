using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib;
using ulib.Base;

namespace udesign
{
    public class Atlas
    {
        public string FilePath;
        public Gwen.Texture Texture;
        public JObject Desc;

        public Rectangle GetTileBorderInfo(string tileName)
        {
            if (Desc == null)
                return ucore.Const.ZERO_RECT;

            JProperty jTile = Desc.Property(tileName);
            if (jTile == null)
                return ucore.Const.ZERO_RECT;

            JObject jsub = jTile.Value as JObject;
            Rectangle rect = Rectangle.FromLTRB(
                ucore.JsonHelpers.GetIntProperty(jsub, "horiLeft"),
                ucore.JsonHelpers.GetIntProperty(jsub, "vertTop"),
                ucore.JsonHelpers.GetIntProperty(jsub, "horiRight"),
                ucore.JsonHelpers.GetIntProperty(jsub, "vertBottom"));
            return rect;
        }
    }

    public class AtlasManager
    {
        public static AtlasManager Instance = new AtlasManager();

        public Atlas GetAtlas(Gwen.Renderer.Tao renderer, string filePath)
        {
            // 先在缓存里找
            Atlas ret;
            if (m_lut.TryGetValue(filePath, out ret))
                return ret;

            // 加载贴图，如果这个过程失败，那么认为 atlas 加载失败
            Gwen.Texture t;
            t = new Gwen.Texture(renderer);
            if (t == null)
                return null;
            t.Load(filePath + ResProtocol.ResImageFilePostfix);
            if (t.Failed)
                return null;

            // 加载描述文件，如果这个过程失败，仍返回有效的 atlas
            string descFilePath = filePath + ResProtocol.ResDescFilePostfix;
            JObject desc = null;
            if (File.Exists(descFilePath))
            {
                desc = ucore.JsonHelpers.ReadTextIntoJObject(filePath + ResProtocol.ResDescFilePostfix);
            }

            // 拼装成有效的 atlas
            ret = new Atlas();
            ret.FilePath = filePath;
            ret.Texture = t;
            ret.Desc = desc;
            return ret;
        }

        Dictionary<string, Atlas> m_lut = new Dictionary<string, Atlas>(); 
    }
}
