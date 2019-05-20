using A3ServerTool.Enums;
using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using Interchangeable;
using System;
using System.Collections.ObjectModel;

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

        public MissionsViewModel(ServerViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
            _missions = new ObservableCollection<Mission>(CurrentProfile.ServerConfig.Missions);
        }

        private void RefreshMissions()
        {
            //Missions = new ObservableCollection<Mission>(_profileDirector.GetAll());
        }
    }
}
