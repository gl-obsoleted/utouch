using System;
using System.Collections.Generic;
using System.Drawing;
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
        private Point m_targetPoint;

        public Action_Insert(Node targetNode, Node insertedNode, Point targetPoint)
        {
            m_targetNode = targetNode;
            m_insertedNode = insertedNode;
            m_targetPoint = targetPoint;
        }

        public override void Undo()
        {
            m_targetNode.Detach(m_insertedNode);
        }

        public override void Redo()
        {
            m_insertedNode.SetPositionClamped(m_targetPoint);
            m_targetNode.Attach(m_insertedNode);
        }
    }
}
