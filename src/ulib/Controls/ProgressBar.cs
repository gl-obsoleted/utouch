using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using ulib.Elements;

namespace ulib.Controls
{
    public class ProgressBar : Control
    {
        public ProgressBar()
        {
            m_text = new TextNode();
            m_text.TextColor = Color.White;
            m_text.Dock = DockType.Center;
            m_text.AlignH = AlignHori.Center;
            m_text.AlignV = AlignVert.Middle;
            Attach(m_text);

            Res_Progress = ResourceManager.Instance.ComposeDefaultResURL("yulanxiaoguodi.png");

            Size = new Size(250, 50);

            UpdateText();
        }

        [Category("ProgressBar")]
        public string Res_Progress { get; set; }

        [Category("ProgressBar")]
        public Size DescTextSize { get { return m_text.Size; } set { m_text.Size = value; } }


        public string Text { get { return m_text.Text; } }
        public int Accumulated { get { return m_accumulated; } set { m_accumulated = value; UpdateText(); } }
        public int Total { get { return m_total; } set { m_total = value; UpdateText(); } }
        private int m_accumulated = 0;
        private int m_total = 100;
        private void UpdateText() { m_text.Text = string.Format("{0} / {1}", m_accumulated, m_total); }
        private TextNode m_text;

        public override System.Drawing.Size GetExpectedResourceSize()
        {
            return ResourceManager.Instance.GetResourceSize(Res_Progress);
        }
    }
}
