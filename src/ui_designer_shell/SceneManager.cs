using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_designer;

namespace ui_designer_shell
{
    public class SceneManager
    {
        public static SceneManager Instance;

        public SceneManager()
        {
            m_scene = new Scene();
        }

        public void Configure(IRenderContext rc, IRenderSystem rs)
        {
            m_rc = rc;
            m_rs = rs;
        }

        public void RenderScene()
        {
            m_scene.Render(m_rc, m_rs);
        }

        private Scene m_scene;
        private IRenderContext m_rc;
        private IRenderSystem m_rs;
    }
}
