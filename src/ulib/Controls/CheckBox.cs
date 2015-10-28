using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
 
using ulib.Elements;

namespace ulib.Controls
{
    public class CheckBox : Control
    {
        public CheckBox()
        {
            m_text = new TextNode();
            m_text.Position = new Point(MarkSize.Width + INTERVAL, 0);
            m_text.Size = new Size(Size.Width - MarkSize.Width, MarkSize.Height);
            m_text.Text = "Checkbox";
            m_text.TextColor = Color.White;
            m_text.AlignH = AlignHori.Left;
            m_text.AlignV = AlignVert.Middle;
            Attach(m_text);

            Res_Background = ResourceManager.Instance.ComposeDefaultResURL("yulanxiaoguodi.png");
            Res_On = ResourceManager.Instance.ComposeDefaultResURL("duigou.png");
            Res_Off = "";

            Size = new Size(150, 50);
            MarkSize = new Size(DEFAULT_MARK_SIZE, DEFAULT_MARK_SIZE);
        }

        [Category("CheckBox")]
        public string Res_Background { get; set; }

        [Category("CheckBox")]
        public string Res_On { get; set; }

        [Category("CheckBox")]
        public string Res_Off { get; set; }

        [Category("CheckBox")]
        public Size MarkSize 
        { 
            get { return m_markSize; } 
            set 
            { 
                m_markSize = MarkSize; 
                Size = new Size(Size.Width, MarkSize.Height);
                if (m_text != null)
                {
                    m_text.Position = new Point(MarkSize.Width + INTERVAL, 0);
                    m_text.Size = new Size(Size.Width - MarkSize.Width - INTERVAL, MarkSize.Height);
                }
            }
        }

        [Category("CheckBox")]
        public string DescText { get { return m_text.Text; } set { m_text.Text = value; } }

        public bool Toggled { get { return m_toggled; } }

        public string Res_Mark { get { return m_toggled ? Res_On : Res_Off; } }

        private bool m_toggled = false;
        private Size m_markSize = new Size(DEFAULT_MARK_SIZE, DEFAULT_MARK_SIZE);

        private TextNode m_text;

        private static int DEFAULT_MARK_SIZE = 20;
        private static int INTERVAL = 8;
    }
}
