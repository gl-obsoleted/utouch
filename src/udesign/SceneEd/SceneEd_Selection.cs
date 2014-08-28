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
