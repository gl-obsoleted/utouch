using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ucore;
using ulib;
 
using ulib.Elements;

namespace ulib
{
    public class Scene : IDisposable
    {
        public static Scene Instance;

        /// <summary>
        /// 只读属性
        /// </summary>
        public RootNode Root { get { return m_root; } }

        public ResolutionV2 DesignTimeResolution;

        public Scene()
        {
        }

        public bool Init(ResolutionV2 designTimeResolution)
        {
            if (designTimeResolution != null)
            {
                DesignTimeResolution = designTimeResolution;
            }
            else
            {
                DesignTimeResolution = new ResolutionV2() { width = 960, height = 640, category = 1, tag = "iPhone 4" };
            }
            m_root = new RootNode();
            return true;
        }

        public void Dispose()
        {

        }

        public bool Load(string targetLocation)
        {
            //using (GScope_LoadingProcess lp = new GScope_LoadingProcess())
            {
                Node loaded = m_archiveSys.Load(targetLocation);
                if (loaded == null || !(loaded is RootNode))
                {
                    Logging.Instance.Log("Scene.Load 加载失败. '{0}'", targetLocation);
                    return false;
                }

                m_currentFilePath = targetLocation;
                m_root = loaded as RootNode;
            }

            return true;
        }

        public bool Save()
        {
            if (string.IsNullOrEmpty(m_currentFilePath))
            {
                Logging.Instance.Log("保存文件时 m_currentFilePath 无效，且未传入有效的路径。");
                return false;
            }

            return Save(m_currentFilePath);
        }

        public bool Save(string targetLocation)
        {
            if (!SysUtil.InTheSameDrive(GState.AssetRoot, targetLocation))
            {
                Logging.Instance.Log("将要保存的 {0} 文件与资源路径不在同一个分区，无法使用相对路径。", ConstDefault.LayoutPostfix);
                Logging.Instance.Log("    请将 {0} 文件保存到分区 {1}。", ConstDefault.LayoutPostfix, Path.GetPathRoot(GState.AssetRoot));
                Logging.Instance.Log("    目标路径：{0}", targetLocation);
                Logging.Instance.Log("    资源路径：{1}", GState.AssetRoot);
                return false;
            }

            string folderPath = Path.GetDirectoryName(targetLocation);
            m_root.Assets.AssetRoot = SysUtil.ToUnixPath(SysUtil.GetRelativePath(GState.AssetRoot, folderPath));

            if (!m_archiveSys.Save(m_root, targetLocation))
            {
                m_root.Assets.AssetRoot = GState.AssetRoot;
                return false;
            }

            if (m_currentFilePath != targetLocation)
            {
                m_currentFilePath = targetLocation;
            }
            return true;
        }

        public void Render(RenderContext rc, RenderDevice rs)
        {
            m_renderSys.Render(m_root, rc, rs);
        }

        public Node Pick(Point location)
        {
            return SceneGraphUtil.Pick(m_root, location);
        }

        public AssetDesc GetAssetDesc(string url)
        {
            if (!ResProtocol.IsSingleTexture(url))
                return null;

            string assetName = ResProtocol.GetSingleTextureAssetName(url);
            if (string.IsNullOrEmpty(assetName))
                return null;

            return Root.Assets.GetAssetDesc(assetName);
        }

        public string CurrentFilePath { get { return m_currentFilePath; } }

        private string m_currentFilePath;
        private RootNode m_root;
        private RenderSystem m_renderSys = new RenderSystem();
        private ArchiveSystem m_archiveSys = new ArchiveSystem();
    }
}
