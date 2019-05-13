using A3ServerTool.Models;
using GalaSoft.MvvmLight;

namespace A3ServerTool.ViewModels
{
    /// <summary>
    /// Represents viewmodel with modifications used on server.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    public class ModificationsViewModel : ViewModelBase
    {
        private readonly ServerViewModel _parentViewModel;

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        public ModificationsViewModel(ServerViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
        }
    }
}
