using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using udesign.Controls;
using ulib;

namespace udesign
{
    public class EditorRenderBuffer : UITaoRenderBuffer
    {
        public EditorRenderBuffer() : base()
        {
            GLCtrl.DragDrop += new System.Windows.Forms.DragEventHandler(this.glControl_DragDrop);
            GLCtrl.DragEnter += new System.Windows.Forms.DragEventHandler(this.glControl_DragEnter);
            GLCtrl.DragOver += new System.Windows.Forms.DragEventHandler(this.glControl_DragOver);
            GLCtrl.DragLeave += new System.EventHandler(this.glControl_DragLeave);

            GLCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            GLCtrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            GLCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
        }

        public void SetSceneEd(SceneEd scnEd)
        {
            m_sceneEd = scnEd;
        }

        protected override void PostSceneRender()
        {
            if (m_sceneEd != null)
                m_sceneEd.Render(m_renderContext);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            int btn = -1;
            if (e.Button == MouseButtons.Left)
            {
                btn = 0;
            }
            else if (e.Button == MouseButtons.Right)
            {
                btn = 1;
            }
            if (!m_canvas.Input_MouseButton(btn, true))
            {
                m_sceneEd.MouseDown(e);
            }
            GLCtrl.Invalidate();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            int btn = -1;
            if (e.Button == MouseButtons.Left)
            {
                btn = 0;
            }
            else if (e.Button == MouseButtons.Right)
            {
                btn = 1;
            }
            if (!m_canvas.Input_MouseButton(btn, false))
            {
                m_sceneEd.MouseUp(e);
            }
            GLCtrl.Invalidate();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            bool handled = m_canvas.Input_MouseMoved(e.X, e.Y, e.X - prevX, e.Y - prevY);

            if (!handled)
            {
                m_sceneEd.MouseMove(e);
            }
        }

        private void glControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void glControl_DragDrop(object sender, DragEventArgs e)
        {
            if (e.AllowedEffect == e.Effect && e.Data.GetDataPresent(DataFormats.Text))
            {
                string dragInfo = e.Data.GetData(DataFormats.Text).ToString();
                Session.Log("Dragging object {0} is dropped at {1}, {2}", dragInfo, e.X, e.Y);
                Point clientPos = GLCtrl.PointToClient(new Point(e.X, e.Y));
                m_sceneEd.DragAndDrop.NotifyDroppped(clientPos.X, clientPos.Y, dragInfo);
            }
        }

        private void glControl_DragLeave(object sender, EventArgs e)
        {
            m_sceneEd.DragAndDrop.NotifyLeft();
        }

        private void glControl_DragOver(object sender, DragEventArgs e)
        {
            if (e.AllowedEffect == e.Effect && e.Data.GetDataPresent(DataFormats.Text))
            {
                Point clientPos = GLCtrl.PointToClient(new Point(e.X, e.Y));
                m_sceneEd.DragAndDrop.NotifyUpdated(clientPos.X, clientPos.Y);
            }
        }

        private SceneEd m_sceneEd;
    }
}
