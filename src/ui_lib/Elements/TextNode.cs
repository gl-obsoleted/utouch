using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_lib.Elements
{
    public class TextNode : Node
    {
        public TextNode() : base()
        {
            Text = "<not_set_yet>";
            Color = Color.OrangeRed;
            Size = new Size(100, 30);
        }

        [Category("Text")]
        [Description("文字内容")]
        public string Text { get; set; }

        [Category("Text")]
        [Description("文字颜色")]
        public Color Color { get; set; }

        public Base.Font Font { get; set; }
    }
}
