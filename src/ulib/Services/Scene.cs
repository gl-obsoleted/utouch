using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ulib;
using ulib.Base;
using ulib.Elements;

namespace ulib
{
    public class Scene : IDisposable
    {
        public static Scene Instance;

        /// <summary>
        /// 只读属性
        /// </summary>
        public RootNode Root { get { return m_root; } }

        public Scene()
        {
        }

        public bool Init()
        {
            return true;
        }

        public void Dispose()
        {

        }

        public bool Load(string targetLocation)
        {
            Node loaded = m_archiveSys.Load(targetLocation);
            if (loaded == null || !(loaded is RootNode))
            {
                System.Diagnostics.Debug.WriteLine("[err] Loading failed.");
                return false;
            }

            m_root = loaded as RootNode;
            return true;
        }

        public bool Save(string targetLocation)
        {
            return m_archiveSys.Save(m_root, targetLocation);
        }

        public void Render(RenderContext rc, RenderDevice rs)
        {
            m_renderSys.Render(m_root, rc, rs);
        }

        public Node Pick(Point location)
        {
            return SceneGraphUtil.Pick(m_root, location);
        }

        private RootNode m_root = new RootNode();
        private RenderSystem m_renderSys = new RenderSystem();
        private ArchiveSystem m_archiveSys = new ArchiveSystem();
    }
}
