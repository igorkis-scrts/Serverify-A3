﻿using A3ServerTool.Enums;
using A3ServerTool.Helpers;
using A3ServerTool.Models;
using A3ServerTool.Storage;
using GalaSoft.MvvmLight;
using Interchangeable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace A3ServerTool.ViewModels.ServerSubViewModels
{
    /// <summary>
    /// Represents view model with mission list.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    public class MissionsViewModel : ViewModelBase
    {
        private readonly ServerViewModel _parentViewModel;
        private readonly IDao<Mission> _missionDao;

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        /// <summary>
        /// Gets or sets the forced difficulty.
        /// </summary>
        public DifficultyType ForcedDifficulty
        {
            get
            {
                Enum.TryParse(CurrentProfile.ServerConfig.ForcedDifficulty.FirstLetterToUpperCase(), out DifficultyType difficulty);
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
                       (_refreshCommand = new RelayCommand(_ =>
                       {
                           RefreshMissions();
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
                           if(!Missions.Any())
                           {
                               RefreshMissions();
                           }
                       }));
            }
        }
        private ICommand _windowLoadedCommand;

        public MissionsViewModel(ServerViewModel parentViewModel, IDao<Mission> missionDao)
        {
            _parentViewModel = parentViewModel;           
            _missions = new ObservableCollection<Mission>(CurrentProfile.ServerConfig.Missions);
            _missionDao = missionDao;
        }

        private void RefreshMissions()
        {
            var gamePath = ServerConfigDao.GetGameInstallationPath(CurrentProfile?.ArgumentSettings?.ExecutablePath);
            if (string.IsNullOrWhiteSpace(gamePath)) return;

            Task.Run(() =>
            {
                if(Missions != null)
                {
                    var oldMissions = new List<Mission>(Missions);
                    var updatedMissions = new ObservableCollection<Mission>(_missionDao.GetAll(gamePath));

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

                    Missions = updatedMissions;
                }
                else
                {
                    Missions =new ObservableCollection<Mission>(_missionDao.GetAll(gamePath));
                }
            });
        }
    }
}