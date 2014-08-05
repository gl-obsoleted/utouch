using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Base;

namespace ui_lib.Elements
{
    public class NodeUtil
    {
        /// <summary>
        /// 当向 destNode 节点添加子节点时，检测并生成一个独特的节点名
        /// </summary>
        public static string GenerateUniqueChildName(Node destNode, Node srcNode)
        {
            string srcName = srcNode.Name;
            destNode.TraverseChildren((Node child) => {
                // 这里 id 顺序递增的情况下，理论上最多生成两次
                while (child.Name == srcName)
                    srcName = GetNextAvailName(srcNode.GetType().Name);
            });

            return srcName;
        }

        /// <summary>
        /// 判断是否能对该节点重命名（即是否与同层节点命名有冲突）
        /// </summary>
        public static bool HasNameCollisionWithSiblings(Node node, string newName)
        {
            if (node.Parent == null)
                return false;

            foreach (Node sib in node.Parent.Children)
            {
                if (node != sib && newName == sib.Name)
                    return true;
            }

            return false;
        }

        //public static int ExtractNameSeqID(Node n)
        //{
        //    string typeName = n.GetType().Name;
        //    if (!n.Name.StartsWith(typeName))
        //        return Constants.INVALID_ID;

        //    string numPart = n.Name.Substring(typeName.Length);
        //    if (!numPart.StartsWith("<") || !numPart.EndsWith(">"))
        //        return Constants.INVALID_ID;

        //    try
        //    {
        //        string numStr = numPart.Substring(1, numPart.Length - 2);
        //        return Convert.ToInt32(numStr);
        //    }
        //    catch (Exception)
        //    {
        //        return Constants.INVALID_ID;
        //    }
        //}

        private static Dictionary<string, int> s_idAlloc = new Dictionary<string,int>();
        private static string GetNextAvailName(string typeName)
        {
            int id = 0;
            if (!s_idAlloc.TryGetValue(typeName, out id))
            {
                s_idAlloc[typeName] = 0;
            }
            s_idAlloc[typeName]++;
            return string.Format("{0}<{1}>", typeName, s_idAlloc[typeName]);
        }
    }
}
