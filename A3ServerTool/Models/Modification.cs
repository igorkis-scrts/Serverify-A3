using System.ComponentModel;
using System.Runtime.CompilerServices;
using A3ServerTool.Properties;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Represents game mod.
    /// </summary>
    public class Modification : INotifyPropertyChanged
    {     
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        public bool IsClientMod
        {
            get => _isClientMod;
            set
            {
                if (Equals(_isClientMod, value))
                {
                    return;
                }
                _isClientMod = value;
                OnPropertyChanged();
            }
        }

        private bool _isClientMod;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is whitelisted.
        /// </summary>
        public bool IsServerMod
        {
            get => _isServerMod;
            set
            {
                if (Equals(_isServerMod, value))
                {
                    return;
                }
                _isServerMod = value;
                OnPropertyChanged();
            }
        }

        private bool _isServerMod;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
