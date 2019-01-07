using System.ComponentModel;
using MahApps.Metro.Controls;

namespace A3ServerTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            //CloseApplicationCommand handles app exit
            //, ordinary exit mechanism is turned off
            e.Cancel = true;
        }
    }
}
