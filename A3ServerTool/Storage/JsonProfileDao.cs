using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using A3ServerTool.Models;
using A3ServerTool.Storage;
using Interchangeable.IO;
using Newtonsoft.Json;

namespace A3ServerTool.Storage
{
    public class JsonProfileDao : IDao<Profile>
    {
        private const string StorageFolder = "Profiles";
        private const string FileExtension = ".json";

        private readonly JsonSerializerSettings _serializerSettings;

        public JsonProfileDao()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };

            if (!FileHelper.CheckIfFolderExists(StorageFolder))
            {
                FileHelper.CreateFolder(StorageFolder);
            }
        }

        IList<Profile> IDao<Profile>.GetAll()
        {
            var profiles = new List<Profile>();
            var profileFolders = FileHelper.GetFolder(StorageFolder);
            if (!profileFolders.Any()) return profiles;

            Parallel.ForEach(profileFolders, folder =>
            {
                var files = FileHelper.GetAllFiles(folder);

                Profile profile;
                var metadata = files.FirstOrDefault(x => x.Extension == FileExtension);
                profile = JsonConvert.DeserializeObject<Profile>(TextManager.ReadFileAsWhole(metadata), _serializerSettings);

                //var configFiles = files.Where(x => x.Extension == BasicConfig.FileExtension);
                //foreach (var file in configFiles)
                //{
                //    var properties = TextManager.ReadFileLineByLine(file);
                //    profile.BasicConfig = TextParseHandler.Parse<BasicConfig>(properties);

                //    //TODO: Same thing for config.cfg
                //}

                profiles.Add(profile);
            });

            return profiles;
        }

        public ObservableCollection<Profile> GetAll()
        {
            var profiles = new ObservableCollection<Profile>();
            var profileFolders = FileHelper.GetFolder(StorageFolder);
            if (!profileFolders.Any()) return profiles;

            Parallel.ForEach(profileFolders, folder =>
            {
                var files = FileHelper.GetAllFiles(folder);

                Profile profile;
                var metadata = files.FirstOrDefault(x => x.Extension == FileExtension);
                profile = JsonConvert.DeserializeObject<Profile>(TextManager.ReadFileAsWhole(metadata), _serializerSettings);

                //var configFiles = files.Where(x => x.Extension == BasicConfig.FileExtension);
                //foreach (var file in configFiles)
                //{
                //    var properties = TextManager.ReadFileLineByLine(file);
                //    profile.BasicConfig = TextParseHandler.Parse<BasicConfig>(properties);

                //    //TODO: Same thing for config.cfg
                //}

                profiles.Add(profile);
            });

            return profiles;
        }

        public Profile Get(Profile item)
        {
            return GetAll().FirstOrDefault(p => Equals(item.Id, p.Id));
        }


        public void Save(Profile item)
        {
            var metadataDto = new SaveDataDto
            {
                Content = JsonConvert.SerializeObject(item, Formatting.Indented, _serializerSettings),
                FileExtension = FileExtension,
                FileName = "Main",
                Folders = new List<string>
                {
                    StorageFolder,
                    item.Id.ToString()
                }
            };
            FileHelper.Save(metadataDto);
        }

        public void Delete(Profile item)
        {
            var dto = new SaveDataDto
            {
                Folders = new List<string>
                {
                    StorageFolder,
                    item.Id.ToString()
                }
            };

            FileHelper.DeleteFolder(dto);
        }
    }
}
