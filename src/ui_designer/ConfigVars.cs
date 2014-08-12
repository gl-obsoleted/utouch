using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_designer
{
    public class ConfigTypical
    {
        public string DefaultResourceFilePath;

        public static ConfigTypical Instance = new ConfigTypical();

        public ConfigTypical() 
        {

        }

        public bool LoadTypical()
        {
            // 请注意这里对系统默认设置(default)和用户设置(user)的不同处理
            // 系统设置读取失败，是一个致命错误，缺乏正确的参数可能导致其他系统都无法正常工作，此时程序应直接退出
            // 而下面的用户设置读取失败，则只是一个普通错误，只是意味着用户对默认值的修改没能生效而已，程序仍能正常工作

            if (!LoadTypicalObject(AppConsts.ConfigFileDefault, Instance))
                return false;

            // 对用户设置读取的额外说明
            // 由于内部的 JsonConvert.PopulateObject() 会将出错前的所有值都写入对象，那么如果
            // 在 ConfigTypical.Instance 对象上读取失败，就会出现不确定哪些属性被覆盖和没被覆盖的情况。
            // 这里我们使用一次额外的对 dummy 对象的读取来保证这个操作的事务性。
            // 也就是说，要么就正常地读取完整的信息，要么就全部丢弃所有的用户设置

            ConfigTypical dummy = new ConfigTypical();
            if (!LoadTypicalObject(AppConsts.ConfigFileUser, dummy))
            {
                Session.Log("加载用户设置文件('{0}')失败，但不影响正常使用。", AppConsts.ConfigFileUser);
            }
            else
            {
                LoadTypicalObject(AppConsts.ConfigFileUser, Instance);
            }

            return true;
        }

        private bool LoadTypicalObject(string filepath, object obj)
        {
            try
            {
                string json = File.ReadAllText(filepath);
                JsonConvert.PopulateObject(json, obj);
                return true;
            }
            catch (Exception e)
            {
                Session.Log("Failed to load a json file. '{0}'", filepath);
                Session.LogExceptionDetail(e);
                return false;                
            }
        }
    }

    public class ConfigUserPref
    {
        public static ConfigUserPref Instance = new ConfigUserPref();

        /// <summary>
        /// 这个函数不会失败，安全无害，绿色透明，加载失败的话就初始化一个新的。
        /// </summary>
        public void Init()
        {
            JObject loaded = LoadJObject(AppConsts.UserPrefFilePath);
            if (loaded != null)
            {
                m_jsonObject = loaded;
            }
            else
            {
                m_jsonObject = new JObject();
            }
        }

        public void Save()
        {
            if (m_jsonObject != null)
            {
                try
                {
                    File.WriteAllText(AppConsts.UserPrefFilePath, m_jsonObject.ToString());
                }
                catch (Exception e)
                {
                    Session.LogExceptionDetail(e);
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

        private JObject LoadJObject(string filepath)
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

        /// <summary>
        /// 一些存储用户的使用习惯的变量 (系统会保存但用户不需要知道)
        /// </summary>
        private JObject m_jsonObject;
    }
}
