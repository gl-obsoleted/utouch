using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ucore;
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
            if (e.Button == MouseButtons.Middle)
            {
                m_isMiddleDown = true;
                BeginOrthoTransform(e.Location);
            }
            else if (!m_canvas.Input_MouseButton(ToGwenMouseButton(e.Button), true))
            {
                m_sceneEd.MouseDown(e.Button, m_cameraTransform.TransformMouseLocation(e.Location));
            }

            GLCtrl.Invalidate();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                UpdateOrthoTransform(e.Location);
                m_isMiddleDown = false;
            }
            else if (!m_canvas.Input_MouseButton(ToGwenMouseButton(e.Button), false))
            {
                m_sceneEd.MouseUp(e.Button, m_cameraTransform.TransformMouseLocation(e.Location));
            }

            GLCtrl.Invalidate();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (m_isMiddleDown)
            {
                UpdateOrthoTransform(e.Location);
            }
            else
            {
                Point mouseLoc = m_cameraTransform.TransformMouseLocation(e.Location);
                if (!m_canvas.Input_MouseMoved(mouseLoc.X, mouseLoc.Y, mouseLoc.X - prevX, mouseLoc.Y - prevY))
                {
                    m_sceneEd.MouseMove(mouseLoc);
                }
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

        private int ToGwenMouseButton(MouseButtons Button)
        {
            int btn = -1;
            if (Button == MouseButtons.Left)
            {
                btn = 0;
            }
            else if (Button == MouseButtons.Right)
            {
                btn = 1;
            }
            return btn;
        }

        private bool m_isMiddleDown = false;
        private Point m_middleDownInitalPos = Const.ZERO_POINT;
        private OrthoTransform m_middleDownInitalCameraTrans = new OrthoTransform();

        private void BeginOrthoTransform(Point beginMouseLocation)
        {
            m_middleDownInitalPos = beginMouseLocation;
            m_middleDownInitalCameraTrans = m_cameraTransform;
        }

        private void UpdateOrthoTransform(Point newMouseLocation)
        {
            m_cameraTransform.Translate = new Vec2(
                m_middleDownInitalCameraTrans.Translate.X + newMouseLocation.X - m_middleDownInitalPos.X,
                m_middleDownInitalCameraTrans.Translate.Y + newMouseLocation.Y - m_middleDownInitalPos.Y);
        }

        private void EndOrthoTransform(Point endMouseLocation)
        {
            UpdateOrthoTransform(endMouseLocation);

            m_middleDownInitalPos = Const.ZERO_POINT;
            m_middleDownInitalCameraTrans = OrthoTransform.ZERO;
        }

        private SceneEd m_sceneEd;
    }
}
