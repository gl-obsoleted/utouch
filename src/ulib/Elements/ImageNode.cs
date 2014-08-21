using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

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
        [Description("资源名")]
        public string Res { get; set; }
    }
}
