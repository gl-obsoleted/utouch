using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace udesign
{
    public class CtrlUtil
    {
        public static void ShowContextMenu(System.Windows.Forms.Control ctrl)
        {
            if (ctrl == null)
                return;

            if (ctrl.ContextMenuStrip == null)
                return;

            ctrl.ContextMenuStrip.Show(ctrl, 0, ctrl.Height);
        }
    }
}
