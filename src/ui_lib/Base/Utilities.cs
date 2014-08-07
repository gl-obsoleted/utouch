using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ui_lib.Base
{
    public class Utilities
    {
        public static bool Implements(Type sourceClass, Type targetInterface)
        {
            if (!sourceClass.IsClass || !targetInterface.IsInterface)
                return false;

            return sourceClass.GetInterface(targetInterface.FullName) != null;
        }
    }
}
