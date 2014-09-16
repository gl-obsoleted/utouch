using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace udesign
{
    public static class SceneEdShortcutListener
    {
        public static void OnKeyPressed(Keys shortcutKey)
        {
            switch (shortcutKey)
            {
                case Keys.Control | Keys.Z:
                    SceneEd.Instance.Undo();
                    break;

                case Keys.Control | Keys.Y:
                    SceneEd.Instance.Redo();
                    break;

                case Keys.Control | Keys.X:
                    SceneEd.Instance.Cut();
                    break;

                case Keys.Control | Keys.C:
                    SceneEd.Instance.Copy();
                    break;

                case Keys.Control | Keys.V:
                    SceneEd.Instance.Paste();
                    break;

                case Keys.Delete:
                    SceneEd.Instance.DeleteSelected();
                    break;

                default:
                    break;
            }
        }
    }
}
