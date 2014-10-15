using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using udesign.Controls;

namespace udesign
{
    public class PreviewRenderBuffer : UITaoRenderBuffer
    {
        public PreviewRenderBuffer()
            : base()
        {
            GLCtrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
        }

        protected override void PreSceneRender()
        {
            m_renderer.DrawColor = Color.FromArgb(255, 130, 150, 150);
            m_renderer.DrawFilledRect(new Rectangle(0, 0, ResWidth, ResHeight));
        }

        protected override void PostSceneRender()
        {
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
        }

        public int ResWidth { get; set; }
        public int ResHeight { get; set; }
    }
}
