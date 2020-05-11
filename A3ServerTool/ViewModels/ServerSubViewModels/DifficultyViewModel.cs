using A3ServerTool.Enums;
using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using Interchangeable;
using System;
using System.ComponentModel;
using A3ServerTool.Models.Profile;

namespace A3ServerTool.ViewModels.ServerSubViewModels
{
    /// <summary>
    /// Represents view model with server difficulty settings.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    public class DifficultyViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly ServerViewModel _parentViewModel;

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        public DifficultyViewModel(ServerViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
        }

        /// <summary>
        /// Gets or sets the value if the damage is reduced.
        /// </summary>
        public int? IsDamageReduced
        {
            get => CurrentProfile?.ArmaProfile?.IsDamageReduced;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.IsDamageReduced)) return;
                CurrentProfile.ArmaProfile.IsDamageReduced = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the type of the group indication.
        /// </summary>
        public VisibilityType GroupIndicationType
        {
            get
            {
                return CurrentProfile?.ArmaProfile?.GroupIndicationType != null
                     ? (VisibilityType)CurrentProfile.ArmaProfile.GroupIndicationType
                     : VisibilityType.Hide;
            }
            set
            {
                if (Equals((int)value, CurrentProfile?.ArmaProfile?.GroupIndicationType)) return;
                CurrentProfile.ArmaProfile.GroupIndicationType = (int)value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the type of the friendly tags visibility.
        /// </summary>
        public VisibilityType FriendlyTagsVisibilityType
        {
            get
            {
                return CurrentProfile?.ArmaProfile?.FriendlyTagsVisibilityType != null
                     ? (VisibilityType)CurrentProfile.ArmaProfile.FriendlyTagsVisibilityType
                     : VisibilityType.Hide;
            }
            set
            {
                if (Equals((int)value, CurrentProfile?.ArmaProfile?.FriendlyTagsVisibilityType)) return;
                CurrentProfile.ArmaProfile.FriendlyTagsVisibilityType = (int)value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the type of the enemy tags visibility.
        /// </summary>
        public VisibilityType EnemyTagsVisibilityType
        {
            get
            {
                return CurrentProfile?.ArmaProfile?.EnemyTagsVisibilityType != null
                     ? (VisibilityType)CurrentProfile.ArmaProfile.EnemyTagsVisibilityType
                     : VisibilityType.Hide;
            }
            set
            {
                if (Equals((int)value, CurrentProfile?.ArmaProfile?.EnemyTagsVisibilityType)) return;
                CurrentProfile.ArmaProfile.EnemyTagsVisibilityType = (int)value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the type of the detected mines visibility.
        /// </summary>
        public VisibilityType DetectedMinesVisibilityType
        {
            get
            {
                return CurrentProfile?.ArmaProfile?.DetectedMinesVisibilityType != null
                     ? (VisibilityType)CurrentProfile.ArmaProfile.DetectedMinesVisibilityType
                     : VisibilityType.Hide;
            }
            set
            {
                if (Equals((int)value, CurrentProfile?.ArmaProfile?.DetectedMinesVisibilityType)) return;
                CurrentProfile.ArmaProfile.DetectedMinesVisibilityType = (int)value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the type of the commands visibility.
        /// </summary>
        public IndicationType CommandsVisibilityType
        {
            get
            {
                return CurrentProfile?.ArmaProfile?.CommandsVisibilityType != null
                     ? (IndicationType)CurrentProfile.ArmaProfile.CommandsVisibilityType
                     : IndicationType.Hide;
            }
            set
            {
                if (Equals((int) value, CurrentProfile?.ArmaProfile?.CommandsVisibilityType)) return;
                CurrentProfile.ArmaProfile.CommandsVisibilityType = (int) value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the type of the waypoints visibility.
        /// </summary>
        public IndicationType WaypointsVisibilityType
        {
            get
            {
                return CurrentProfile?.ArmaProfile?.WaypointsVisibilityType != null
                     ? (IndicationType)CurrentProfile.ArmaProfile.WaypointsVisibilityType
                     : IndicationType.Hide;
            }
            set
            {
                if (Equals((int)value, CurrentProfile?.ArmaProfile?.WaypointsVisibilityType)) return;
                CurrentProfile.ArmaProfile.WaypointsVisibilityType = (int)value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the type of the weapon information visibility.
        /// </summary>
        public IndicationType WeaponInfoVisibilityType
        {
            get
            {
                return CurrentProfile?.ArmaProfile?.WeaponInfoVisibilityType != null
                     ? (IndicationType)CurrentProfile.ArmaProfile.WeaponInfoVisibilityType
                     : IndicationType.Hide;
            }
            set
            {
                if (Equals((int)value, CurrentProfile?.ArmaProfile?.WeaponInfoVisibilityType)) return;
                CurrentProfile.ArmaProfile.WeaponInfoVisibilityType = (int)value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the type of the stance indicator visibility.
        /// </summary>
        public IndicationType StanceIndicatorVisibilityType
        {
            get
            {
                return CurrentProfile?.ArmaProfile?.StanceIndicatorVisibilityType != null
                     ? (IndicationType)CurrentProfile.ArmaProfile.StanceIndicatorVisibilityType
                     : IndicationType.Hide;
            }
            set
            {
                if (Equals((int)value, CurrentProfile?.ArmaProfile?.StanceIndicatorVisibilityType)) return;
                CurrentProfile.ArmaProfile.StanceIndicatorVisibilityType = (int)value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is stamina bar shown.
        /// </summary>
        public int? IsStaminaBarShown
        {
            get => CurrentProfile?.ArmaProfile?.IsStaminaBarShown;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.IsStaminaBarShown)) return;
                CurrentProfile.ArmaProfile.IsStaminaBarShown = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is crosshair shown.
        /// </summary>
        public int? IsCrosshairShown
        {
            get => CurrentProfile?.ArmaProfile?.IsCrosshairShown;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.IsCrosshairShown)) return;
                CurrentProfile.ArmaProfile.IsCrosshairShown = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is visual aid allowed.
        /// </summary>
        public int? IsVisualAidAllowed
        {
            get => CurrentProfile?.ArmaProfile?.IsVisionAidAllowed;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.IsVisionAidAllowed)) return;
                CurrentProfile.ArmaProfile.IsVisionAidAllowed = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the are death messages shown.
        /// </summary>
        public int? AreDeathMessagesShown
        {
            get => CurrentProfile?.ArmaProfile?.AreDeathMessagesShown;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.AreDeathMessagesShown)) return;
                CurrentProfile.ArmaProfile.AreDeathMessagesShown = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the are voice-over-net IDs shown.
        /// </summary>
        public int? AreVonIdsShown
        {
            get => CurrentProfile?.ArmaProfile?.AreVonIdsShown;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.AreVonIdsShown)) return;
                CurrentProfile.ArmaProfile.AreVonIdsShown = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is extended map friendly content allowed.
        /// </summary>
        public int? IsExtendedMapFriendlyContentAllowed
        {
            get => CurrentProfile?.ArmaProfile?.IsExtendedMapFriendlyContentAllowed;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.IsExtendedMapFriendlyContentAllowed)) return;
                CurrentProfile.ArmaProfile.IsExtendedMapFriendlyContentAllowed = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is extended map enemy content allowed.
        /// </summary>
        public int? IsExtendedMapEnemyContentAllowed
        {
            get => CurrentProfile?.ArmaProfile?.IsExtendedMapEnemyContentAllowed;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.IsExtendedMapEnemyContentAllowed)) return;
                CurrentProfile.ArmaProfile.IsExtendedMapEnemyContentAllowed = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is extended map mines content allowed.
        /// </summary>
        public int? IsExtendedMapMinesContentAllowed
        {
            get => CurrentProfile?.ArmaProfile?.IsExtendedMapMinesContentAllowed;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.IsExtendedMapMinesContentAllowed)) return;
                CurrentProfile.ArmaProfile.IsExtendedMapMinesContentAllowed = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets is third person allowed.
        /// </summary>
        public int? IsThirdPersonViewAllowed
        {
            get => CurrentProfile?.ArmaProfile?.IsThirdPersonViewAllowed;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.IsThirdPersonViewAllowed)) return;
                CurrentProfile.ArmaProfile.IsThirdPersonViewAllowed = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is camera shake allowed.
        /// </summary>
        public int? IsCameraShakeAllowed
        {
            get => CurrentProfile?.ArmaProfile?.IsCameraShakeAllowed;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.IsCameraShakeAllowed)) return;
                CurrentProfile.ArmaProfile.IsCameraShakeAllowed = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is automatic report enabled.
        /// </summary>
        public int? IsAutoReportEnabled
        {
            get => CurrentProfile?.ArmaProfile?.IsAutoReportEnabled;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.IsAutoReportEnabled)) return;
                CurrentProfile.ArmaProfile.IsAutoReportEnabled = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is score table shown.
        /// </summary>
        public int? IsScoreTableShown
        {
            get => CurrentProfile?.ArmaProfile?.IsScoreTableShown;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.IsScoreTableShown)) return;
                CurrentProfile.ArmaProfile.IsScoreTableShown = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the are multiple saves allowed.
        /// </summary>
        public int? AreMultipleSavesAllowed
        {
            get => CurrentProfile?.ArmaProfile?.AreMultipleSavesAllowed;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.AreMultipleSavesAllowed)) return;
                CurrentProfile.ArmaProfile.AreMultipleSavesAllowed = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the type of the tactical ping.
        /// </summary>
        public TacticalPingType TacticalPingType
        {
            get
            {
                return CurrentProfile?.ArmaProfile?.TacticalPingType != null
                     ? (TacticalPingType)CurrentProfile.ArmaProfile.TacticalPingType
                     : TacticalPingType.Hide;
            }
            set
            {
                if (Equals((int)value, CurrentProfile?.ArmaProfile?.TacticalPingType)) return;
                CurrentProfile.ArmaProfile.TacticalPingType = (int)value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the ai level preset.
        /// </summary>
        public AiLevelPresetType AiLevelPreset
        {
            get
            {
                return CurrentProfile?.ArmaProfile?.AiLevelPreset != null
                     ? (AiLevelPresetType) CurrentProfile.ArmaProfile.AiLevelPreset
                     : AiLevelPresetType.Normal;
            }
            set
            {
                if (Equals((int)value, CurrentProfile?.ArmaProfile?.AiLevelPreset)) return;
                CurrentProfile.ArmaProfile.AiLevelPreset = (int)value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the AI skill.
        /// </summary>
        public float? AiSkill
        {
            get => CurrentProfile?.ArmaProfile?.AiSkill;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.AiSkill)) return;
                CurrentProfile.ArmaProfile.AiSkill = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the ai precision.
        /// </summary>
        public float? AiPrecision
        {
            get => CurrentProfile?.ArmaProfile?.AiPrecision;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.AiPrecision)) return;
                CurrentProfile.ArmaProfile.AiPrecision = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the default difficulty.
        /// </summary>
        public DifficultyType DefaultDifficulty
        {
            get
            {
                if(CurrentProfile?.ArmaProfile?.DefaultDifficulty == null)
                {
                    return DifficultyType.Recruit;
                }

                Enum.TryParse(CurrentProfile.ArmaProfile.DefaultDifficulty.FirstLetterToUpperCase(),
                    out DifficultyType difficulty);
                return difficulty;
            }
            set
            {
                if (Equals(value.ToString(), CurrentProfile.ArmaProfile.DefaultDifficulty)) return;
                CurrentProfile.ArmaProfile.DefaultDifficulty = value.ToString().ToLower();
                RaisePropertyChanged();
            }
        }

        #region

        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case nameof(AiSkill) when AiSkill == null:
                        return "Ai Skill should be defined.";
                    case nameof(AiPrecision) when AiPrecision == null:
                        return "Ai Precision should be defined.";
                    default:
                        return null;
                }
            }
        }       

        #endregion
    }
}
