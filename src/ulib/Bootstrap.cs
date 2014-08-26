using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulib.Elements;

namespace ulib
{
    public class BootParams
    {
        public string DefaultReourceImage;
        public List<string> ReourceImages;
        public string ScenePath;
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

            // 初始化资源系统
            if (bp.DefaultReourceImage.Length == 0 ||
                !ResourceManager.Instance.LoadDefault(bp.DefaultReourceImage))
            {
                Session.Log("加载默认资源 ('{0}') 失败.", bp.DefaultReourceImage);
                return false;
            }
            if (bp.ReourceImages != null)
            {
                foreach (string resFile in bp.ReourceImages)
                {
                    if (!ResourceManager.Instance.LoadFile(resFile))
                    {
                        Session.Log("加载资源 ('{0}') 失败.", bp.DefaultReourceImage);
                        return false;
                    }
                }
            }
            Session.Log("资源系统初始化成功.");

            // 初始化场景
            Scene.Instance = new Scene();
            if (!Scene.Instance.Init())
                return false;
            if (bp.ScenePath.Length != 0 && !Scene.Instance.Load(bp.ScenePath))
                return false;
            Session.Log("场景初始化成功.");

            return true;
        }
    }
}
