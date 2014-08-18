using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ui_lib.Elements;

namespace ui_designer_shell
{
    public class Action_Insert : Action
    {
        private Node m_targetNode;
        private Node m_insertedNode;

        public Action_Insert(Node targetNode, Node insertedNode)
        {
            m_targetNode = targetNode;
            m_insertedNode = insertedNode;
        }

        public override void Undo()
        {
            m_targetNode.Detach(m_insertedNode);
        }

        public override void Redo()
        {
            m_targetNode.Attach(m_insertedNode);
        }
    }
}
