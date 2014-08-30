using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ulib.Base
{
    public class Constants
    {
        public static readonly Point ZeroPoint = new Point { X = 0, Y = 0 };
        public static readonly Rectangle INVALID_RECT = new Rectangle { X = -10000, Y = -10000, Width = 1, Height = 1 };

        public const int INVALID_ID = -1;

        public const string LayoutPostfix = ".ui_layout";

        public const string ResProtocol = "uires://";
        public const string ResIndexFilePostfix = ".txt";
        public const string ResImageFilePostfix = ".png";
        public const char ResDelimeter = ':';

        public static readonly string LibName = "ulib";
    }

    public class Resolution
    {
        public enum Slot
        {
            RT_None,
            RT_800x600,
            RT_1024x768,
        }

        public static readonly Dictionary<Slot, Size> Table = new Dictionary<Slot, Size> {
            { Slot.RT_None, new Size { Width = 0, Height = 0 } },
            { Slot.RT_800x600, new Size { Width = 800, Height = 600 } },
            { Slot.RT_1024x768, new Size { Width = 1024, Height = 768 } },
        };

        public static readonly Slot DefaultSlot = Slot.RT_1024x768;

        public static Size GetDefaultResolution() 
        {
            Size size;
            bool ret = Resolution.Table.TryGetValue(DefaultSlot, out size);
            System.Diagnostics.Debug.Assert(ret);
            return size;
        }
    }

    public enum AlignHori
    {
        Left,
        Center,
        Right,
    }

    public enum AlignVert
    {
        Top,
        Middle,
        Bottom,
    }

    public enum DockType
    {
        None,
        Left,
        Right,
        Top,
        Bottom,
        Center,
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
