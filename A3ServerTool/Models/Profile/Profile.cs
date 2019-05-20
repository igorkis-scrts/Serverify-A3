using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using A3ServerTool.Annotations;
using A3ServerTool.Models.Config;
using Newtonsoft.Json;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Server profile
    /// </summary>
    public class Profile : IProfile, IDataErrorInfo, INotifyPropertyChanged, ICloneable
    {
        public const string StorageFolder = "Profiles";

        public Profile()
        {
            BasicConfig = new BasicConfig();
            ServerConfig = new ServerConfig();
        }

        public Profile(Guid id) : this()
        {
            Id = id;
        }

        [JsonConstructor]
        public Profile(ArgumentSettings argumentSettings, Guid id) : this(id)
        {
            ArgumentSettings = argumentSettings;
        }

        /// <inheritdoc />
        [JsonProperty]
        public Guid Id { get; }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public string Description
        {
            get => _description;
            set
            {
                if (Equals(value, _description)) return;
                _description = value;
                OnPropertyChanged();
            }
        }
        private string _description;

        /// <inheritdoc />
        public ArgumentSettings ArgumentSettings
        {
            get => _serverSettings;
            set
            {
                if (Equals(value, _serverSettings)) return;
                _serverSettings = value;
                OnPropertyChanged();
            }
        }
        private ArgumentSettings _serverSettings = new ArgumentSettings();

        /// <inheritdoc />
        [JsonIgnore]
        public BasicConfig BasicConfig
        {
            get => _basicConfig;
            set
            {
                if (Equals(value, _basicConfig)) return;
                _basicConfig = value;
                OnPropertyChanged();
            }
        }
        private BasicConfig _basicConfig = new BasicConfig();

        [JsonIgnore]
        public string BasicConfigString => BasicConfig != null ? BasicConfig.FileLocation : string.Empty;

        /// <inheritdoc />
        [JsonIgnore]
        public ServerConfig ServerConfig
        {
            get => _serverConfig;
            set
            {
                if (Equals(value, _serverConfig)) return;
                _serverConfig = value;
                OnPropertyChanged();
            }
        }
        private ServerConfig _serverConfig = new ServerConfig();

        [JsonIgnore]
        public string ServerConfigString => ServerConfig != null ? ServerConfig.FileLocation : string.Empty;

        #region IDataErrorInfo members

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Name) when string.IsNullOrWhiteSpace(Name):
                        return "Profile name should be not empty";
                    case nameof(Description) when string.IsNullOrWhiteSpace(Description):
                        return "Profile description should be not empty";
                    default:
                        return null;
                }
            }
        }

        [JsonIgnore]
        public string Error => string.Empty;

        #endregion

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region ICloneable
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
