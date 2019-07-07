using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace A3ServerTool.ViewModels.ServerSubViewModels
{
    /// <summary>
    /// Represents viewmodel with modifications used on server.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    public class ModificationsViewModel : ViewModelBase
    {
        private readonly ServerViewModel _parentViewModel;

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        /// <summary>
        /// Gets or sets the client modifications.
        /// </summary>
        public ObservableCollection<Modification> Modifications
        {
            get => _mods;
            set
            {
                if (Equals(_mods, value))
                {
                    return;
                }
                _mods = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<Modification> _mods;

        /// <summary>
        /// Gets or sets the selected mod.
        /// </summary>
        public Modification SelectedModification
        {
            get => _selectedMod;
            set
            {
                if (Equals(_selectedMod, value))
                {
                    return;
                }

                _selectedMod = value;
                RaisePropertyChanged();
            }
        }
        private Modification _selectedMod;

        /// <summary>
        /// Gets the refresh command.
        /// </summary>
        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ??
                       (_refreshCommand = new RelayCommand(_ =>
                       {
                           RefreshModifications();
                       }));
            }
        }
        private ICommand _refreshCommand;

        /// <summary>
        /// Sets the actions that will be executed after form will be fully ready to be drawn on screen.
        /// </summary>
        public ICommand WindowLoadedCommand
        {
            get
            {
                return _windowLoadedCommand ??
                       (_windowLoadedCommand = new RelayCommand(_ =>
                       {
                           if (!Modifications.Any())
                           {
                               RefreshModifications();
                           }
                       }));
            }
        }
        private ICommand _windowLoadedCommand;

        public ModificationsViewModel(ServerViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
            _mods = new ObservableCollection<Modification>(CurrentProfile.Modifications);
            //TODO: server Dao
        }

        private void RefreshModifications()
        {
            throw new NotImplementedException();
        }
    }
}
