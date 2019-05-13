using A3ServerTool.Models;
using GalaSoft.MvvmLight;

namespace A3ServerTool.ViewModels.ServerSubViewModels
{
    /// <summary>
    /// Represents view model with mission list.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    public class MissionsViewModel : ViewModelBase
    {
        private readonly ServerViewModel _parentViewModel;

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        public MissionsViewModel(ServerViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
        }
    }
}
