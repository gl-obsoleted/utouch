using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_lib.Base
{
    public class Vec2
    {
        public int x, y;

        public Vec2(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

    public class Size
    {
        public int width, height;

        public Size(int w, int h)
        {
            width = w;
            height = h;
        }
    }

    public class Rect
    {
        public Vec2 min, max;
    }

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
