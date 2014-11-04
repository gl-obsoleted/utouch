using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ucore
{
    /// <summary>
    /// 不抛异常的类型转换函数
    ///     通常用在一些不太重要的场合，当转换失败时直接返回一个默认值
    /// </summary>
    public class EzConv
    {
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
