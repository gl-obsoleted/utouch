using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ulib.Elements
{
    public class NodeSGUtil
    {
        /// <summary>
        /// 找到所有超出父节点边界的子节点，调整其位置和尺寸以避免超出父节点的范围
        /// 具体调整方式是，先调整位置，如果子节点尺寸超过了父节点尺寸，则调整尺寸
        /// </summary>
        /// <param name="n"></param>
        public static void ClampBounds(Node n)
        {
            if (n == null)
                return;

            if (n.Parent != null)
            {
                Point clampedPosition = n.Position;
                Size clampedSize = n.Size;

                // 处理宽度
                if (n.Size.Width >= n.Parent.Size.Width)
                {
                    clampedPosition.X = 0;
                    clampedSize.Width = n.Parent.Size.Width;
                }
                else if (n.Position.X < 0)
                {
                    clampedPosition.X = 0;
                }
                else if (n.GetBounds().Right > n.Parent.Size.Width)
                {
                    clampedPosition.X = n.Parent.Size.Width - n.Size.Width;
                }

                // 处理高度
                if (n.Size.Height >= n.Parent.Size.Height)
                {
                    clampedPosition.Y = 0;
                    clampedSize.Height = n.Parent.Size.Height;
                }
                else if (n.Position.Y < 0)
                {
                    clampedPosition.Y = 0;
                }
                else if (n.GetBounds().Bottom > n.Parent.Size.Height)
                {
                    clampedPosition.Y = n.Parent.Size.Height - n.Size.Height;
                }

                n.Position = clampedPosition;
                n.Size = clampedSize;
            }

            foreach (Node child in n.Children)
            {
                ClampBounds(child);
            }
        }

        public static bool HasLockedLayoutParent(Node n)
        {
            while (n != null)
            {
                if (n.Parent != null && n.Parent.LockChildrenLayoutRecursively)
                {
                    return true;
                }

                n = n.Parent;
            }

            return false;
        }
    }
}
