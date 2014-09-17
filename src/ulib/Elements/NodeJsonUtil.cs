using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulib.Elements
{
    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jObject">
        /// contents of JSON object that will be deserialized
        /// </param>
        /// <returns></returns>
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                         object existingValue,
                                         JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            T target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public override void WriteJson(JsonWriter writer,
                                       object value,
                                       JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class NodeConverter : JsonCreationConverter<Node>
    {
        protected override Node Create(Type objectType, JObject jObject)
        {
            return NodeJsonUtil.CreateNode(jObject);
        }
    }

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
            string typeName = (string)jsonObject["__type_info__"];
            return TypeRegistry.Instance.QueryType(typeName);
        }

        public static Node CreateNode(JObject jsonObject)
        {
            Type type = GetNodeType(jsonObject);
            object instance = Activator.CreateInstance(type);
            return instance as Node;
        }

        public static Node JObjectToNode(JObject jobj)
        {
            JsonSerializer se = JsonSerializer.CreateDefault();
            se.Converters.Add(new NodeConverter());
            object obj = jobj.ToObject(NodeJsonUtil.GetNodeType(jobj), se);
            if (!(obj is Node))
            {
                Session.Log("Invalid json object.");
                return null;
            }

            // 手动恢复每个 node 的 m_parent 字段
            Node root = obj as Node;
            SceneGraphUtil.UnifyParents(root);
            return root;
        }

        public static string NodeToString(Node n)
        {
            try
            {
                JObject obj = (JObject)JToken.FromObject(n);
                return obj.ToString();
            }
            catch (System.Exception ex)
            {
                Session.LogExceptionDetail(ex);
                return "";
            }
        }

        public static Node StringToNode(string str)
        {
            try
            {
                return JObjectToNode((JObject)JToken.Parse(str));
            }
            catch (System.Exception ex)
            {
                Session.LogExceptionDetail(ex);
                return null;
            }
        }

        public static bool PopulateExistingNodeWithString(Node targetNode, string str)
        {
            try
            {
                JObject jobj = (JObject)JToken.Parse(str);
                if (jobj == null)
                    return false;

                JsonSerializer se = JsonSerializer.CreateDefault();
                se.Converters.Add(new NodeConverter());
                se.Populate(jobj.CreateReader(), targetNode);
                return true;
            }
            catch (System.Exception ex)
            {
                Session.LogExceptionDetail(ex);
                return false;
            }
        }
    }
}
