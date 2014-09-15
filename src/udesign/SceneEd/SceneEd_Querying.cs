using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace udesign
{
    public delegate bool HasModifierKeyDownHandler(Keys keyCode);

    public partial class SceneEd
    {
        public HasModifierKeyDownHandler HasModifierKeyDown;
    }
}
