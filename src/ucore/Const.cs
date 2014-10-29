using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ucore
{
    public class Const
    {
        /*
         *  请注意，ZERO_RECT 和 INVALID_RECT 有着不同的用途
         * 
         *  INVALID_RECT 
         *      通常用于 null object 的判断，但逻辑上仍是一个有效矩形（长宽为非零值，但因为坐标关系屏幕上不可见）
         *      这样在一些逻辑处理的场合就可以满足一些逻辑上的约束。
         *      X,Y 坐标用一组比较奇葩的 magic number 是为了与普通的正常矩形降低碰撞的几率，也就是说出现了这样一组值就可以认定是 INVALID_RECT
         *
         *  ZERO_RECT
         *      通常用于序列化的场合。为了紧凑起见，有时我们会在序列化时序列化某一个或某几个分量
         *      一种常见的做法是该分量若不为0就写入磁盘，这样 ZERO_RECT 在这种场合下，可以用来初始化对应的变量
         */
        public static readonly Rectangle ZERO_RECT      = new Rectangle { X = 0, Y = 0, Width = 0, Height = 0 };
        public static readonly Rectangle INVALID_RECT   = new Rectangle { X = -100123, Y = -100123, Width = 1, Height = 1 };
    }
}
