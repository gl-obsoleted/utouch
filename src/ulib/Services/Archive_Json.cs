using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ucore;
using ulib.Elements;

namespace ulib
{
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
                    return NodeJsonUtil.JObjectToNode(jsonObject);
                }
            }
            catch (Exception e)
            {
                Logging.Instance.Log(e.Message);
                return null;
            }
        }

        public bool SaveTo(Node node, string targetLocation)
        {
            string text = NodeJsonUtil.NodeToString(node);
            if (text.Length == 0)
                return false;

            try
            {
                using (StreamWriter file = File.CreateText(targetLocation))
                {
                    file.Write(text);
                }
            }
            catch (System.Exception ex)
            {
                Logging.Instance.LogExceptionDetail(ex);
                return false;
            }

            return true;
        }
    }
}
