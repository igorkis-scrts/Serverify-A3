using System.Collections.Generic;
using System.Collections.ObjectModel;
using A3ServerTool.Models;

namespace A3ServerTool.ProfileStorage
{
    public interface IProfileDao
    {
        ObservableCollection<Profile> GetAll();
        Profile Get(Profile profile);

        void SaveOrUpdate(Profile profile);
        void Insert(Profile profile);
        void Update(Profile profile);
        void Delete(Profile profile);
    }
}
