using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Elements;
using ui_lib.Widgets;

namespace ui_designer
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

    public class Archive_Json : IArchive
    {
        public ArchiveType GetArcType()
        {
            return ArchiveType.Json;
        }

        public bool Validate(string targetLocation)
        {
            return true;
        }

        public Node LoadFrom(string targetLocation)
        {
            try
            {
                // read JSON directly from a file
                using (StreamReader file = File.OpenText(targetLocation))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject jsonObject = (JObject)JToken.ReadFrom(reader);
                    JsonSerializer se = JsonSerializer.CreateDefault();
                    se.Converters.Add(new NodeConverter());
                    object obj = jsonObject.ToObject(NodeJsonUtil.GetNodeType(jsonObject), se);
                    if (!(obj is Node))
                        return null;

                    // 手动恢复每个 node 的 m_parent 字段
                    Node root = obj as Node;
                    SceneGraphUtil.UnifyParents(root);
                    return root;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return null;
        }

        public bool SaveTo(Node node, string targetLocation)
        {
            JObject obj = (JObject)JToken.FromObject(node);
            //System.Diagnostics.Debug.Write(m_object.ToString());

            using (StreamWriter file = File.CreateText(targetLocation))
            {
                file.Write(obj.ToString());
            }
            return true;
        }
    }
}
