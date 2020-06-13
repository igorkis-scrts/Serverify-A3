using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using A3ServerTool.Attributes;
using A3ServerTool.Properties;
using Newtonsoft.Json;

namespace A3ServerTool.Models
{
    public sealed class ArgumentSettings : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (Equals(value, _name)) return;
                _name = value;
                OnPropertyChanged();
            }
        }
        private string _name;

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        [ServerArgument(Argument = "-port")]
        public string Port
        {
            get => _port;
            set
            {
                if (Equals(value, _port)) return;
                _port = value;
                OnPropertyChanged();
            }
        }
        private string _port = "2302";

        /// <summary>
        /// Gets or sets a value indicating whether [are logs disabled].
        /// </summary>
        [ServerArgument(Argument = "-noLogs")]
        public bool AreLogsDisabled
        {
            get => _areLogsDisabled;
            set
            {
                if (Equals(value, _areLogsDisabled)) return;
                _areLogsDisabled = value;
                OnPropertyChanged();
            }
        }
        private bool _areLogsDisabled;

        /// <summary>
        /// Gets or sets a value indicating whether [are huge memory pages enabled].
        /// </summary>
        [ServerArgument(Argument = "-hugePages")]
        public bool AreHugePagesEnabled
        {
            get => _areHugePagesEnabled;
            set
            {
                if (Equals(value, _areHugePagesEnabled)) return;
                _areHugePagesEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _areHugePagesEnabled;

        /// <summary>
        /// Gets or sets a value indicating whether [is file patching enabled].
        /// </summary>
        [ServerArgument(Argument = "-filePatching")]
        public bool IsFilePatchingEnabled
        {
            get => _isFilePatchingEnabled;
            set
            {
                if (Equals(value, _isFilePatchingEnabled)) return;
                _isFilePatchingEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _isFilePatchingEnabled;

        /// <summary>
        /// Gets or sets the type of the memory allocator.
        /// </summary>
        [ServerArgument(Argument = "-noLogs")]
        public string MemoryAllocatorType
        {
            get => _memoryAllocatorType;
            set
            {
                if (Equals(value, _memoryAllocatorType)) return;
                _memoryAllocatorType = value;
                OnPropertyChanged();
            }
        }
        private string _memoryAllocatorType;

        /// <summary>
        /// Gets or sets the thread count.
        /// </summary>
        [ServerArgument(Argument = "-exThreads")]
        public int? ThreadCount
        {
            get => _threadCount;
            set
            {
                if (Equals(value, _threadCount)) return;
                _threadCount = value;
                OnPropertyChanged();
            }
        }
        private int? _threadCount;

        /// <summary>
        /// Gets or sets the core count.
        /// </summary>
        [ServerArgument(Argument = "-cpuCount")]
        public int? CpuCoreCount
        {
            get => _coreCount;
            set
            {
                if (Equals(value, _coreCount)) return;
                _coreCount = value;
                OnPropertyChanged();
            }
        }
        private int? _coreCount;

        /// <summary>
        /// Gets or sets a value indicating whether hyper threading for server instance enabled.
        /// </summary>
        [ServerArgument(Argument = "-enableHT")]
        public bool IsHyperThreadingEnabled
        {
            get => _isHyperThreadingEnabled;
            set
            {
                if (Equals(value, _isHyperThreadingEnabled)) return;
                _isHyperThreadingEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _isHyperThreadingEnabled;

        /// <summary>
        /// Gets or sets the memory allocation limit.
        /// </summary>
        [ServerArgument(Argument = "-maxMem")]
        public string MaximumMemory
        {
            get => _maximumMemory;
            set
            {
                if (Equals(value, _maximumMemory)) return;
                _maximumMemory = value;
                OnPropertyChanged();
            }
        }
        private string _maximumMemory;

        /// <summary>
        /// Gets or sets the automatic test path.
        /// </summary>
        [ServerArgument(Argument = "-autotest", IsQuotationMarksRequired = true)]
        public string AutoTestPath
        {
            get => _autoTestPath;
            set
            {
                if (Equals(value, _autoTestPath)) return;
                _autoTestPath = value;
                OnPropertyChanged();
            }
        }
        private string _autoTestPath;

        /// <summary>
        /// Gets or sets the process identifier.
        /// </summary>
        [ServerArgument(Argument = "-pid")]
        public string ProcessIdFileName
        {
            get => _processIdFileName;
            set
            {
                if (Equals(value, _processIdFileName)) return;
                _processIdFileName = value;
                OnPropertyChanged();
            }
        }
        private string _processIdFileName;

        /// <summary>
        /// Gets or sets the path to the ranking file.
        /// </summary>
        [ServerArgument(Argument = "-ranking", IsQuotationMarksRequired = true)]
        public string RankingFilePath
        {
            get => _rankingFilePath;
            set
            {
                if (Equals(value, _rankingFilePath)) return;
                _rankingFilePath = value;
                OnPropertyChanged();
            }
        }
        private string _rankingFilePath;

        /// <summary>
        /// Gets the name of the game profile.
        /// </summary>
        [ServerArgument(Argument = "-name")]
        public string GameProfileName { get; } = "serverProfile";

        /// <summary>
        /// Gets or sets a value indicating whether is bandwidth algorhitm2 enabled.
        /// </summary>
        [ServerArgument(Argument = "-bandwidthAlg=2")]
        public bool IsBandwidthAlgorithm2Enabled
        {
            get => _isBandwidthAlgorithm2Enabled;
            set
            {
                if (Equals(value, _isBandwidthAlgorithm2Enabled)) return;
                _isBandwidthAlgorithm2Enabled = value;
                OnPropertyChanged();
            }
        }
        private bool _isBandwidthAlgorithm2Enabled;

        /// <summary>
        /// Gets or sets a value indicating whether is server thread disabled.
        /// </summary>
        [ServerArgument(Argument = "-disableServerThread")]
        public bool IsServerThreadDisabled
        {
            get => _isServerThreadDisabled;
            set
            {
                if (Equals(value, _isServerThreadDisabled)) return;
                _isServerThreadDisabled = value;
                OnPropertyChanged();
            }
        }
        private bool _isServerThreadDisabled;

        /// <summary>
        /// Gets or sets a value indicating whether is mission loaded to memory.
        /// </summary>
        [ServerArgument(Argument = "-loadMissionToMemory")]
        public bool IsMissionLoadedToMemory
        {
            get => _isMissionLoadedToMemory;
            set
            {
                if (Equals(value, _isMissionLoadedToMemory)) return;
                _isMissionLoadedToMemory = value;
                OnPropertyChanged();
            }
        }
        private bool _isMissionLoadedToMemory;

        /// <summary>
        /// Gets or sets a value indicating whether is mission automatically initialized.
        /// </summary>
        [ServerArgument(Argument = "-autoInit")]
        public bool IsMissionAutoInitialized
        {
            get => _isMissionAutoInitialized;
            set
            {
                if (Equals(value, _isMissionAutoInitialized)) return;
                _isMissionAutoInitialized = value;
                OnPropertyChanged();
            }
        }
        private bool _isMissionAutoInitialized;

        /// <summary>
        /// Gets or sets a value indicating whether is traffic logged.
        /// </summary>
        [ServerArgument(Argument = "-netlog")]
        public bool IsTrafficLogged
        {
            get => _isTrafficLogged;
            set
            {
                if (Equals(value, _isTrafficLogged)) return;
                _isTrafficLogged = value;
                OnPropertyChanged();
            }
        }
        private bool _isTrafficLogged;

        /// <summary>
        /// Gets or sets the required game modifications.
        /// </summary>
        public List<Modification> Modifications { get; set; } = new List<Modification>();

        /// <summary>
        /// Gets all client modifications as single string.
        /// </summary>
        [JsonIgnore]
        [ServerArgument(Argument = "-mod", IsQuotationMarksRequired = true)]
        public string ClientModificationsAsString => Modifications?.Any(m => m.IsClientMod) == true
            ? string.Join(";", Modifications.Where(m => m.IsClientMod).Select(s => s.Name))
            : string.Empty;

        /// <summary>
        /// Gets all client modifications as single string.
        /// </summary>
        [JsonIgnore]
        [ServerArgument(Argument = "-serverMod", IsQuotationMarksRequired = true)]
        public string ServerModificationsAsString => Modifications?.Any(m => m.IsServerMod) == true
            ? string.Join(";", Modifications.Where(m => m.IsServerMod).Select(s => s.Name))
            : string.Empty;

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Name) when string.IsNullOrWhiteSpace(Name):
                        return "Name of server must be specified.";
                    case nameof(Port) when string.IsNullOrWhiteSpace(Port):
                        return "Server port must be specified.";
                    default:
                         return null;
                }
            }
        }

        [JsonIgnore]
        public string Error => string.Empty;

        #endregion

    }
}
