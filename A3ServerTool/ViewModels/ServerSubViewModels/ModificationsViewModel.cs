using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using A3ServerTool.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;
using A3ServerTool.Models.Profile;
using A3ServerTool.Storage;
using GalaSoft.MvvmLight.Messaging;

namespace A3ServerTool.ViewModels.ServerSubViewModels
{
    /// <summary>
    /// Represents viewmodel with modifications used on server.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    public class ModificationsViewModel : ViewModelBase
    {
        public const string Token = nameof(ModificationsViewModel);

        private readonly ServerViewModel _parentViewModel;
        private readonly GameLocationFinder _locationFinder;
        private readonly IDao<Modification> _modDao;

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
        private ObservableCollection<Modification> _mods = new ObservableCollection<Modification>();

        /// <summary>
        /// Gets or sets the selected mod.
        /// </summary>
        public Modification SelectedModification
        {
            get => _selectedMod;
            set
            {
                _selectedMod = value;
                if(_selectedMod != null)
                {
                    _selectedMod.PropertyChanged += OnModChanged;
                }

                RaisePropertyChanged();
            }
        }
        private Modification _selectedMod;

        /// <summary>
        /// Gets the modifications counter.
        /// </summary>
        public string ModificationsCounter => Modifications != null
            ? Modifications.Count(m => m.IsClientMod) + "/" + Modifications.Count()
            : string.Empty;

        /// <summary>
        /// Gets the refresh command.
        /// </summary>
        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ??= new RelayCommand(async _ =>
                {
                    await RefreshModifications().ConfigureAwait(false);
                });
            }
        }
        private ICommand _refreshCommand;

        /// <summary>
        /// Gets the select all command.
        /// </summary>
        public ICommand SelectAllCommand
        {
            get
            {
                return _selectAllCommand ??
                       (_selectAllCommand = new RelayCommand(async _ =>
                       {
                           if(Modifications?.Any() == true)
                           {
                               await Task.Run(() =>
                               {
                                   foreach(var mod in Modifications)
                                   {
                                       mod.IsClientMod = true;
                                   }
                               }).ConfigureAwait(false);
                               RaisePropertyChanged("ModificationsCounter");
                           }
                       }));
            }
        }
        private ICommand _selectAllCommand;

        /// <summary>
        /// Gets the deselect all command.
        /// </summary>
        public ICommand DeselectAllCommand
        {
            get
            {
                return _deselectAllCommand ??
                       (_deselectAllCommand = new RelayCommand(async _ =>
                       {
                           if (Modifications?.Any() == true)
                           {
                               await Task.Run(() =>
                               {
                                   foreach (var mod in Modifications)
                                   {
                                       mod.IsClientMod = false;
                                   }
                               }).ConfigureAwait(false);
                               RaisePropertyChanged("ModificationsCounter");
                           }
                       }));
            }
        }
        private ICommand _deselectAllCommand;


        /// <summary>
        /// Sets the actions that will be executed after form will be fully ready to be drawn on screen.
        /// </summary>
        public ICommand WindowLoadedCommand
        {
            get
            {
                return _windowLoadedCommand ??= new RelayCommand(async _ =>
                {
                    if (!Modifications.Any())
                    {
                        await RefreshModifications().ConfigureAwait(false);
                    }
                });
            }
        }
        private ICommand _windowLoadedCommand;

        public ModificationsViewModel(ServerViewModel parentViewModel, IDao<Modification> modDao,
            GameLocationFinder locationFinder)
        {
            _parentViewModel = parentViewModel;
            _mods = new ObservableCollection<Modification>(CurrentProfile.ArgumentSettings.Modifications);
            _modDao = modDao;
            _locationFinder = locationFinder;

            Messenger.Default.Register<string>(this, Token, DoByRequest);
        }

        private Task RefreshModifications()
        {
            var gamePath = _locationFinder.GetGameInstallationPath(CurrentProfile);
            if (string.IsNullOrWhiteSpace(gamePath))
            {
                Modifications.Clear();
                return Task.FromResult<object>(null);
            }

            return Task.Run(() =>
            {
                if (Modifications != null)
                {
                    var oldMods = new List<Modification>(Modifications);
                    var updatedMods = new List<Modification>(_modDao.GetAll(gamePath));

                    foreach (var mod in updatedMods)
                    {
                        var oldMod = oldMods.FirstOrDefault(m => m.Name == mod.Name);

                        if (oldMod?.IsClientMod == true)
                        {
                            mod.IsClientMod = true;
                        }

                        if (oldMod?.IsServerMod == true)
                        {
                            mod.IsServerMod = true;
                        }
                    }

                    //it looks like that observable collection created by this way
                    //contains link to CurrentProfile.ArgumentSettings.Modifications
                    //which allows us to store data in the config file without additional code
                    //this fact looks suspicious and might lead to an additional bugs
                    CurrentProfile.ArgumentSettings.Modifications = updatedMods;
                    Modifications = new ObservableCollection<Modification>(CurrentProfile.ArgumentSettings.Modifications);
                }
                else
                {
                    var mods = _modDao.GetAll(gamePath);
                    CurrentProfile.ArgumentSettings.Modifications = mods.ToList();
                    Modifications = new ObservableCollection<Modification>(CurrentProfile.ArgumentSettings.Modifications);
                }
            });
        }

        private void OnModChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("ModificationsCounter");
        }

        /// <summary>
        /// Refreshes modlist by requests from other view models.
        /// </summary>
        /// <param name="request">message to do something in this viewmodel.</param>
        private void DoByRequest(string request)
        {
            RefreshModifications();
        }
    }
}
