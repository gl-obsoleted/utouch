using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_lib.Base
{
    public class Constants
    {
        public static readonly Point ZeroPoint = new Point { X = 0, Y = 0 };
    }

    public class ResolutionLut
    {
        public static readonly Dictionary<Resolution, Size> Table = new Dictionary<Resolution, Size> {
            { Resolution.RT_800x600, new Size { Width = 800, Height = 600 } },
            { Resolution.RT_1024x768, new Size { Width = 1024, Height = 768 } },
        };
    }

    public class RootNodeConstants
    {
        public static readonly bool Default_IsFullscreen = false;
        public static readonly Point Default_Position = new Point { X = 100, Y = 100 };
        public static readonly Size Default_Size = new Size { Width = 300, Height = 300 };
        public static readonly Resolution Default_Resolution = Resolution.RT_800x600;
        public static readonly string Default_Name = "Root";
    }
}
