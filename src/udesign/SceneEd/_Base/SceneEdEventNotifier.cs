using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib.Elements;

namespace udesign
{
    [Flags]
    public enum RefreshSceneOpt
    {
        Refresh_Rendering = 1,
        Refresh_Layout = 2,
        Refresh_Properties = 4,
        Refresh_MainMenu = 8,
        Refresh_All = Refresh_Rendering | Refresh_Layout | Refresh_Properties | Refresh_MainMenu,
    }

    public delegate void SelectNodeHandler(Node n, object sender);
    public delegate void RefreshSceneHandler(RefreshSceneOpt opts);

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

        public void Emit_RefreshScene(RefreshSceneOpt opts)
        {
            if (RefreshScene != null)
            {
                RefreshScene(opts);
            }
        }

        public event SelectNodeHandler SelectNode;
        public event RefreshSceneHandler RefreshScene;
    }
}
