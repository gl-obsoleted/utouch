using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Elements;

namespace ui_designer_shell
{
    public delegate void OnNodeSelected(Node n, object sender);

    public class ShellNotifier
    {
        public static ShellNotifier Instance = new ShellNotifier();

        public void SelectSceneNode(Node n, object sender)
        {
            if (SelectNode != null)
            {
                SelectNode(n, sender);
            }
        }

        public event OnNodeSelected SelectNode; 
    }
}
