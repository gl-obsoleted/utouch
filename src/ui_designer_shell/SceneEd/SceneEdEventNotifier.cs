using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Elements;

namespace ui_designer_shell
{
    public class MoveNodeEventArgs : EventArgs
    {
        public MoveNodeEventArgs(Node target, Point oldP, Point newP)
        {
            m_target = target;
            m_oldPos = oldP;
            m_newPos = newP;
        }

        Node m_target;
        Point m_oldPos;
        Point m_newPos;
    }

    public delegate void SelectNodeHandler(Node n, object sender);
    public delegate void MoveNodeHandler(object sender, MoveNodeEventArgs args);
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

        public void Emit_MoveNode(object sender, MoveNodeEventArgs args)
        {
            if (MoveNode != null)
            {
                MoveNode(sender, args);
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
        public event MoveNodeHandler MoveNode;
        public event RefreshSceneHandler RefreshScene;
    }
}
