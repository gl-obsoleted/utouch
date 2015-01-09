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
using ucore;
using udesign;
using ulib;
using ulib.Elements;

namespace udesign.Controls
{
    public partial class UITaoRenderBuffer : UserControl
    {
        protected GwenRenderContext m_renderContext;
        protected GwenRenderDevice m_renderDevice;

        protected Gwen.Control.Canvas m_canvas;
        protected Gwen.Renderer.Tao m_renderer;
        protected Gwen.Skin.Base m_skin;

        protected Scene m_scene;

        protected Tao.Platform.Windows.SimpleOpenGlControl GLCtrl { get { return glControl; } }

        public UITaoRenderBuffer()
        {
            InitializeComponent();
            glControl.InitializeContexts();
        }

        public void SetScene(Scene scn)
        {
            m_scene = scn;
        }

        public Canvas GetCanvas()
        {
            return m_canvas;
        }

        public Gwen.Renderer.Tao GetRenderer()
        {
            return m_renderer;
        }

        public bool InitContext()
        {
            Gl.glClearColor(1f, 0f, 0f, 1f);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(0, glControl.Width, glControl.Height, 0, -1, 1);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glViewport(0, 0, glControl.Width, glControl.Height);

            string resfile = LuaRuntime.Instance.BootstrapScript.Globals["ResPath_GwenDefaultSkin"] as string;
            if (string.IsNullOrEmpty(resfile))
            {
                Logging.Printf("Error: failed to get global var 'ResPath_GwenDefaultSkin' from script.");
                Logging.Printf("Error: failed to locate the default gwen skin due to error above.");
                return false;
            }
                                                                                          
            m_renderer = new Gwen.Renderer.Tao();
            m_skin = new Gwen.Skin.TexturedBase(m_renderer, resfile);
            m_canvas = new Canvas(m_skin);
            m_canvas.SetSize(glControl.Width, glControl.Height);
            m_canvas.ShouldDrawBackground = true;
            m_canvas.BackgroundColor = Color.FromArgb(255, 150, 170, 170);
            m_canvas.KeyboardInputEnabled = true;
            m_canvas.MouseInputEnabled = true;

            m_renderContext = new GwenRenderContext(m_canvas, m_renderer);
            m_renderDevice = new GwenRenderDevice();

            SceneEd.Instance.InitSelectionContainer(m_canvas);
            glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_Paint);
            glControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);

            return true;
        }

        private void UIRenderBuffer_GL_Tao_Resize(object sender, System.EventArgs e)
        {
            ResetViewport();

            if (m_canvas != null && (m_canvas.Width != glControl.Width || m_canvas.Height != glControl.Height))
            {
                m_canvas.SetSize(glControl.Width, glControl.Height);
            }
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

            PreSceneRender();

            if (m_scene != null)
                m_scene.Render(m_renderContext, m_renderDevice);

            PostSceneRender();

            m_renderer.End();
        }

        protected int prevX = -1;
        protected int prevY = -1;
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            prevX = e.X;
            prevY = e.Y;
            GLCtrl.Invalidate();
        }

        protected virtual void PreSceneRender() { }
        protected virtual void PostSceneRender() { }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            //Logging.DbgPrintf("KeyDown, key {0}", e.KeyCode);
            SceneEdShortcutListener.OnKeyPressed(e.KeyCode | e.Modifiers);
            m_canvas.Input_Key(ConvertKeysToGwenKey(e.KeyCode), true);
            glControl.Invalidate();
        }

        private void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            //Logging.DbgPrintf("KeyUp, key {0}", e.KeyCode);
            SceneEdShortcutListener.OnKeyReleased(e.KeyCode | e.Modifiers);
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
            //Logging.DbgPrintf("KeyPress, key {0}", e.KeyChar);
            m_canvas.Input_Character(e.KeyChar);
            glControl.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //
            // 这里只处理 Up / Down / Left / Right 四个按键的 KeyDown 消息
            //
            // 请注意，正常按键的 KeyDown 消息是在 glControl_KeyDown() 函数中处理的
            // 这四个按键的特殊性解释如下：
            // http://stackoverflow.com/questions/1646998/up-down-left-and-right-arrow-keys-do-not-trigger-keydown-event?rq=1
            // "Microsoft chose to omit these keys from KeyDown events because they affect 
            // multiple controls and move the focus, but this makes it very difficult to 
            // make an app react to these keys in any other way."

            //Logging.DbgPrintf("ProcessCmdKey, msg {0}, key {1}", msg, keyData);
            switch (keyData)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    SceneEdShortcutListener.OnDirectionalKeysPressed(keyData);
                    break;

                default:
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void DisposeUnmanaged()
        {
            //TODO[GL]: 这个对象的 Dispose 内会抛空引用异常，待查
            try
            {
                GwenTextureProvider.Instance.ReleaseAllTextures();

                m_renderContext.Font.Dispose();
                m_canvas.Dispose();
                m_skin.Dispose();
                m_renderer.Dispose();
            }
            catch (NullReferenceException)
            {
            }
        }
    }
}
