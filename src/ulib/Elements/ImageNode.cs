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
                string tex = ResProtocol.GetSingleTextureFullPath(Res);
                
                if (string.IsNullOrEmpty(tex))
                    return Const.ZERO_SIZE;

                if (!File.Exists(tex))
                    return Const.ZERO_SIZE;

                try
                {
                    return Image.FromFile(tex).Size;
                }
                catch (Exception)
                {
                    return Const.ZERO_SIZE;
                }
            }
            else 
            {
                return ResourceManager.Instance.GetResourceSize(Res);
            }
        }

        private string m_res;
    }
}
