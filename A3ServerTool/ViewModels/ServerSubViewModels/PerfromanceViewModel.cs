using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A3ServerTool.Models;
using GalaSoft.MvvmLight;

namespace A3ServerTool.ViewModels.ServerSubViewModels
{
    /// <summary>
    /// Represents view model with server performance properties.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    public class PerfromanceViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly ServerViewModel _parentViewModel;

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        /// <summary>
        /// Gets or sets the terrain grid view distance.
        /// </summary>
        public float? TerrainGridViewDistance
        {
            get => CurrentProfile.BasicConfig.TerrainGridViewDistance;
            set
            {
                if (Equals(value, CurrentProfile.BasicConfig.TerrainGridViewDistance)) return;
                CurrentProfile.BasicConfig.TerrainGridViewDistance = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the object view distance.
        /// </summary>
        public int? ObjectViewDistance
        {
            get => CurrentProfile.BasicConfig.ObjectViewDistance;
            set
            {
                if (Equals(value, CurrentProfile.BasicConfig.ObjectViewDistance)) return;
                CurrentProfile.BasicConfig.ObjectViewDistance = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the cpu core count.
        /// </summary>
        public int? CpuCoreCount
        {
            get => CurrentProfile.ArgumentSettings.CpuCoreCount;
            set
            {
                if (Equals(value, CurrentProfile.ArgumentSettings.CpuCoreCount)) return;
                CurrentProfile.ArgumentSettings.CpuCoreCount = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the thread count.
        /// </summary>
        public int? ThreadCount
        {
            get => CurrentProfile.ArgumentSettings.ThreadCount;
            set
            {
                if (Equals(value, CurrentProfile.ArgumentSettings.ThreadCount)) return;
                CurrentProfile.ArgumentSettings.ThreadCount = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the memory allocation limit.
        /// </summary>
        /// <value>
        /// The maximum memory.
        /// </value>
        public string MaximumMemory
        {
            get => CurrentProfile.ArgumentSettings.MaximumMemory;
            set
            {
                if (Equals(value, CurrentProfile.ArgumentSettings.MaximumMemory)) return;
                CurrentProfile.ArgumentSettings.MaximumMemory = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is hyper threading enabled.
        /// </summary>
        public bool IsHyperThreadingEnabled
        {
            get => CurrentProfile.ArgumentSettings.IsHyperThreadingEnabled;
            set
            {
                if (Equals(value, CurrentProfile.ArgumentSettings.IsHyperThreadingEnabled)) return;
                CurrentProfile.ArgumentSettings.IsHyperThreadingEnabled = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [are huge pages enabled].
        /// </summary>
        public bool AreHugePagesEnabled
        {
            get => CurrentProfile.ArgumentSettings.AreHugePagesEnabled;
            set
            {
                if (Equals(value, CurrentProfile.ArgumentSettings.AreHugePagesEnabled)) return;
                CurrentProfile.ArgumentSettings.AreHugePagesEnabled = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is mission loaded to memory.
        /// </summary>
        public bool IsMissionLoadedToMemory
        {
            get => CurrentProfile.ArgumentSettings.IsMissionLoadedToMemory;
            set
            {
                if (Equals(value, CurrentProfile.ArgumentSettings.IsMissionLoadedToMemory)) return;
                CurrentProfile.ArgumentSettings.IsMissionLoadedToMemory = value;
                RaisePropertyChanged();
            }
        }

        public PerfromanceViewModel(ServerViewModel viewModel)
        {
            _parentViewModel = viewModel;
        }

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case nameof(TerrainGridViewDistance) when TerrainGridViewDistance < 0:
                        return "TerrainGridViewDistance must be more than zero.";
                    case nameof(ObjectViewDistance) when ObjectViewDistance < 0:
                        return "ObjectViewDistance must be more than zero.";
                    default:
                        return null;
                }
            }
        }

        public string Error
        {
            get
            {
                if (TerrainGridViewDistance < 0 || TerrainGridViewDistance == null)
                {
                    return "TerrainGridViewDistance must be more than zero.";
                }
                if (ObjectViewDistance < 0 || ObjectViewDistance == null)
                {
                    return "ObjectViewDistance must be more than zero.";
                }
                return string.Empty;
            }
        }
        

        #endregion
    }
}
