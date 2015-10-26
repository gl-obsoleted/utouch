using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ucore
{
    public class EzMath
    {
        public static bool IsZero(float f1)
        {
            return (Math.Abs(f1) <= Single.Epsilon);
        }

        public static bool FuzzyEqual(float f1, float f2)
        {
            return (Math.Abs(f1 - f2) <= Single.Epsilon);
        }

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

        public static Rectangle Clamp(Rectangle clampee, Rectangle clamper)
        {
            return new Rectangle(
                Clamp(clampee.Location, clamper),
                (Size)(Clamp(clampee.Location + clampee.Size, clamper) - (Size)clampee.Location));
        }

        public static bool IsInvalid(Rectangle rect)
        {
            return rect == Const.INVALID_RECT;
        }
    }
}
