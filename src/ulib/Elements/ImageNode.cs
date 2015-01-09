using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ulib.Base;

namespace ulib.Elements
{
    public class ImageNode : Node
    {
        public ImageNode()
            : base()
        {
            // 给一个合理的默认值
            Res = "uires://testres/uiatlas:tongyi.png";
        }

        [Category("Image")]
        [DisplayName("资源名")]
        public string Res 
        {
            get { return m_res; } 
            set 
            {
                m_res = value;

                string filePath;
                string tileName;
                if (ResUtil.ExtractTextureInfo(m_res, out filePath, out tileName) && ResUtil.IsLegacyDefaultAtlas(filePath))
                {
                    m_res = ResourceManager.Instance.ComposeDefaultResURL(tileName);
                }

                if (!GState.IsInLoadingProcess)
                {
                    ResizeToResSize(); 
                }
            } 
        }

        public override System.Drawing.Size GetExpectedResourceSize()
        {
            return ResourceManager.Instance.GetResourceSize(Res);
        }

        private string m_res;
    }
}
