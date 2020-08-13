using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace A3ServerTool.Views
{
    public partial class AppHamburgerMenu : UserControl
    {
        public AppHamburgerMenu()
        {
            InitializeComponent();
        }
        
        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            //this.HamburgerMenuControl.Content = e.InvokedItem;
        
            if (!e.IsItemOptions && this.HamburgerMenuControl.IsPaneOpen)
            {
                // close the menu if a item was selected
                this.HamburgerMenuControl.IsPaneOpen = false;
            }
        }

        private void HamburgerMenuControl_OnOptionsItemClick(object sender, ItemClickEventArgs e)
        {
            //this.HamburgerMenuControl.Content = e.ClickedItem;
        
            if (this.HamburgerMenuControl.IsPaneOpen)
            {
                // close the menu if a item was selected
                this.HamburgerMenuControl.IsPaneOpen = false;
            }
        }
    }
}