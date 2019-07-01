using A3ServerTool.Enums;
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

        /// <summary>
        /// Gets or sets the value if the damage is reduced.
        /// </summary>
        public int? IsDamageReduced
        {
            get => CurrentProfile?.ArmaProfile?.IsDamageReduced;
            set
            {
                if (Equals(value, CurrentProfile?.ArmaProfile?.IsDamageReduced)) return;
                CurrentProfile.ArmaProfile.IsDamageReduced = IsDamageReduced;
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
    }
}
