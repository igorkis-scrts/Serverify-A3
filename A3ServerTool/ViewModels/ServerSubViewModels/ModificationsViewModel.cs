using System.Windows.Forms;

namespace A3ServerTool.ViewModels.ServerSubViewModels;

/// <summary>
/// Represents viewmodel with modifications used on server.
/// </summary>
/// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
public class ModificationsViewModel : ViewModelBase
{
    public const string Token = nameof(ModificationsViewModel);
    
    private readonly ServerViewModel _parentViewModel;
    private readonly GameLocationFinder _locationFinder;
    private readonly IProfileDirector _profileDirector;
    
    private Profile CurrentProfile => _parentViewModel.CurrentProfile;

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
            _selectedMod = value;
            if (_selectedMod != null)
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
        ? Modifications.Count(m => m.IsClientMod) + "/" + Modifications.Count
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
            return _selectAllCommand ??= new RelayCommand(async _ =>
            {
                if (Modifications?.Any() != true) return;

                await Task.Run(() =>
                {
                    foreach (var mod in Modifications)
                    {
                        mod.IsClientMod = true;
                    }
                }).ConfigureAwait(false);

                RaisePropertyChanged(nameof(ModificationsCounter));
            });
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
            return _deselectAllCommand ??= new RelayCommand(async _ =>
            {
                if (Modifications?.Any() != true) return;

                await Task.Run(() =>
                {
                    foreach (var mod in Modifications)
                    {
                        mod.IsClientMod = false;
                    }
                }).ConfigureAwait(false);
                RaisePropertyChanged(nameof(ModificationsCounter));
            });
        }
    }

    private ICommand _deselectAllCommand;

    /// <summary>
    /// Gets the select all command.
    /// </summary>
    public ICommand AddModCommand
    {
        get
        {
            return _addModCommand ??= new RelayCommand(_ =>
            {
                using var folderDialog = new FolderBrowserDialog();

                if (folderDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                var modPath = folderDialog.SelectedPath;
                var isModAlreadyExists = Modifications.Any(um => um.Name == modPath);

                if (isModAlreadyExists)
                {
                    return;
                }
                
                var mod = new Modification
                {
                    Name = modPath,
                    IsAbsolutePathMod = true
                };

                Modifications.Add(mod);
                CurrentProfile.ArgumentSettings.Modifications = new List<Modification>(Modifications);
                
                RaisePropertyChanged(nameof(ModificationsCounter));
            });
        }
    }

    private ICommand _addModCommand;

    /// <summary>
    /// Gets the select all command.
    /// </summary>
    public ICommand RemoveModCommand
    {
        get
        {
            return _removeModCommand ??= new RelayCommand(_ =>
            {
                if (SelectedModification.IsAbsolutePathMod)
                {
                    Modifications.Remove(SelectedModification);
                }
            });
        }
    }

    private ICommand _removeModCommand;


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

    public ModificationsViewModel(ServerViewModel parentViewModel, IProfileDirector profileDirector,
        GameLocationFinder locationFinder)
    {
        _parentViewModel = parentViewModel;
        _mods = new ObservableCollection<Modification>(CurrentProfile.ArgumentSettings.Modifications);
        _mods.CollectionChanged += ModListChanged;

        _profileDirector = profileDirector;
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
                var oldMods = CurrentProfile.ArgumentSettings.Modifications.ConvertAll(mod => mod.Clone());
                _profileDirector.GetProfileModifications(CurrentProfile);

                var updatedMods = CurrentProfile.ArgumentSettings.Modifications;

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

                foreach (var mod in oldMods.Where(x => x.IsAbsolutePathMod))
                {
                    var updatedMod = updatedMods.FirstOrDefault(um => um.Name == mod.Name);
                    if (updatedMod == null)
                    {
                        updatedMods.Add(mod);
                    }
                }

                Modifications = new ObservableCollection<Modification>(updatedMods);
            }
            else
            {
                _profileDirector.GetProfileModifications(CurrentProfile);
                Modifications = new ObservableCollection<Modification>(CurrentProfile.ArgumentSettings.Modifications);
            }
        });
    }

    private void OnModChanged(object sender, EventArgs e)
    {
        Messenger.Default.Send("UpdateFinalString", GeneralViewModel.Token);
        RaisePropertyChanged(nameof(ModificationsCounter));
    }

    /// <summary>
    /// Refreshes modlist by requests from other view models.
    /// </summary>
    /// <param name="request">message to do something in this viewmodel.</param>
    private void DoByRequest(string request)
    {
        RefreshModifications();
    }

    private void ModListChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (!(sender is ObservableCollection<Modification>))
        {
            return;
        }

        CurrentProfile.ArgumentSettings.Modifications =
            new List<Modification>((ObservableCollection<Modification>) sender);
    }
}