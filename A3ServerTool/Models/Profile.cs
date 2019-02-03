using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using A3ServerTool.Annotations;
using Newtonsoft.Json;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Server profile
    /// </summary>
    public class Profile : IDataErrorInfo, INotifyPropertyChanged
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id = Guid.NewGuid();


        private string _name;

        /// <summary>
        /// Name of setting
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

        private ProfileType _type;

        /// <summary>
        /// Profile type
        /// </summary>
        public ProfileType Type
        {
            get => _type;
            set
            {
                if (Equals(value, _type)) return;
                _type = value;
                OnPropertyChanged();
            }

        }

        private string _description;

        /// <summary>
        /// Short description of server profile
        /// </summary>
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

        private IServerSettings _serverSettings;

        /// <summary>
        /// Server settings
        /// </summary>
        public IServerSettings ServerSettings
        {
            get => _serverSettings;
            set
            {
                if (Equals(value, _serverSettings)) return;
                _serverSettings = value;
                OnPropertyChanged();
            }
        }

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
