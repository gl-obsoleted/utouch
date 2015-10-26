using Newtonsoft.Json;
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
    public class RootNode : Node
    {
        [Category("根节点")]
        [DisplayName("是否全屏")]
        public bool IsFullscren 
        { 
            get 
            { 
                return m_isFullscreen; 
            }
        }

        [Category("根节点")]
        [DisplayName("设计时分辨率")]
        public Size DesignTimeResolution { get; set; }

        [Browsable(false)]
        public AssetLut Assets { get; set; }

        public RootNode()
        {
            base.m_parent = null;
            base.Name = Default_Name;

            Position = ucore.Const.ZERO_POINT;
            Size = new System.Drawing.Size(Scene.Instance.DesignTimeResolution.width, Scene.Instance.DesignTimeResolution.height);

            Assets = new AssetLut();
            Assets.AssetRoot = GState.AssetRoot;
        }

        public override bool IsResizable()
        {
            return !IsFullscren && !Locked;
        }

        protected bool m_isFullscreen = true;

        public static readonly Point Default_Position = new Point { X = 50, Y = 30 };
        public static readonly Size Default_Size = new Size { Width = 500, Height = 500 };
        public static readonly string Default_Name = "Root";
    }
}
