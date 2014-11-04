using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ulib.Elements;

namespace udesign
{
    public class SceneEdCmdListener
    {
        public enum Command
        {
            ResizeControlToResourceSize,
        }

        public static void OnCommand(Command cmd)
        {
            switch (cmd)
            {
                case Command.ResizeControlToResourceSize:
                    if (SceneEd.Instance.Selection.Selection.Count == 1)
                    {
                        Node target = SceneEd.Instance.Selection.Selection[0];
                        if (target.GetExpectedResourceSize() != ucore.Const.ZERO_SIZE)
	                    {
                            Action_Resize resizeAction = new Action_Resize(target);
                            Rectangle bound = new Rectangle(target.GetWorldPosition(), target.GetExpectedResourceSize());
                            resizeAction.EndResizing(bound);
                            ActionQueue.Instance.PushAction(resizeAction);
                            SceneEdEventNotifier.Instance.Emit_RefreshScene(RefreshSceneOpt.Refresh_All);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
