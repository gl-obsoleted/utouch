using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ucore;
using ulib;
 

namespace udesign
{
    public class UserPreference
    {
        public static UserPreference Instance = new UserPreference();

        private string m_filePath;

        /// <summary>
        /// 这个函数不会失败，安全无害，绿色透明，加载失败的话就初始化一个新的。
        /// </summary>
        public void Init(string filePath)
        {
            JObject loaded = ucore.JsonHelpers.ReadTextIntoJObject(filePath);
            if (loaded != null)
            {
                m_jsonObject = loaded;
            }
            else
            {
                m_jsonObject = new JObject();
            }
            m_filePath = filePath;
        }

        public void Save()
        {
            if (m_jsonObject != null)
            {
                try
                {
                    File.WriteAllText(m_filePath, m_jsonObject.ToString());
                }
                catch (Exception e)
                {
                    Logging.Instance.LogExceptionDetail(e);
                }
            }
        }

        public string GetValue(string path, string key)
        {
            JObject node = Lookup(path);
            if (node == null)
                return "";

            JToken v;
            if (node.TryGetValue(key, out v))
            {
                return (string)v;
            }
            else
            {
                return "";
            }
        }

        public bool SetValue(string path, string key, string value)
        {
            JObject node = Lookup(path);
            if (node == null)
                return false;

            node[key] = value;
            return true;
        }

        private JObject Lookup(string jsonPath)
        {
            if (m_jsonObject == null)
                m_jsonObject = new JObject();

            string[] nodes = jsonPath.Split('.');
            JObject current = m_jsonObject;
            foreach (string n in nodes)
            {
                JProperty p = current.Property(n);
                if (p != null && p.Value.Type != JTokenType.Object)
                {
                    p.Remove();
                    p = null;
                }
                if (p == null)
                {
                    p = new JProperty(n, new JObject());
                    current.Add(p);
                }
                current = p.Value as JObject;
            }

            return current;
        }

        /// <summary>
        /// 一些存储用户的使用习惯的变量 (系统会保存但用户不需要知道)
        /// </summary>
        private JObject m_jsonObject;
    }
}
