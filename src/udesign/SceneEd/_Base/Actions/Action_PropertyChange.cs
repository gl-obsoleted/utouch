using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib.Elements;

namespace udesign
{
    class Action_PropertyChange : Action
    {
        public Action_PropertyChange(Node targetNode)
        {
            m_targetNode = targetNode;
            m_initialContent = NodeJsonUtil.NodeToString(m_targetNode);
        }

        public void ChangeCommitted()
        {
            m_changedContent = NodeJsonUtil.NodeToString(m_targetNode);
        }

        public override void Undo()
        {
            NodeJsonUtil.PopulateExistingNodeWithString(m_targetNode, m_initialContent);
        }

        public override void Redo()
        {
            NodeJsonUtil.PopulateExistingNodeWithString(m_targetNode, m_changedContent);
        }

        private Node m_targetNode;
        private string m_initialContent;
        private string m_changedContent;
    }
}
