using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Elements;

namespace ui_designer
{
    public class Archive_Json : IArchive
    {
        public ArchiveType GetType()
        {
            return ArchiveType.Json;
        }

        public bool Validate(string targetLocation)
        {
            return true;
        }

        public Node LoadFrom(string targetLocation)
        {
            return new Node();
        }

        public bool SaveTo(Node node, string targetLocation)
        {
            m_object = (JObject)JToken.FromObject(node);
            //System.Diagnostics.Debug.Write(m_object.ToString());

            using (StreamWriter file = File.CreateText(targetLocation))
            {
                file.Write(m_object.ToString());
            }
            return true;
        }

        JObject m_object;
    }
}
