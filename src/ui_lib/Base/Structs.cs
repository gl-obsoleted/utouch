using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_lib.Base
{
    public class Color4b
    {
        public byte r, g, b, a;
    }

    public class Font
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public Color4b Color { get; set; }
    }
}
