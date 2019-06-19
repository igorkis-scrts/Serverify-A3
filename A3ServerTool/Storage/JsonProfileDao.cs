using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using A3ServerTool.Models;
using Interchangeable.IO;
using Newtonsoft.Json;

namespace A3ServerTool.Storage
{
    public class JsonProfileDao : IDao<Profile>
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonProfileDao()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };

            if (!FileHelper.CheckFolderExistence(Path.Combine(Constants.RootFolder, Profile.StorageFolder)))
            {
                FileHelper.CreateFolder(Path.Combine(Constants.RootFolder, Profile.StorageFolder));
            }
        }

        IList<Profile> IDao<Profile>.GetAll()
        {
            var profiles = new List<Profile>();
            var profileFolders = FileHelper.GetFolder(Path.Combine(Constants.RootFolder, Profile.StorageFolder));
            if (!profileFolders.Any()) return profiles;

            Parallel.ForEach(profileFolders, folder =>
            {
                var files = FileHelper.GetAllFiles(folder);

                Profile profile;
                var metadata = files.FirstOrDefault(x => x.Extension == Constants.ServerProfileFileExtension);
                profile = JsonConvert.DeserializeObject<Profile>(TextManager.ReadFileAsWhole(metadata), _serializerSettings);

                profiles.Add(profile);
            });

            return profiles;
        }

        public IList<Profile> GetAll(string location)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all stored profiles.
        /// </summary>
        /// <returns>Observable collection with all stored profiles.</returns>
        public ObservableCollection<Profile> GetAll()
        {
            var profiles = new ObservableCollection<Profile>();
            var profileFolders = FileHelper.GetFolder(Path.Combine(Constants.RootFolder, Profile.StorageFolder));
            if (!profileFolders.Any()) return profiles;

            Parallel.ForEach(profileFolders, folder =>
            {
                var files = FileHelper.GetAllFiles(folder);

                Profile profile;
                var metadata = files.FirstOrDefault(x => x.Extension == Constants.ServerProfileFileExtension);
                profile = JsonConvert.DeserializeObject<Profile>(TextManager.ReadFileAsWhole(metadata), _serializerSettings);


                profiles.Add(profile);
            });

            return profiles;
        }

        public Profile Get(Profile item)
        {
            return GetAll().FirstOrDefault(p => Equals(item.Id, p.Id));
        }


        /// <summary>
        /// Saves the specified profile.
        /// </summary>
        /// <param name="item">Profile.</param>
        public void Save(Profile item)
        {
            var metadataDto = new SaveDataDto
            {
                Content = JsonConvert.SerializeObject(item, Formatting.Indented, _serializerSettings),
                FileExtension = Constants.ServerProfileFileExtension,
                FileName = "Main",
                Folders = new List<string>
                {
                    Constants.RootFolder,
                    Profile.StorageFolder,
                    item.Id.ToString()
                }
            };
            FileHelper.Save(metadataDto);
        }

        /// <summary>
        /// Deletes the specified profile.
        /// </summary>
        /// <param name="item">Profile.</param>
        public void Delete(Profile item)
        {
            var dto = new SaveDataDto
            {
                Folders = new List<string>
                {
                    //TODO: Fix it without path.combine
                    Path.Combine(Constants.RootFolder, Profile.StorageFolder),
                    item.Id.ToString()
                }
            };

            FileHelper.DeleteFolder(dto);
        }
    }
}
