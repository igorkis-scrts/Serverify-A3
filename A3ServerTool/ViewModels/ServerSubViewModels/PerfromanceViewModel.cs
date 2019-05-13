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
