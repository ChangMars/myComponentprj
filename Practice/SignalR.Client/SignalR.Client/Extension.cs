using System.Windows.Forms;

namespace SignalR.Client
{
    static class Extension
    {
        public static void InvokeIfNecessary(this System.Windows.Forms.Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
