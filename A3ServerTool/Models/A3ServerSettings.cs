using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using A3ServerTool.Annotations;
using A3ServerTool.Attributes;

namespace A3ServerTool.Models
{
    public class A3ServerSettings : IServerSettings, INotifyPropertyChanged
    {
        /// <inheritdoc/>
        [SettingSource(SourceType = SettingSourceType.None)]
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


        // <inheritdoc/>
        [SettingSource(SourceType = SettingSourceType.Argument)]
        [CommandLineArgument(Argument = "port")]
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


        /// <inheritdoc/>
        [SettingSource(SourceType = SettingSourceType.None)]
        public string ExecutablePath
        {
            get => _executablePath;
            set
            {
                if (Equals(value, _executablePath)) return;
                _executablePath = value;
                OnPropertyChanged();
            }
        }
        private string _executablePath;

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
