using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using ulib.Base;
using ulib.Elements;

namespace ulib.Controls
{
    public class Button : Control
    {
        public Button()
        {
            ImageResource ir = ResourceManager.Instance.GetDefaultResource("anniu2changtai.png");
            if (ir != null)
            {
                Size = ir.Size;
            }
            Res_Normal = ResourceManager.Instance.ComposeDefaultResURL("anniu2changtai.png");
            Res_Pressed = ResourceManager.Instance.ComposeDefaultResURL("anniu2hui.png");
                                 
            m_textNode = new TextNode();
            m_textNode.Text = "Button";
            m_textNode.TextColor = Color.White;
            m_textNode.Dock = DockType.Center;
            m_textNode.AlignH = AlignHori.Center;
            m_textNode.AlignV = AlignVert.Middle;
            Attach(m_textNode);
        }

        [Category("Button")]
        public string Res_Normal { get; set; }

        [Category("Button")]
        public string Res_Pressed { get; set; }

        [Category("Button")]
        public string ButtonText { get { return m_textNode.Text; } set { m_textNode.Text = value; } }

        public bool Pressed { get { return m_isPressed; } }
        
        public string Res_Background { get { return m_isPressed ? Res_Pressed : Res_Normal ; } }

        private bool m_isPressed = false;
        private TextNode m_textNode;
    }
}
