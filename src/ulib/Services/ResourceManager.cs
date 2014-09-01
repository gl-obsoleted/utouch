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
        public string ResFilePath { get { return m_resFilePath; } }

        private string m_resFilePath;
        private Dictionary<string, ImageResource> m_resLut = new Dictionary<string, ImageResource>();
    }

    public partial class ResourceManager
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
            ImageResourceGroup rg = ResourceManagerUtil.LoadResFileIntoResourceGroup(resFile);
            if (rg == null)
            {
                Session.Message("Resource group '{0}' loading failed. ", resFile);
                return false;
            }
            m_resGroupsLut.Add(resFile, rg);
            return true;
        }

        public ImageResource GetResource(string resFile, string resName)
        {
            if (resFile == m_defaultResGroup.ResFilePath)
            {
                return GetDefaultResource(resName);
            }

            ImageResourceGroup group;
            if (!m_resGroupsLut.TryGetValue(resFile, out group))
                return null;

            return group.FindResource(resName);
        }

        public Dictionary<string, ImageResourceGroup> ResGroups { get { return m_resGroupsLut; } }

        private Dictionary<string, ImageResourceGroup> m_resGroupsLut = 
            new Dictionary<string, ImageResourceGroup>();

        public bool LoadDefault(string resFile)
        {
            ImageResourceGroup rg = ResourceManagerUtil.LoadResFileIntoResourceGroup(resFile);
            if (rg == null)
            {
                Session.Message("默认资源组 '{0}' 加载失败. ", resFile);
                return false;
            }

            m_defaultResGroup = rg;
            return true;
        }

        public string ComposeDefaultResURL(string resName)
        {
            if (m_defaultResGroup == null)
                return "";

            return BaseUtil.ComposeResURL(m_defaultResGroup.ResFilePath, resName);
        }

        public ImageResource GetDefaultResource(string resName)
        {
            if (m_defaultResGroup == null)
                return null;

            return m_defaultResGroup.FindResource(resName);
        }

        public ImageResourceGroup DefaultResGroup { get { return m_defaultResGroup; } }
        private ImageResourceGroup m_defaultResGroup;
    }
}
