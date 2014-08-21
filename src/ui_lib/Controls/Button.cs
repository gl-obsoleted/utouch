using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ui_lib.Base;
using ui_lib.Elements;

namespace ui_lib.Controls
{
    public class Button : Node
    {
        public Button()
            : base()
        {
            ImageResource ir = ResourceManager.Instance.GetResource(Constants.DefaultAtlasFile, "anniu2changtai.png");
            if (ir != null)
            {
                Size = ir.Size;
            }
            Res_Normal = Utilities.ComposeResURL(Constants.DefaultAtlasFile, "anniu2changtai.png");
            Res_Pressed = Utilities.ComposeResURL(Constants.DefaultAtlasFile, "anniu2hui.png");
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
