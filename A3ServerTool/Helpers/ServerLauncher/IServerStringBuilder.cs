using A3ServerTool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Helpers.ServerLauncher
{
    /// <summary>
    /// Provides helpers to work with executable file arguments.
    /// </summary>
    public interface IServerStringBuilder: INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        Profile Profile { get; set; }

        /// <summary>
        /// Provides string with applied command line arguments.
        /// </summary>
        /// <returns>Final string with all arguments that will be applied to the server executable file.</returns>
        string FinalArgumentString { get; }

        void ReaggregateArgumentString();
    }
}
