using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using A3ServerTool.Annotations;
using A3ServerTool.Attributes;
using Newtonsoft.Json;

namespace A3ServerTool.Models
{
    public class ArgumentSettings : INotifyPropertyChanged, IDataErrorInfo
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
        private string _port;

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
        /// Gets or sets the port.
        /// </summary>
        [ServerArgument(Argument = "-name")]
        public string GameProfileName { get; } = "serverProfile";

        /// <summary>
        /// Gets or sets the required game modifications.
        /// </summary>
        public List<Modification> Modifications { get; set; } = new List<Modification>();

        [JsonIgnore]
        [ServerArgument(Argument = "-mod", IsQuotationMarksRequired = true)]
        /// <summary>
        /// Gets all client modifications as single string.
        /// </summary>
        public string ClientModificationsAsString => Modifications?.Any(m => m.IsClientMod) == true
            ? string.Join(";", Modifications.Where(m => m.IsClientMod).Select(s => s.Name))
            : string.Empty;

        [JsonIgnore]
        [ServerArgument(Argument = "-serverMod", IsQuotationMarksRequired = true)]
        /// <summary>
        /// Gets all client modifications as single string.
        /// </summary>
        public string ServerModificationsAsString => Modifications?.Any(m => m.IsServerMod) == true
            ? string.Join(";", Modifications.Where(m => m.IsServerMod).Select(s => s.Name))
            : string.Empty;

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
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
