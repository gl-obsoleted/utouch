using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ulib.Elements
{
    public class TextNode : Node
    {
        public TextNode() : base()
        {
            Text = "<not_set_yet>";
            TextColor = Color.White;
            Size = new Size(100, 30);
            RequestedSizeRefreshing = false;
        }

        [Category("Text")]
        [Description("文字内容")]
        public string Text { get { return m_text; } set { m_text = value; RequestedSizeRefreshing = true; } }

        [Category("Text")]
        [Description("文字颜色")]
        public Color TextColor { get; set; }

        public Base.Font Font { get; set; }

        public bool RequestedSizeRefreshing { get; set; }
        protected string m_text;
    }
}
