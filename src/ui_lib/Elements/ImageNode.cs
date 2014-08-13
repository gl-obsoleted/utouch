using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_lib.Elements
{
    public class ImageNode : Node
    {
        public ImageNode() : base()
        {

        }

        [Category("Image")]
        [Description("资源名")]
        public string ResLocation { get; set; }
    }
}
