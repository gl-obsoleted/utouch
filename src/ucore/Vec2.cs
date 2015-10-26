using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ucore
{
    public struct Vec2
    {
        public static readonly Vec2 ZERO = new Vec2 { X = 0, Y = 0 };

        public float X;
        public float Y;

        public Vec2(int x, int y)
        {
            X = (float)x;
            Y = (float)y;
        }

        public Vec2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vec2(Point pt)
        {
            X = (float)pt.X;
            Y = (float)pt.Y;
        }

        public static implicit operator Vec2(Point pt)
        {
            return new Vec2(pt);
        }
    }
}
