using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ui_lib.Elements
{
    public class NodeJsonUtil
    {
        /// <summary>
        /// 从 json 获取对应的对象类型
        /// 
        /// 注意，这个函数可能会抛出下面两种异常
        ///     1. json object 缺乏类型信息，
        ///     2. 类型信息找不到（也就是代码和资源不匹配）
        /// </summary>
        public static Type GetNodeType(JObject jsonObject)
        {
            string typeFullName = (string)jsonObject["__type_info__"];
            return Type.GetType(typeFullName);
        }

        public static Node CreateNode(JObject jsonObject)
        {
            Type type = GetNodeType(jsonObject);
            object instance = Activator.CreateInstance(type);
            return instance as Node;
        }
    }
}
