using Gwen;
using Gwen.Control;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Tao.OpenGl;
using udesign;
using ulib;
using ulib.Elements;

namespace udesign.Controls
{
    public partial class UIRenderBuffer_GL_Tao : UserControl
    {
        private GwenRenderContext m_renderContext;
        private GwenRenderDevice m_renderDevice;

        private Gwen.Control.Canvas m_canvas;
        private Gwen.Renderer.Tao m_renderer;
        private Gwen.Skin.Base m_skin;

        private Scene m_scene;
        private SceneEd m_sceneEd;

        public UIRenderBuffer_GL_Tao()
        {
            InitializeComponent();
        }

        public void SetScene(Scene scn)
        {
            m_scene = scn;
        }

        public void SetSceneEd(SceneEd scnEd)
        {
            m_sceneEd = scnEd;
        }

        public Canvas GetCanvas()
        {
            return m_canvas;
        }

        public Gwen.Renderer.Tao GetRenderer()
        {
            return m_renderer;
        }

        private void UIRenderBuffer_GL_Tao_Load(object sender, System.EventArgs e)
        {
            glControl.InitializeContexts();
            Gl.glClearColor(1f, 0f, 0f, 1f);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(0, glControl.Width, glControl.Height, 0, -1, 1);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glViewport(0, 0, glControl.Width, glControl.Height);

            m_renderer = new Gwen.Renderer.Tao();
            m_skin = new Gwen.Skin.TexturedBase(m_renderer, Properties.Settings.Default.GwenMediaFile);
            m_canvas = new Canvas(m_skin);
            m_canvas.SetSize(glControl.Width, glControl.Height);
            m_canvas.ShouldDrawBackground = true;
            m_canvas.BackgroundColor = Color.FromArgb(255, 150, 170, 170);
            m_canvas.KeyboardInputEnabled = true;
            m_canvas.MouseInputEnabled = true;

            m_renderContext = new GwenRenderContext(m_canvas, m_renderer);
            m_renderDevice = new GwenRenderDevice();

            SceneEd.Instance.InitSelectionContainer(m_canvas);
        }

        private void UIRenderBuffer_GL_Tao_Resize(object sender, System.EventArgs e)
        {
            ResetViewport();

            if (m_canvas != null && (m_canvas.Width != glControl.Width || m_canvas.Height != glControl.Height))
                m_canvas.SetSize(glControl.Width, glControl.Height);
        }

        private void ResetViewport()
        {
            Gl.glClearColor(1f, 0f, 0f, 1f);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(0, glControl.Width, glControl.Height, 0, -1, 1);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glViewport(0, 0, glControl.Width, glControl.Height);
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            ResetViewport();
            Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_COLOR_BUFFER_BIT);
            m_canvas.RenderCanvas();

            m_renderContext.CurrentMousePos = new Point(prevX, prevY);

            m_renderer.Begin();

            if (m_scene != null)
                m_scene.Render(m_renderContext, m_renderDevice);

            if (m_sceneEd != null)
                m_sceneEd.Render(m_renderContext);

            m_renderer.End();
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
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
                SceneEd.Instance.MouseDown(e);
            }
            glControl.Invalidate();
        }

        private void glControl_MouseUp(object sender, MouseEventArgs e)
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
                SceneEd.Instance.MouseUp(e);
            }
            glControl.Invalidate();
        }

        int prevX = -1;
        int prevY = -1;
        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            bool handled = m_canvas.Input_MouseMoved(e.X, e.Y, e.X - prevX, e.Y - prevY);
            prevX = e.X;
            prevY = e.Y;

            if (!handled)
            {
                SceneEd.Instance.MouseMove(e);
            }
            glControl.Invalidate();
        }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            m_canvas.Input_Key(ConvertKeysToGwenKey(e.KeyCode), true);
            glControl.Invalidate();
        }

        private void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            SceneEdShortcutListener.OnKeyPressed(e.KeyCode | e.Modifiers);

            m_canvas.Input_Key(ConvertKeysToGwenKey(e.KeyCode), false);
            glControl.Invalidate();
        }

        private Key ConvertKeysToGwenKey(Keys keys)
        {
            switch (keys)
            {
                case Keys.Alt:
                    return Key.Alt;

                case Keys.Back:
                    return Key.Backspace;

                case Keys.Control:
                case Keys.LControlKey:
                case Keys.RControlKey:
                    return Key.Control;

                case Keys.Delete:
                    return Key.Delete;

                case Keys.Down:
                    return Key.Down;

                case Keys.End:
                    return Key.End;

                case Keys.Escape:
                    return Key.Escape;

                case Keys.Home:
                    return Key.Home;

                case Keys.Left:
                    return Key.Left;

                case Keys.Return:
                    return Key.Return;

                case Keys.Right:
                    return Key.Right;

                case Keys.Shift:
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    return Key.Shift;

                case Keys.Space:
                    return Key.Space;

                case Keys.Tab:
                    return Key.Tab;

                case Keys.Up:
                    return Key.Up;

                default:
                    return Key.Invalid;
            }
        }

        private void glControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            m_canvas.Input_Character(e.KeyChar);
            glControl.Invalidate();
        }

        private void DisposeUnmanaged()
        {
            //TODO[GL]: 这个对象的 Dispose 内会抛空引用异常，待查
            try
            {
                m_renderContext.Font.Dispose();
                m_canvas.Dispose();
                m_skin.Dispose();
                m_renderer.Dispose();
            }
            catch (NullReferenceException)
            {
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
                Point clientPos = glControl.PointToClient(new Point(e.X, e.Y));
                SceneEd.Instance.DragAndDrop.NotifyDroppped(clientPos.X, clientPos.Y, dragInfo);
            }
        }

        private void glControl_DragLeave(object sender, EventArgs e)
        {
            SceneEd.Instance.DragAndDrop.NotifyLeft();
        }

        private void glControl_DragOver(object sender, DragEventArgs e)
        {
            if (e.AllowedEffect == e.Effect && e.Data.GetDataPresent(DataFormats.Text))
            {
                Point clientPos = glControl.PointToClient(new Point(e.X, e.Y));
                SceneEd.Instance.DragAndDrop.NotifyUpdated(clientPos.X, clientPos.Y);
            }
        }
    }
}
