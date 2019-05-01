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

        public BasicConfig CurrentBasicConfig => _parentViewModel.CurrentProfile.BasicConfig;

        public int? MaxMessagesSend
        {
            get => CurrentBasicConfig.MaxMessagesSend;
            set
            {
                if (Equals(value, CurrentBasicConfig.MaxMessagesSend)) return;
                CurrentBasicConfig.MaxMessagesSend = value;
                RaisePropertyChanged();
            }
        }

        public int? MaxSizeGuaranteed
        {
            get => CurrentBasicConfig.MaxSizeGuaranteed;
            set
            {
                if (Equals(value, CurrentBasicConfig.MaxSizeGuaranteed)) return;
                CurrentBasicConfig.MaxSizeGuaranteed = value;
                RaisePropertyChanged();
            }
        }

        public int? MaxSizeNonguaranteed
        {
            get => CurrentBasicConfig.MaxSizeNonguaranteed;
            set
            {
                if (Equals(value, CurrentBasicConfig.MaxSizeNonguaranteed)) return;
                CurrentBasicConfig.MaxSizeNonguaranteed = value;
                RaisePropertyChanged();
            }
        }

        public int? MinBandwidth
        {
            get => CurrentBasicConfig.MinBandwidth;
            set
            {
                if (Equals(value, CurrentBasicConfig.MinBandwidth)) return;
                CurrentBasicConfig.MinBandwidth = value;
                RaisePropertyChanged();
            }
        }

        public int? MaxBandwidth
        {
            get => CurrentBasicConfig.MaxBandwidth;
            set
            {
                if (Equals(value, CurrentBasicConfig.MaxBandwidth)) return;
                CurrentBasicConfig.MaxBandwidth = value;
                RaisePropertyChanged();
            }
        }

        public float? MinErrorToSend
        {
            get => CurrentBasicConfig.MinErrorToSend;
            set
            {
                if (Equals(value, CurrentBasicConfig.MinErrorToSend)) return;
                CurrentBasicConfig.MinErrorToSend = value;
                RaisePropertyChanged();
            }
        }

        public float? MinErrorToSendNear
        {
            get => CurrentBasicConfig.MinErrorToSendNear;
            set
            {
                if (Equals(value, CurrentBasicConfig.MinErrorToSendNear)) return;
                CurrentBasicConfig.MinErrorToSendNear = value;
                RaisePropertyChanged();
            }
        }

        public int? MaxCustomFileSize
        {
            get => CurrentBasicConfig.MaxCustomFileSize;
            set
            {
                if (Equals(value, CurrentBasicConfig.MaxCustomFileSize)) return;
                CurrentBasicConfig.MaxCustomFileSize = value;
                RaisePropertyChanged();
            }
        }

        public int? MaxPacketSize
        {
            get => CurrentBasicConfig.MaxPacketSize;
            set
            {
                if (Equals(value, CurrentBasicConfig.MaxPacketSize)) return;
                CurrentBasicConfig.MaxPacketSize = value;
                RaisePropertyChanged();
            }
        }

        public float? TerrainGridViewDistance
        {
            get => CurrentBasicConfig.TerrainGridViewDistance;
            set
            {
                if (Equals(value, CurrentBasicConfig.TerrainGridViewDistance)) return;
                CurrentBasicConfig.TerrainGridViewDistance = value;
                RaisePropertyChanged();
            }
        }

        public int? ObjectViewDistance
        {
            get => CurrentBasicConfig.ObjectViewDistance;
            set
            {
                if (Equals(value, CurrentBasicConfig.ObjectViewDistance)) return;
                CurrentBasicConfig.ObjectViewDistance = value;
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
