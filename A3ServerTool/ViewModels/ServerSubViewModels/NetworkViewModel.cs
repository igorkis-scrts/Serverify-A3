using A3ServerTool.Models;
using A3ServerTool.Models.Config;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace A3ServerTool.ViewModels.ServerSubViewModels
{
    /// <summary>
    /// Represents basic.cfg as VM
    /// </summary>
    public class NetworkViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly ServerViewModel _parentViewModel;

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        public int? MaxMessagesSend
        {
            get => CurrentProfile.BasicConfig.MaxMessagesSend;
            set
            {
                if (Equals(value, CurrentProfile.BasicConfig.MaxMessagesSend)) return;
                CurrentProfile.BasicConfig.MaxMessagesSend = value;
                RaisePropertyChanged();
            }
        }

        public int? MaxSizeGuaranteed
        {
            get => CurrentProfile.BasicConfig.MaxSizeGuaranteed;
            set
            {
                if (Equals(value, CurrentProfile.BasicConfig.MaxSizeGuaranteed)) return;
                CurrentProfile.BasicConfig.MaxSizeGuaranteed = value;
                RaisePropertyChanged();
            }
        }

        public int? MaxSizeNonguaranteed
        {
            get => CurrentProfile.BasicConfig.MaxSizeNonguaranteed;
            set
            {
                if (Equals(value, CurrentProfile.BasicConfig.MaxSizeNonguaranteed)) return;
                CurrentProfile.BasicConfig.MaxSizeNonguaranteed = value;
                RaisePropertyChanged();
            }
        }

        public int? MinBandwidth
        {
            get => CurrentProfile.BasicConfig.MinBandwidth;
            set
            {
                if (Equals(value, CurrentProfile.BasicConfig.MinBandwidth)) return;
                CurrentProfile.BasicConfig.MinBandwidth = value;
                RaisePropertyChanged();
            }
        }

        public int? MaxBandwidth
        {
            get => CurrentProfile.BasicConfig.MaxBandwidth;
            set
            {
                if (Equals(value, CurrentProfile.BasicConfig.MaxBandwidth)) return;
                CurrentProfile.BasicConfig.MaxBandwidth = value;
                RaisePropertyChanged();
            }
        }

        public float? MinErrorToSend
        {
            get => CurrentProfile.BasicConfig.MinErrorToSend;
            set
            {
                if (Equals(value, CurrentProfile.BasicConfig.MinErrorToSend)) return;
                CurrentProfile.BasicConfig.MinErrorToSend = value;
                RaisePropertyChanged();
            }
        }

        public float? MinErrorToSendNear
        {
            get => CurrentProfile.BasicConfig.MinErrorToSendNear;
            set
            {
                if (Equals(value, CurrentProfile.BasicConfig.MinErrorToSendNear)) return;
                CurrentProfile.BasicConfig.MinErrorToSendNear = value;
                RaisePropertyChanged();
            }
        }

        public int? MaxCustomFileSize
        {
            get => CurrentProfile.BasicConfig.MaxCustomFileSize;
            set
            {
                if (Equals(value, CurrentProfile.BasicConfig.MaxCustomFileSize)) return;
                CurrentProfile.BasicConfig.MaxCustomFileSize = value;
                RaisePropertyChanged();
            }
        }

        public int? MaxPacketSize
        {
            get => CurrentProfile.BasicConfig.MaxPacketSize;
            set
            {
                if (Equals(value, CurrentProfile.BasicConfig.MaxPacketSize)) return;
                CurrentProfile.BasicConfig.MaxPacketSize = value;
                RaisePropertyChanged();
            }
        }

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
        /// Gets or sets a value indicating whether server instance is in UPNP mode.
        /// </summary>
        public bool IsUpnp
        {
            get => CurrentProfile.ServerConfig.IsUpnp;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.IsUpnp)) return;
                CurrentProfile.ServerConfig.IsUpnp = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the value that represents that if server is LAN.
        /// </summary>
        public bool IsLan
        {
            get => CurrentProfile.ServerConfig.IsLan;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.IsLan)) return;
                CurrentProfile.ServerConfig.IsLan = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the disconnect timeout.
        /// </summary>
        public int? DisconnectTimeout
        {
            get => CurrentProfile.ServerConfig.DisconnectTimeout;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.DisconnectTimeout)) return;
                CurrentProfile.ServerConfig.DisconnectTimeout = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the maximum desync.
        /// </summary>
        public int? MaxDesync
        {
            get => CurrentProfile.ServerConfig.MaximumDesync;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.MaximumDesync)) return;
                CurrentProfile.ServerConfig.MaximumDesync = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the maximum ping.
        /// </summary>
        public int? MaxPing
        {
            get => CurrentProfile.ServerConfig.MaximumPing;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.MaximumPing)) return;
                CurrentProfile.ServerConfig.MaximumPing = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the maximum ping.
        /// </summary>
        public int? MaxPacketLoss
        {
            get => CurrentProfile.ServerConfig.MaximumPacketLoss;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.MaximumPacketLoss)) return;
                CurrentProfile.ServerConfig.MaximumPacketLoss = value;
                RaisePropertyChanged();
            }
        }

        public NetworkViewModel(ServerViewModel viewModel)
        {
            _parentViewModel = viewModel;
        }

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(MaxMessagesSend) when MaxMessagesSend < 0:
                        return "MaxMessagesSend must be more than zero.";
                    case nameof(MaxSizeGuaranteed) when MaxSizeGuaranteed < 0:
                        return "MaxSizeGuaranteed must be more than zero.";
                    case nameof(MaxSizeNonguaranteed) when MaxSizeNonguaranteed < 0:
                        return "MaxSizeNonguaranteed must be more than zero.";
                    case nameof(MinBandwidth) when MinBandwidth < 0:
                        return "MinBandwidth must be more than zero.";
                    case nameof(MaxBandwidth) when MaxBandwidth < 0:
                        return "MaxBandwidth must be more than zero.";
                    case nameof(MinErrorToSend) when MinErrorToSend < 0:
                        return "MinErrorToSend must be more than zero.";
                    case nameof(MinErrorToSendNear) when MinErrorToSendNear < 0:
                        return "MinErrorToSend must be more than zero.";
                    case nameof(MaxCustomFileSize) when MaxCustomFileSize < 0:
                        return "MaxCustomFileSize must be more than zero.";
                    case nameof(MaxPacketSize) when MaxPacketSize < 0:
                        return "MaxPacketSize must be more than zero.";
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
                if(MaxMessagesSend < 0 || MaxMessagesSend == null)
                {
                    return "MaxMessagesSend must be more than zero.";
                }
                if (MaxSizeGuaranteed < 0 || MaxSizeGuaranteed == null)
                {
                    return "MaxSizeGuaranteed must be more than zero.";
                }
                if (MaxSizeNonguaranteed < 0 || MaxSizeNonguaranteed == null)
                {
                    return "MaxSizeNonguaranteed must be more than zero.";
                }
                if (MinBandwidth < 0 || MaxBandwidth == null)
                {
                    return "MinBandwidth must be more than zero.";
                }
                if (MaxBandwidth < 0 || MaxBandwidth == null)
                {
                    return "MaxBandwidth must be more than zero.";
                }
                if (MinErrorToSend < 0 || MinErrorToSend == null)
                {
                    return "MinErrorToSend must be more than zero.";
                }
                if (MinErrorToSendNear < 0 || MinErrorToSendNear == null)
                {
                    return "MinErrorToSendNear must be more than zero.";
                }
                if (MaxCustomFileSize < 0 || MaxCustomFileSize == null)
                {
                    return "MaxCustomFileSize must be more than zero.";
                }
                if (MaxPacketSize < 0 || MaxPacketSize == null)
                {
                    return "MaxPacketSize must be more than zero.";
                }
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
