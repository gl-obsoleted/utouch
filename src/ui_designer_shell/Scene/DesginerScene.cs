using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib;
using ui_lib.Base;
using ui_lib.Elements;
using ui_lib.Widgets;

namespace ui_designer
{
    public partial class DesginerScene : IDisposable
    {
        /// <summary>
        /// 只读属性
        /// </summary>
        public RootNode Root { get { return m_root; } }

        public DesginerScene()
        {
            ImageNode m_child = new ImageNode();
            m_child.Res = "uires://testres/uiatlas:4880yuanbao.png";
            m_child.Position = new Point(50, 50);
            m_child.Size = new Size(50, 50);
            m_root.Attach(m_child);

            ImageNode m_child2 = new ImageNode();
            m_child2.Res = "uires://testres/uiatlas:+.png";
            m_child2.Position = new Point(150, 50);
            m_child2.Size = new Size(50, 50);
            m_root.Attach(m_child2);

            TextNode m_child3 = new TextNode();
            m_child3.Text = "hello world";
            m_child3.Color = Color.Purple;
            m_child3.Position = new Point(50, 120);
            m_child3.Size = new Size(100, 30);
            m_root.Attach(m_child3);
        }

        public bool Init()
        {
            foreach (string resFile in ConfigTypical.Instance.ReourceImages)
            {
                if (!ResourceManager.Instance.LoadFile(resFile))
                    return false;
            }
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
