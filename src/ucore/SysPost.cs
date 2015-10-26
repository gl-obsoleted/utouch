using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ucore
{
    public class SysPost
    {
        public static bool AssertException(bool expr, string msg)
        {
#if DEBUG
            System.Diagnostics.Debug.Assert(expr);
            return expr;
#else
            if (!expr)
            {
                throw new AssertionFailure(msg);
            }
            else
            {
                return true;
            }
#endif
        }

        public delegate void StdMulticastDelegation(object sender, EventArgs e);

        static EventArgs DummyEventObject = new EventArgs();
        public static void InvokeMulticast(object sender, MulticastDelegate md)
        {
            if (md != null)
            {
                InvokeMulticast(sender, md, DummyEventObject);
            }
        }

        public static void InvokeMulticast(object sender, MulticastDelegate md, EventArgs e)
        {
            if (md == null)
                return;

            foreach (StdMulticastDelegation Caster in md.GetInvocationList())
            {
                ISynchronizeInvoke SyncInvoke = Caster.Target as ISynchronizeInvoke;
                try
                {
                    if (SyncInvoke != null && SyncInvoke.InvokeRequired)
                        SyncInvoke.Invoke(md, new object[] { sender, e });
                    else
                        Caster(sender, e);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Event handling failed. \n");
                    Console.WriteLine("{0}:\n", ex.ToString());
                }
            }
        }
    }
}
