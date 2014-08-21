using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Elements;

namespace ui_designer_shell
{
    public class Action_Delete : Action
    {
        private Node[] m_parents;
        private Node[] m_deleted;

        public Action_Delete(List<Node> sel)
        {
            m_deleted = sel.ToArray();
            m_parents = new Node[m_deleted.Length];
            for (int i = 0; i < m_deleted.Length; ++i)
            {
                m_parents[i] = m_deleted[i].Parent;
                if (m_parents[i] != null)
                {
                    m_parents[i].Detach(m_deleted[i]);
                }
            }
        }

        public override void Undo()
        {
            for (int i = 0; i < m_deleted.Length; ++i)
            {
                if (m_parents[i] != null)
                {
                    m_parents[i].Attach(m_deleted[i]);
                }
            }
        }

        public override void Redo()
        {
            for (int i = 0; i < m_deleted.Length; ++i)
            {
                if (m_parents[i] != null)
                {
                    m_parents[i].Detach(m_deleted[i]);
                }
            }
        }
    }
}
