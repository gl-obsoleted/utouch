using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Base;
using ui_lib.Elements;
using ui_lib.Widgets;

namespace ui_designer
{
    public partial class DesginerScene
    {
        /// <summary>
        /// 只读属性
        /// </summary>
        public RootNode Root { get { return m_root; } }

        public DesginerScene()
        {
            Node m_child = new Node();
            m_child.Position = new Point(50, 50);
            m_child.Size = new Size(50, 50);
            Node m_child2 = new Node();
            m_child2.Position = new Point(150, 50);
            m_child2.Size = new Size(50, 50);
            m_root.Attach(m_child);
            m_root.Attach(m_child2);
        }

        public bool Load(string targetLocation)
        {
            Node loaded = m_archiveSys.Load(targetLocation);
            if (loaded == null || !(loaded is RootNode))
                return false;

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

        private RootNode m_root = new RootNode();
        private RenderSystem m_renderSys = new RenderSystem();
        private ArchiveSystem m_archiveSys = new ArchiveSystem();
    }
}
