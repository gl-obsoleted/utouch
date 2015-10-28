using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ucore;
 
using ulib.Elements;

namespace ulib
{
    public class BootParams
    {
        public string ReourcePath;
        public string DefaultReourceImage;
        public List<string> ReourceImages;
        public string ScenePath;
        public ResolutionV2 DesignTimeResolution;
    }

    /// <summary>
    /// 
    /// Bootstrap 类是 ulib 库对外的入口点
    /// 
    /// ulib库包含了一些功能性的服务，这些服务定义在 Services 中，
    /// 出于效率和简单性考虑，它们并未被实现为（物理上的）严格隔离，
    /// 但在功能上，是尽量彼此正交而独立的。
    /// 
    /// Bootstrap 充当了启动脚本的作用，以最典型的方式启动这些服务。
    /// 
    /// Init() 函数被设计为可重复调用，效果相当于重置
    /// </summary>
    public class Bootstrap
    {
        public static Bootstrap Instance = new Bootstrap();

        public bool Init(BootParams bp)
        {
            // 请理性工作
            if (Scene.Instance != null)
                Scene.Instance.Dispose();
            if (ResourceManager.Instance != null)
                ResourceManager.Instance.Clear();

            ArchiveUtil.ClearCreators();
            NodeNameUtil.ResetIDAllocLut();

            // 初始化工厂
            ArchiveUtil.RegisterCreator(ArchiveType.Json, typeof(Archive_Json));

            // 跑用户脚本
            string userLua = Path.ChangeExtension(bp.ScenePath, ".lua");
            bool runUserLua = File.Exists(userLua);
            if (runUserLua && !LuaRuntime.RunScript(userLua))
            {
                Logging.Instance.Log("执行用户脚本 ('{0}') 失败.", userLua);
                return false;
            }

            // 初始化资源系统
            if (!ResourceManager.Instance.LoadDefault(Path.Combine(bp.ReourcePath, bp.DefaultReourceImage)))
            {
                Logging.Instance.Log("加载默认资源 ('{0}') 失败.", bp.DefaultReourceImage);
                return false;
            }
            if (bp.ReourceImages != null)
            {
                foreach (string resFile in bp.ReourceImages)
                {
                    string resCombined = Path.Combine(bp.ReourcePath, resFile);
                    if (!ResourceManager.Instance.LoadFile(resCombined))
                    {
                        Logging.Instance.Log("加载资源 ('{0}') 失败.", resCombined);
                        return false;
                    }
                }
            }

            if (runUserLua)
            {
                string userAtlasName = LuaRuntime.GetGlobalString("UserAtlasName");
                if (!string.IsNullOrEmpty(userAtlasName))
                {
                    string userAtlas = Path.Combine(Path.GetDirectoryName(bp.ScenePath), userAtlasName);
                    if (!ResourceManager.Instance.LoadFile(userAtlas, userAtlasName))
                    {
                        Logging.Instance.Log("加载用户资源 ('{0}') 失败.", userAtlas);
                        return false;
                    }
                }
            }
            Logging.Instance.Log("资源系统初始化成功.");

            // 初始化场景
            Scene.Instance = new Scene();
            if (!Scene.Instance.Init(bp.DesignTimeResolution))
                return false;
            if (bp.ScenePath.Length != 0 && !Scene.Instance.Load(bp.ScenePath))
                return false;
            Logging.Instance.Log("场景初始化成功.");

            return true;
        }
    }
}
