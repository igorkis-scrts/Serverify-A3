using System;
using System.Collections.Generic;

namespace A3ServerTool.Models.Profile
{
    /// <summary>
    /// Provides an interface between VMs and DAO to interact with profiles.
    /// </summary>
    public interface IProfileDirector
    {
        /// <summary>
        /// Creates/updates profile physical presence on hard drive.
        /// </summary>
        /// <param name="profile">Profile to save.</param>
        void SaveStorage(Profile profile);

        /// <summary>
        /// Gets all profiles.
        /// </summary>
        /// <returns>All stored profiles as IList.</returns>
        IList<Profile> GetAll();

        /// <summary>
        /// Deletes profile
        /// </summary>
        /// <param name="profile">Profile to delete.</param>
        void Delete(Profile profile);

        /// <summary>
        /// Checks if profile has physical presence on hard drive
        /// </summary>
        /// <param name="profile">Profile to check.</param> 
        /// <returns>True on exists, false on not exists.</returns>
        bool ExistOnStorage(Profile profile);

        /// <summary>
        /// Checks if profile has physical presence on hard drive
        /// </summary>
        Profile GetById(Guid id);

        /// <summary>
        /// Get mods that used by profile.
        /// </summary>
        /// <param name="profile"></param>
        void GetProfileModifications(Profile profile);
    }
}
