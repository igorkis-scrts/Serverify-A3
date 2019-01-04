using System;
using System.Collections.Generic;
using A3ServerTool.Models;
using Interchangeable.Helpers;

namespace A3ServerTool.Profiles
{
    public class JsonProfileDao : IProfileDao
    {
        public List<Profile> GetAllProfiles()
        {
            var profiles = new List<Profile>();

            var fileList = FileHelper.GetSpecificFiles(AppDomain.CurrentDomain.BaseDirectory, "json");

            return profiles;
        }

        public Profile GetProfile()
        {
            throw new NotImplementedException();
        }

        public void Insert(Profile profile)
        {
            throw new NotImplementedException();
        }

        public void Update(Profile profile)
        {
            throw new NotImplementedException();
        }

        public void Delete(Profile profile)
        {
            throw new NotImplementedException();
        }
    }
}
