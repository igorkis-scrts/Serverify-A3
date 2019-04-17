using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using A3ServerTool.Annotations;
using A3ServerTool.Models.ConfigStorages;
using Newtonsoft.Json;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Server profile
    /// </summary>
    public class Profile : IDataErrorInfo, INotifyPropertyChanged
    {
        public Profile() { }

        [JsonConstructor]
        public Profile(ArgumentSettings argumentSettings, Guid id)
        {
            ArgumentSettings = argumentSettings;
            Id = id;
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
        private ArgumentSettings _serverSettings;

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
        private BasicConfig _basicConfig;


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
    }
}
