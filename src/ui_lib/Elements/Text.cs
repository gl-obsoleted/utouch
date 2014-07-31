using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_lib.Elements
{
    class Text : Node
    {
        public Text() : base()
        {
            Content = "<not_set_yet>";
        }

        string Content { get; set; }
        Base.Font Font { get; set; }
    }
}
