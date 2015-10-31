using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ucore;
 

namespace ulib.Elements
{
    public class ImageNode : Node
    {
        public ImageNode()
            : base()
        {
            // 给一个合理的默认值
            Res = ResourceManager.Instance.ComposeDefaultResURL("tongyi.png");
        }

        [Category("Image")]
        [DisplayName("资源名")]
        public string Res 
        {
            get { return m_res; } 
            set 
            {
                m_res = value;

                if (!GState.IsInLoadingProcess)
                {
                    ResizeToResSize(); 
                }
            } 
        }

        public override System.Drawing.Size GetExpectedResourceSize()
        {
            if (ResProtocol.IsSingleTexture(Res))
            {
                AssetDesc desc = Scene.Instance.GetAssetDesc(Res);
                if (desc == null)
                    return Const.ZERO_SIZE;

                return AssetUtil.GetImageSize(desc.Path);
            }
            else 
            {
                return ResourceManager.Instance.GetResourceSize(Res);
            }
        }

        private string m_res;
    }
}
