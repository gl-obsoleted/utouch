using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib;
using ulib.Elements;

namespace udesign
{
    public class TestScene
    {
        public static Node Build()
        {
            Node root = new Node();
            root.Size = new Size(300, 300);

            ImageNode m_child = new ImageNode();
            m_child.Res = ResourceManager.Instance.ComposeDefaultResURL("4880yuanbao.png");
            m_child.Position = new Point(50, 50);
            m_child.Size = new Size(50, 50);
            root.Attach(m_child);

            ImageNode m_child2 = new ImageNode();
            m_child2.Res = ResourceManager.Instance.ComposeDefaultResURL("+.png");
            m_child2.Position = new Point(150, 50);
            m_child2.Size = new Size(50, 50);
            root.Attach(m_child2);

            TextNode m_child3 = new TextNode();
            m_child3.Text = "hello world";
            m_child3.TextColor = Color.Purple;
            m_child3.Position = new Point(50, 120);
            m_child3.Size = new Size(100, 30);
            root.Attach(m_child3);

            return root;
        }

        public static void Run()
        {
            Node n = TestScene.Build();
            n.Position = new Point(-100, 100); // would see the clamping
            Scene.Instance.Root.Size = new Size(500, 500);
            Scene.Instance.Root.Attach(n);
            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
        }
    }
}
