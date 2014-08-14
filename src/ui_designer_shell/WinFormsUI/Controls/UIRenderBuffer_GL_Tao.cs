﻿using Gwen;
using Gwen.Control;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Tao.OpenGl;
using ui_designer;
using ui_lib.Elements;

namespace ui_designer_shell.Controls
{
    public partial class UIRenderBuffer_GL_Tao : UserControl
    {
        private DesginerScene m_scene;
        private GwenRenderContext m_renderContext;
        private GwenRenderDevice m_renderDevice;

        private Gwen.Control.Canvas canvas;
        private Gwen.Renderer.Tao renderer;
        private Gwen.Skin.Base skin;

        public UIRenderBuffer_GL_Tao()
        {
            InitializeComponent();
        }

        public void SetScene(DesginerScene scene)
        {
            m_scene = scene;
            glControl.Invalidate();
        }

        public Canvas GetCanvas()
        {
            return canvas;
        }

        public Gwen.Renderer.Tao GetRenderer()
        {
            return renderer;
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

            renderer = new Gwen.Renderer.Tao();
            skin = new Gwen.Skin.TexturedBase(renderer, Path.Combine("media", "DefaultSkin.png"));
            canvas = new Canvas(skin);
            canvas.SetSize(glControl.Width, glControl.Height);
            canvas.ShouldDrawBackground = true;
            canvas.BackgroundColor = Color.FromArgb(255, 150, 170, 170);
            canvas.KeyboardInputEnabled = true;
            canvas.MouseInputEnabled = true;

            m_renderContext = new GwenRenderContext(canvas, renderer);
            m_renderDevice = new GwenRenderDevice();
        }

        private void UIRenderBuffer_GL_Tao_Resize(object sender, System.EventArgs e)
        {
            Gl.glClearColor(1f, 0f, 0f, 1f);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(0, glControl.Width, glControl.Height, 0, -1, 1);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glViewport(0, 0, glControl.Width, glControl.Height);

            if (canvas != null)
                canvas.SetSize(glControl.Width, glControl.Height);
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_COLOR_BUFFER_BIT);
            canvas.RenderCanvas();

            renderer.Begin();
            if (m_scene != null)
                m_scene.Render(m_renderContext, m_renderDevice);

            //float[] c = new float[4];
            //Gl.glGetFloatv(Gl.GL_CURRENT_COLOR, c);
            //Gl.glColor3f(1.0f, 0.0f, 0.0f);
            //Gl.glColor4fv(c);

            foreach (Node n in SceneEd.Instance.Selection)
            {
                Rectangle rect = n.GetWorldBounds();
                rect.Inflate(5, 5);

                Color c = renderer.DrawColor;
                renderer.DrawColor = Color.Red;
                renderer.DrawLinedRect(rect);
                renderer.RenderText(m_renderContext.m_font, 
                    new Point(rect.Left, rect.Top - 15),
                    n.Name + " [" + n.GetType().Name + "]");
                renderer.DrawColor = c;
            }

            renderer.End();
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
            canvas.Input_MouseButton(btn, true);
            SceneEd.Instance.MouseDown(e);
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
            canvas.Input_MouseButton(btn, false);
            SceneEd.Instance.MouseUp(e);
            glControl.Invalidate();
        }

        int prevX = -1;
        int prevY = -1;
        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            canvas.Input_MouseMoved(e.X, e.Y, e.X - prevX, e.Y - prevY);
            prevX = e.X;
            prevY = e.Y;
            SceneEd.Instance.MouseMove(e);
            glControl.Invalidate();
        }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (Keys.LButton | Keys.ShiftKey))
            {
                SceneEd.Instance.IsHoldingCtrl = true;
            }

            canvas.Input_Key(ConvertKeysToGwenKey(e.KeyCode), true);
            glControl.Invalidate();
        }

        private void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (Keys.LButton | Keys.ShiftKey))
            {
                SceneEd.Instance.IsHoldingCtrl = false;
            }

            canvas.Input_Key(ConvertKeysToGwenKey(e.KeyCode), false);
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
            canvas.Input_Character(e.KeyChar);
            glControl.Invalidate();
        }

        private void DisposeUnmanaged()
        {
            //TODO[GL]: 这个对象的 Dispose 内会抛空引用异常，待查
            try
            {
                canvas.Dispose();
                skin.Dispose();
                renderer.Dispose();
            }
            catch (NullReferenceException)
            {
            }
        }
    }
}
