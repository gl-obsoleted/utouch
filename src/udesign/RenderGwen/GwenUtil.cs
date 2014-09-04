using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace udesign
{
    public static class GwenUtil
    {
        public static void RenderWorldBound(Gwen.Renderer.Tao renderer, Gwen.Control.Base ctrl, bool filled)
        {
            if (ctrl.Parent != null)
            {
                Rectangle rect = new Rectangle(ctrl.Parent.LocalPosToCanvas(new Point(ctrl.X, ctrl.Y)), new Size(ctrl.Width, ctrl.Height));
                if (filled)
                {
                    renderer.DrawFilledRect(rect);
                }
                else
                {
                    renderer.DrawLinedRect(rect);
                }
            }
        }
    }
}
