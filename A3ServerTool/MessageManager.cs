using System.Diagnostics;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace A3ServerTool
{
    public class MessageManager
    {
        public async void ShowMessage(string message)
        {
            Trace.TraceError(message);
            await((MetroWindow)System.Windows.Application.Current.MainWindow).ShowMessageAsync("Warning!", message);
        }
    }
}
