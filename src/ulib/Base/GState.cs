using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ulib.Base
{
    public static class GState
    {
        public static bool IsInLoadingProcess { get; set; }

        public static string AssetRoot { get; set; }
    }

    public class GScope_LoadingProcess : IDisposable
    {
        public GScope_LoadingProcess()
        {
            ucore.SysPost.AssertException(!GState.IsInLoadingProcess, "<unexpected_behavior> 一个 LoadingProcess 尚未结束时，开启了另一个 LoadingProcess。目前不允许加载进程的递归调用。");

            GState.IsInLoadingProcess = true;
        }

        public void Dispose()
        {
            ucore.SysPost.AssertException(GState.IsInLoadingProcess, "<unexpected_behavior> LoadingProcess 结束前，GState.IsInLoadingProcess 已被置为 false。");

            GState.IsInLoadingProcess = false;
        }
    }
}
