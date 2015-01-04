using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ucore
{
    public class SysPost
    {
        public static bool AssertException(bool expr, string msg)
        {
#if DEBUG
            System.Diagnostics.Debug.Assert(expr);
            return expr;
#else
            if (!expr)
            {
                throw new AssertionFailure(msg);
            }
            else
            {
                return true;
            }
#endif
        }
    }
}
