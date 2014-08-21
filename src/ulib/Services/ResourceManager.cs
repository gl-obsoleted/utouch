using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ulib.Base;

namespace ulib
{
    public class ImageResource
    {
        public string Name { get; set; }
        public Point Position { get; set; }
        public Size Size { get; set; }
    }

    public class ImageResourceGroup
    {
        public ImageResourceGroup(string resFilePath) 
        {
            m_resFilePath = resFilePath;
        }

        public void AddResouce(ImageResource res)
        {
            m_resLut.Add(res.Name, res);
        }

        public ImageResource FindResource(string name)
        {
            ImageResource res;
            return m_resLut.TryGetValue(name, out res) ? res : null;
        }

        public Dictionary<string, ImageResource> ResLut { get { return m_resLut; } }

        private string m_resFilePath;
        private Dictionary<string, ImageResource> m_resLut = new Dictionary<string, ImageResource>();
    }

    public class ResourceManager
    {
        public static ResourceManager Instance = new ResourceManager();

        public ResourceManager()
        {

        }

        public void Clear()
        {
            m_resGroupsLut.Clear();
        }

        public bool LoadFile(string resFile)
        {
            string indexFile = resFile + Constants.ResIndexFilePostfix;
            string imageFile = resFile + Constants.ResImageFilePostfix;
            if (!File.Exists(indexFile) || !File.Exists(imageFile))
            {
                Session.Message("Resource '{0}' broken. ({1} or {2} not found)",
                    resFile, Constants.ResIndexFilePostfix, Constants.ResImageFilePostfix);
                return false;
            }

            JObject jobj = JsonUtil.LoadJObject(indexFile);
            if (jobj == null || jobj.Property("frames").Value as JObject == null)
            {
                Session.Message("Resource index file '{0}' loading failed.", indexFile);
                return false;
            }

            JObject jsub = jobj.Property("frames").Value as JObject;
            ImageResourceGroup resGroup = new ImageResourceGroup(imageFile);
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
                    Session.LogExceptionDetail(e);
                }
            }
            m_resGroupsLut.Add(resFile, resGroup);
            return true;
        }

        public ImageResource GetResource(string resFile, string resName)
        {
            ImageResourceGroup group;
            if (!m_resGroupsLut.TryGetValue(resFile, out group))
                return null;

            return group.FindResource(resName);
        }

        public Dictionary<string, ImageResourceGroup> ResGroups { get { return m_resGroupsLut; } }

        private Dictionary<string, ImageResourceGroup> m_resGroupsLut = 
            new Dictionary<string, ImageResourceGroup>();
    }
}
