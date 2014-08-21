using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ui_lib.Elements;

namespace ui_lib
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

        /// <summary>
        /// 找到该位置所在的节点
        /// </summary>
        public static Node Pick(Node node, Point location)
        {
            if (!node.GetBounds().Contains(location))
                return null;

            location.X -= node.Position.X;
            location.Y -= node.Position.Y;
            foreach (Node child in node.Children)
            {
                Node ret = Pick(child, location);
                if (ret != null)
                    return ret;
            }
            location.X += node.Position.X;
            location.Y += node.Position.Y;
            return node;
        }
    }
}
