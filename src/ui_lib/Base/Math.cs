using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_lib.Base
{
    public class Math
    {
        public static int Clamp(int x, int min, int max)
        {
            return x < min ? min : (x > max ? max : x);
        }

        public static Point Clamp(Point pt, Rectangle rect)
        {
            return new Point(
                Clamp(pt.X, rect.Left, rect.Right),
                Clamp(pt.Y, rect.Top, rect.Bottom));
        }
    }
}
