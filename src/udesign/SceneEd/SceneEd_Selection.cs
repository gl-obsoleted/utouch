using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib.Elements;

namespace udesign
{
    public partial class SceneEd
    {
        bool SelectionContainsFullscreenRootNode()
        {
            foreach (var n in m_selection)
            {
                if (n is RootNode)
                {
                    RootNode r = n as RootNode;
                    if (r != null && r.IsFullscren)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        bool SelectionContainsLockedNode()
        {
            foreach (var n in m_selection)
            {
                if (NodeSGUtil.HasLockedLayoutParent(n))
                {
                    return true;
                }
            }

            return false;
        }
        private List<Node> m_selection = new List<Node>();
    }
}
