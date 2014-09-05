using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace udesign
{
    public partial class GwenUnitTestForm : Form
    {
        public GwenUnitTestForm()
        {
            InitializeComponent();

            m_glCtrl = new Controls.UIRenderBuffer_GL_Tao();
            m_glCtrl.Dock = DockStyle.Fill;
            this.Controls.Add(m_glCtrl);

            ftime = new List<long>();
            stopwatch = new Stopwatch();
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            if (ftime.Count == fps_frames)
                ftime.RemoveAt(0);

            ftime.Add(stopwatch.ElapsedMilliseconds - lastTime);
            lastTime = stopwatch.ElapsedMilliseconds;

            if (stopwatch.ElapsedMilliseconds > 1000)
            {
                Gwen.Renderer.Tao renderer = m_glCtrl.GetRenderer();

                test.Note = String.Format("String Cache size: {0}", renderer.TextCacheSize);
                test.Fps = 1000f * ftime.Count / ftime.Sum();
                stopwatch.Restart();

                if (renderer.TextCacheSize > 1000) // each cached string is an allocated texture, flush the cache once in a while in your real project
                    renderer.FlushTextCache();
            }
        }

        private Controls.UIRenderBuffer_GL_Tao m_glCtrl;
        private Gwen.UnitTest.UnitTest test;
        const int fps_frames = 50;
        private readonly List<long> ftime;
        private readonly Stopwatch stopwatch;
        private long lastTime;

        private void GwenUnitTestForm_Load(object sender, EventArgs e)
        {
            // 构造函数内 Canvas 还没初始化，所以挪到这里做
            test = new Gwen.UnitTest.UnitTest(m_glCtrl.GetCanvas());

            m_glCtrl.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_Paint);

            stopwatch.Restart();
            lastTime = 0;
        }
    }
}
