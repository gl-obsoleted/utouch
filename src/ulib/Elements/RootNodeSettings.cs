using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ulib.Base;

namespace ulib.Elements
{
    public class RootNodeSettings
    {
        public Resolution.Slot EditTimeResSlot { get { return m_editTimeResSlot; } }
        public Size EditTimeResolution { get { return m_editTimeResolution; } }

        public RootNodeSettings()
        {
            SetSlot(Resolution.DefaultSlot);
        }

        public void SetSlot(Resolution.Slot s)
        {
            if (s == Resolution.Slot.RT_None)
            {
                s = Resolution.Slot.RT_800x600;                
            }

            m_editTimeResSlot = s;
            m_editTimeResolution = Resolution.Table[s];
        }

        private Resolution.Slot m_editTimeResSlot;
        private Size m_editTimeResolution;
    }
}
