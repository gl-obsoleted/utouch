using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace udesign
{
    public class OperationHistory
    {
        public void PushAction(Action a)
        {
            m_actionHistory.Add(a);
            m_redoQueue.Clear();    // 当正在 undo/redo 过程中时，如果有了新的操作，那些没有做的 redo 全部丢弃
        }

        public void Clear()
        {
            m_actionHistory.Clear();
            m_redoQueue.Clear();
        }

        public void Undo()
        {
            if (m_actionHistory.Count == 0)
                return;

            int last = m_actionHistory.Count - 1;
            Action act = m_actionHistory.ElementAt(last);
            if (act != null)
            {
                act.Undo();
            }

            m_actionHistory.RemoveAt(last);
            m_redoQueue.Add(act);
        }

        public void Redo()
        {
            if (m_redoQueue.Count == 0)
                return;

            int last = m_redoQueue.Count - 1;
            Action act = m_redoQueue.ElementAt(last);
            if (act != null)
            {
                act.Redo();
            }

            m_redoQueue.RemoveAt(last);
            m_actionHistory.Add(act);
        }

        private List<Action> m_actionHistory = new List<Action>();
        private List<Action> m_redoQueue = new List<Action>();
    }
}
