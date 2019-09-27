using A3ServerTool.Enums;
using A3ServerTool.Helpers;
using A3ServerTool.Models;
using A3ServerTool.Storage;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Interchangeable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace A3ServerTool.ViewModels.ServerSubViewModels
{
    /// <summary>
    /// Represents view model with mission list.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    public class MissionsViewModel : ViewModelBase
    {
        public const string Token = nameof(MissionsViewModel);

        private readonly ServerViewModel _parentViewModel;
        private readonly IDao<Mission> _missionDao;
        private readonly GameLocationFinder _locationFinder;
        private readonly SynchronizationContext _uiContext = SynchronizationContext.Current;

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        /// <summary>
        /// Gets or sets the forced difficulty.
        /// </summary>
        public DifficultyType ForcedDifficulty
        {
            get
            {
                Enum.TryParse(CurrentProfile.ServerConfig.ForcedDifficulty.FirstLetterToUpperCase(), out DifficultyType difficulty);
                if(difficulty == DifficultyType.None)
                {
                    return DifficultyType.Recruit;
                }
                return difficulty;
            }
            set
            {
                if (Equals(value.ToString(), CurrentProfile.ServerConfig.ForcedDifficulty)) return;
                CurrentProfile.ServerConfig.ForcedDifficulty = value.ToString().ToLower();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether server instance is automatic select missions.
        /// </summary>
        public bool IsAutoSelectMissions
        {
            get => CurrentProfile.ServerConfig.IsAutoSelectMissions;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.IsAutoSelectMissions)) return;
                CurrentProfile.ServerConfig.IsAutoSelectMissions = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether server instance is random mission order.
        /// </summary>
        public bool IsRandomMissionOrder
        {
            get => CurrentProfile.ServerConfig.IsRandomMissionOrder;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.IsRandomMissionOrder)) return;
                CurrentProfile.ServerConfig.IsRandomMissionOrder = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the missions to server restart quantity.
        /// </summary>
        public int? MissionsToServerRestartQuantity
        {
            get => CurrentProfile?.ServerConfig.MissionsToServerRestartQuantity;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig.MissionsToServerRestartQuantity)) return;
                CurrentProfile.ServerConfig.MissionsToServerRestartQuantity = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the missions to shutdown quantity.
        /// </summary>
        public int? MissionsToShutdownQuantity
        {
            get => CurrentProfile?.ServerConfig.MissionsToShutdownQuantity;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig.MissionsToShutdownQuantity)) return;
                CurrentProfile.ServerConfig.MissionsToShutdownQuantity = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the played missions.
        /// </summary>
        public ObservableCollection<Mission> Missions
        {
            get => _missions;
            set
            {
                if (Equals(_missions, value))
                {
                    return;
                }
                _missions = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Mission> _missions;

        /// <summary>
        /// Gets or sets the selected mission.
        /// </summary>
        public Mission SelectedMission
        {
            get => _selectedMission;
            set
            {
                if (Equals(_selectedMission, value))
                {
                    return;
                }

                _selectedMission = value;
                RaisePropertyChanged();
            }
        }
        private Mission _selectedMission;

        /// <summary>
        /// Gets the refresh command.
        /// </summary>
        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ??
                       (_refreshCommand = new RelayCommand(_ => RefreshMissions()));
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
                       (_windowLoadedCommand = new RelayCommand(async _ =>
                       {
                           if(!Missions.Any())
                           {
                               await RefreshMissions().ConfigureAwait(false);
                           }
                       }));
            }
        }

        private ICommand _windowLoadedCommand;

        public MissionsViewModel(ServerViewModel parentViewModel, IDao<Mission> missionDao, GameLocationFinder locationFinder)
        {
            _parentViewModel = parentViewModel;
            _missions = new ObservableCollection<Mission>(CurrentProfile.ServerConfig.Missions);
            _missionDao = missionDao;
            _locationFinder = locationFinder;

            Messenger.Default.Register<string>(this, Token, DoByRequest);
        }

        private Task RefreshMissions()
        {
            try
            {
                var gamePath = _locationFinder.GetGameInstallationPath(CurrentProfile);
                if (string.IsNullOrWhiteSpace(gamePath))
                {
                    Missions.Clear();
                    return Task.FromResult<object>(null);
                }

                return Task.Run(() =>
                {
                    try
                    {
                        if (Missions != null)
                        {
                            var oldMissions = new List<Mission>(Missions);
                            var updatedMissions = new List<Mission>(_missionDao.GetAll(gamePath));

                            foreach (var mission in updatedMissions)
                            {
                                var oldMission = oldMissions.FirstOrDefault(m => m.Name == mission.Name);
                                if (oldMission != null)
                                {
                                    mission.IsSelected = oldMission.IsSelected;
                                    mission.IsWhitelisted = oldMission.IsWhitelisted;
                                    mission.Difficulty = oldMission.Difficulty;
                                }
                            }

                            CurrentProfile.ServerConfig.Missions = updatedMissions;
                            Missions = new ObservableCollection<Mission>(CurrentProfile.ServerConfig.Missions);
                        }
                        else
                        {
                            var missions = _missionDao.GetAll(gamePath);
                            CurrentProfile.ServerConfig.Missions = missions.ToList();
                            Missions = new ObservableCollection<Mission>(CurrentProfile.ServerConfig.Missions);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                });
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Refreshes mission list by requests from other view models.
        /// </summary>
        /// <param name="request">message to do something in this viewmodel.</param>
        private void DoByRequest(string request)
        {
            RefreshMissions();
        }
    }
}
