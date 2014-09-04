using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib.Elements;

namespace udesign
{
    public class SelectionContainer
    {
        public List<Node> Selection { get { return m_selection; } }

        public void Init(Gwen.Control.Canvas canvas)
        {
            m_resizeCtrl = new Gwen.Control.ResizableControl(canvas);
            m_resizeCtrl.SetPosition(50, 50);
            m_resizeCtrl.SetSize(50, 50);
        }

        public void ClearSelection()
        {
            m_selection.Clear();

            SceneEdEventNotifier.Instance.Emit_SelectNode(null, this);
            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
        }

        public bool SelectedRoot()
        {
            foreach (Node n in Selection)
            {
                if (n.Parent == null)
                    return true;
            }
            return false;
        }

        public bool IsSelectionDraggable()
        {
            foreach (var n in m_selection)
            {
                if (n is RootNode)
                {
                    RootNode r = n as RootNode;
                    if (r != null && r.IsFullscren)
                    {
                        return false;
                    }
                }
                else if (NodeSGUtil.HasLockedLayoutParent(n))
                {
                    return false;
                }
            }

            return true;
        }

        private List<Node> m_selection = new List<Node>();

        private Gwen.Control.ResizableControl m_resizeCtrl;
    }
}
