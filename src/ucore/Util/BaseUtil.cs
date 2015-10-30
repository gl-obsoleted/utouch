using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

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

    public class BaseUtil
    {
        public static bool Implements(Type sourceClass, Type targetInterface)
        {
            if (!sourceClass.IsClass || !targetInterface.IsInterface)
                return false;

            return sourceClass.GetInterface(targetInterface.FullName) != null;
        }

        public static string PointToString(Point pt)
        {
            return string.Format("{0},{1}", pt.X, pt.Y);
        }

        public static string SizeToString(Size pt)
        {
            return string.Format("{0},{1}", pt.Width, pt.Height);
        }

        public static Point StringToPoint(string ptStr)
        {
            string[] coords = ptStr.Split(',');
            return new Point(int.Parse(coords[0]), int.Parse(coords[1]));
        }

        public static Size StringToSize(string szStr)
        {
            string[] coords = szStr.Split(',');
            return new Size(int.Parse(coords[0]), int.Parse(coords[1]));
        }

        /// <summary>
        /// 不抛异常的类型转换函数
        ///     通常用在一些不太重要的场合，当转换失败时直接返回一个默认值
        /// </summary>

        public static int ToInt(string literal)
        {
            try
            {
                return int.Parse(literal);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
