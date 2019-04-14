using System.Collections.Generic;
using System.Collections.ObjectModel;
using A3ServerTool.Models;

namespace A3ServerTool.ProfileStorage
{
    /// <summary>
    /// Provides Data Access Object for server profiles
    /// </summary>
    public interface IProfileDao
    {
        ObservableCollection<Profile> GetAll();
        Profile Get(Profile profile);

        void Save(Profile profile);
        void Delete(Profile profile);
    }
}
