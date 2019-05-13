using A3ServerTool.Models;
using GalaSoft.MvvmLight;

namespace A3ServerTool.ViewModels.ServerSubViewModels
{
    /// <summary>
    /// Represents view model with server difficulty settings.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    public class DifficultyViewModel : ViewModelBase
    {
        private readonly ServerViewModel _parentViewModel;

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        public DifficultyViewModel(ServerViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
        }
    }
}
