using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_lib.Base
{
    public class JsonUtil
    {
        public static JObject LoadJObject(string filepath)
        {
            try
            {
                // read JSON directly from a file
                using (StreamReader file = File.OpenText(filepath))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    return (JObject)JToken.ReadFrom(reader);
                }
            }
            catch (Exception e)
            {
                Session.LogExceptionDetail(e);
                return null;
            }
        }
    }
}
