using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ucore;
 

namespace ulib
{
    public static class ResourceManagerUtil
    {
        public static ImageResourceGroup LoadResFileIntoResourceGroup(string resFile)
        {
            string indexFile = resFile + ResProtocol.ResIndexFilePostfix;
            string imageFile = resFile + ResProtocol.ResImageFilePostfix;
            if (!File.Exists(indexFile) || !File.Exists(imageFile))
            {
                Logging.Instance.Message("Resource '{0}' broken. ({1} or {2} not found)",
                    resFile, ResProtocol.ResIndexFilePostfix, ResProtocol.ResImageFilePostfix);
                return null;
            }

            JObject jobj = ucore.JsonHelpers.ReadTextIntoJObject(indexFile);
            if (jobj == null || jobj.Property("frames").Value as JObject == null)
            {
                Logging.Instance.Message("Resource index file '{0}' loading failed.", indexFile);
                return null;
            }

            JObject jsub = jobj.Property("frames").Value as JObject;
            ImageResourceGroup resGroup = new ImageResourceGroup(resFile);
            foreach (JProperty p in jsub.Properties())
            {
                string resName = p.Name;
                try
                {
                    JObject prop = p.Value as JObject;
                    int x = int.Parse((string)prop["frame"]["x"]);
                    int y = int.Parse((string)prop["frame"]["y"]);
                    int w = int.Parse((string)prop["frame"]["w"]);
                    int h = int.Parse((string)prop["frame"]["h"]);
                    resGroup.AddResouce(new ImageResource { Name = resName, Position = new Point(x, y), Size = new Size(w, h) });
                }
                catch (Exception e)
                {
                    // 这里不高兴处理各种琐碎的错误了，要是格式不符合就直接跳过吧
                    // 不过问题还是记在 log 里了，想看还是可以看的
                    Logging.Instance.LogExceptionDetail(e);
                }
            }
            return resGroup;
        }
    }
}
