using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Provides an interface between VMs and DAO to interact with profiles
    /// </summary>
    public interface IProfileDirector
    {
        /// <summary>
        /// Creates/update profile physical presence on hard drive
        /// </summary>
        void SaveStorage(Profile profile);

        /// <summary>
        /// Gets all profiles
        /// </summary>
        ObservableCollection<Profile> GetAll();

        /// <summary>
        /// Deletes profile
        /// </summary>
        void Delete(Profile profile);

        /// <summary>
        /// Checks if profile has physical presence on hard drive
        /// </summary>
        bool ExistOnStorage(Profile profile);
    }
}
