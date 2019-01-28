using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using A3ServerTool.Models;
using Interchangeable.IO;
using Newtonsoft.Json;

namespace A3ServerTool.ProfileStorage
{
    public class JsonProfileDao : IProfileDao
    {
        private const string StorageFolder = "Profiles";
        private const string FileExtension = ".json";

        public ObservableCollection<Profile> GetAll()
        {
            var profiles = new ObservableCollection<Profile>();
            var files = FileHelper.GetSpecificFiles(FileExtension, StorageFolder);

            //TODO: Parallel?
            if (files.Any())
            {
                foreach (var file in files)
                {
                    var json = File.ReadAllText(file.FullName);
                    profiles.Add(JsonConvert.DeserializeObject<Profile>(json));
                }
            }

            return profiles;
        }

        public Profile Get()
        {
            throw new NotImplementedException();
        }

        public void Insert(Profile profile)
        {
            var json = JsonConvert.SerializeObject(profile, Formatting.Indented);
            FileHelper.Save(json, profile.Name, FileExtension, StorageFolder);
        }

        public void Update(Profile profile)
        {
            throw new NotImplementedException();
        }

        public void Delete(Profile profile)
        {
            FileHelper.Delete(profile.Name + FileExtension, StorageFolder);
        }
    }
}
