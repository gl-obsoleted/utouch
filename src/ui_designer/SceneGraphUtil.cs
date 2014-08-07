using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Elements;

namespace ui_designer
{
    public class SceneGraphUtil
    {
        /// <summary>
        /// 目前唯一的用途是在由 json 重建场景树之后，手动恢复所有子节点的 m_parent 字段
        /// </summary>
        public static void UnifyParents(Node node)
        {
            node.UnifyParentOfChildren();

            foreach (Node child in node.Children)
                UnifyParents(child);
        }
    }
}
