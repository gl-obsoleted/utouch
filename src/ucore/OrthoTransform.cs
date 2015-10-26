using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ucore
{
    public struct OrthoTransform
    {
        public static readonly OrthoTransform ZERO = new OrthoTransform { m_translate = { X = 0, Y = 0 }, m_scale = 0 };

        public Vec2 Translate { get { return m_translate; } set { m_translate = value; } }

        public float Scale { get { return m_scale; } set { m_scale = value; } }

        private Vec2 m_translate;
        private float m_scale;

        public OrthoTransform(OrthoTransform ot)
        {
            m_translate = ot.m_translate;
            m_scale = ot.m_scale;
        }

        public Point TransformMouseLocation(Point pt)
        {
            return new Point(pt.X - (int)m_translate.X, pt.Y - (int)m_translate.Y);
        }

        public Rectangle TransformCameraProjection(Rectangle cameraOrthoRect)
        {
            return new Rectangle(
                cameraOrthoRect.X - (int)m_translate.X,
                cameraOrthoRect.Y - (int)m_translate.Y,
                cameraOrthoRect.Width,
                cameraOrthoRect.Height);
        }

        public Rectangle TransformClipRegion(Rectangle clipRegion)
        {
            return new Rectangle(
                clipRegion.X + (int)m_translate.X,
                clipRegion.Y + (int)m_translate.Y,
                clipRegion.Width,
                clipRegion.Height);
        }

        public Rectangle UntransformClipRegion(Rectangle clipRegion)
        {
            return new Rectangle(
                clipRegion.X - (int)m_translate.X,
                clipRegion.Y - (int)m_translate.Y,
                clipRegion.Width,
                clipRegion.Height);
        }
    }
}
