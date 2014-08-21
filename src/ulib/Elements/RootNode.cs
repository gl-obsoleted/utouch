using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ulib.Base;
using ulib.Elements;

namespace ulib.Elements
{
    public class RootNode : Node
    {
        public bool IsFullscren 
        { 
            get 
            { 
                return m_isFullscreen; 
            } 
            set 
            { 
                m_isFullscreen = value;
                OnFullscreenChanged(); 
            }
        }

        public RootNode()
        {
            base.m_parent = null;
            base.Name = RootNodeConstants.Default_Name;

            OnFullscreenChanged();
        }

        protected void OnFullscreenChanged()
        {
            if (IsFullscren)
            {
                Position = Constants.ZeroPoint;

                System.Drawing.Size size;
                bool ret = ResolutionLut.Table.TryGetValue(RootNodeConstants.Default_Resolution, out size);
                System.Diagnostics.Debug.Assert(ret);
                Size = size;
            }
            else
            {
                Position = RootNodeConstants.Default_Position;
                Size = RootNodeConstants.Default_Size;
            }
        }

        protected bool m_isFullscreen = RootNodeConstants.Default_IsFullscreen;
    }
}
