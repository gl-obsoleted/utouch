using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using ulib.Base;
using ulib.Elements;

namespace ulib.Elements
{
    public class Button : Node
    {
        public Button()
            : base()
        {
            ImageResource ir = ResourceManager.Instance.GetDefaultResource("anniu2changtai.png");
            if (ir != null)
            {
                Size = ir.Size;
            }
            Res_Normal = ResourceManager.Instance.ComposeDefaultResURL("anniu2changtai.png");
            Res_Pressed = ResourceManager.Instance.ComposeDefaultResURL("anniu2hui.png");

            LockChildrenLayoutRecursively = true;

            TextNode tn = new TextNode();
            tn.Text = "Button";
            tn.TextColor = Color.White;
            tn.Dock = DockType.Center;
            tn.AlignH = AlignHori.Center;
            tn.AlignV = AlignVert.Middle;
            Attach(tn);
        }

        [Category("Button")]
        public string Res_Normal { get; set; }

        [Category("Button")]
        public string Res_Pressed { get; set; }

        public bool Pressed { get { return m_isPressed; } }
        
        public string BackgroundResLocation { get { return m_isPressed ? Res_Pressed : Res_Normal ; } }

        private bool m_isPressed = false;
    }
}
