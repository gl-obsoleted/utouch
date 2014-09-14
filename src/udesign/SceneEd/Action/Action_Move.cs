using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib;
using ulib.Elements;

namespace udesign
{
    public class Action_Move : Action
    {
        public Action_Move(List<Node> sel)
        {
            m_selection = sel.ToArray();
            m_initialPositions = new Point[m_selection.Length];
            for (int i = 0; i < m_selection.Length; ++i)
            {
                m_initialPositions[i] = m_selection[i].Position;
            }
        }

        public void UpdatePosition(Point offset)
        {
            for (int i = 0; i < m_selection.Length; ++i)
            {
                Point loc = m_initialPositions[i];
                loc.X += offset.X;
                loc.Y += offset.Y;

                Node p = m_selection[i].Parent;
                if (p != null)
                    loc = ulib.Base.Math.Clamp(loc, new Rectangle(0, 0,
                        p.IsScrollableH() ? p.LogicalSize.Width : p.Size.Width - m_selection[i].Size.Width,
                        p.IsScrollableV() ? p.LogicalSize.Height : p.Size.Height - m_selection[i].Size.Height));

                m_selection[i].Position = loc;
            }
        }

        public void EndUpdatePosition(Point offset)
        {
            UpdatePosition(offset);

            m_finalPositions = new Point[m_selection.Length];
            for (int i = 0; i < m_selection.Length; ++i)
            {
                m_finalPositions[i] = m_selection[i].Position;
            }
        }

        public Node[] m_selection;
        public Point[] m_initialPositions;
        public Point[] m_finalPositions;

        public override void Undo()
        {
            for (int i = 0; i < m_selection.Length; ++i)
            {
                m_selection[i].Position = m_initialPositions[i];
            }
        }

        public override void Redo()
        {
            for (int i = 0; i < m_selection.Length; ++i)
            {
                m_selection[i].Position = m_finalPositions[i];
            }
        }
    }
}
