using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Elements;

namespace ui_designer_shell
{
    public delegate void SelectNodeHandler(Node n, object sender);
    public delegate void RefreshSceneHandler();

    public class SceneEdEventNotifier
    {
        public static SceneEdEventNotifier Instance = new SceneEdEventNotifier();

        public void Emit_SelectNode(Node n, object sender)
        {
            if (SelectNode != null)
            {
                SelectNode(n, sender);
            }
        }

        public void Emit_RefreshScene()
        {
            if (RefreshScene != null)
            {
                RefreshScene();
            }
        }

        public event SelectNodeHandler SelectNode;
        public event RefreshSceneHandler RefreshScene;
    }
}
