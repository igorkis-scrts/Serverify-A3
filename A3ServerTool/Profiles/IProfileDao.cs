using System.Collections.Generic;
using A3ServerTool.Models;

namespace A3ServerTool.Profiles
{
    public interface IProfileDao
    {
        List<Profile> GetAllProfiles();
        Profile GetProfile();

        void Insert(Profile profile);
        void Update(Profile profile);
        void Delete(Profile profile);
    }
}
