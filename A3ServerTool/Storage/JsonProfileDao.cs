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
        private const string ServerProfileFileExtension = ".json";

        private readonly JsonSerializerSettings _serializerSettings;

        public JsonProfileDao()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };

            if (!FileHelper.CheckFolderExistence(Path.Combine(Constants.RootFolder, Constants.ServerProfileFolder)))
            {
                FileHelper.CreateFolder(Path.Combine(Constants.RootFolder, Constants.ServerProfileFolder));
            }
        }

        /// <summary>
        /// Gets all stored profiles.
        /// </summary>
        /// <returns>Observable collection with all stored profiles.</returns>
        public IList<Profile> GetAll()
        {
            var profiles = new ObservableCollection<Profile>();
            var profileFolders = FileHelper.GetFolder(Path.Combine(Constants.RootFolder, Constants.ServerProfileFolder));
            if (!profileFolders.Any()) return profiles;

            Parallel.ForEach(profileFolders, folder =>
            {
                var files = FileHelper.GetAllFiles(folder);

                Profile profile;
                var metadata = files.FirstOrDefault(x => x.Extension == ServerProfileFileExtension);
                if (metadata != null)
                {
                    profile = JsonConvert.DeserializeObject<Profile>(TextManager.ReadFileAsWhole(metadata), _serializerSettings);
                    profile.ProfilePath = folder.FullName;
                    profiles.Add(profile);
                }
            });

            return profiles;
        }

        /// <summary>
        /// NOT IMPLEMENTED!
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public IList<Profile> GetAll(string location)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the certain profile content.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Stored profile.</returns>
        public Profile Get(Profile item)
        {
            return GetAll().FirstOrDefault(p => Equals(item.Id, p.Id));
        }


        /// <summary>
        /// Saves the specified profile.
        /// </summary>
        /// <param name="profile">Profile.</param>
        public void Save(Profile profile)
        {
            var metadataDto = new SaveDataDto
            {
                Content = JsonConvert.SerializeObject(profile, Formatting.Indented, _serializerSettings),
                FileExtension = ServerProfileFileExtension,
                FileName = "Main",
                Folders = new List<string>
                {
                    Constants.RootFolder,
                    Constants.ServerProfileFolder,
                    profile.Id.ToString()
                }
            };

            profile.ProfilePath = Path.Combine(Constants.RootFolder, Constants.ServerProfileFolder, profile.Id.ToString());
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
                    Path.Combine(Constants.RootFolder, Constants.ServerProfileFolder),
                    item.Id.ToString()
                }
            };

            FileHelper.DeleteFolder(dto);
        }
    }
}
