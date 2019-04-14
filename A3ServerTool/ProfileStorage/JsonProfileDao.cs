using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using A3ServerTool.Models;
using A3ServerTool.Models.ConfigStorages;
using Interchangeable.IO;
using Newtonsoft.Json;

namespace A3ServerTool.ProfileStorage
{
    public class JsonProfileDao : IProfileDao
    {
        private const string StorageFolder = "Profiles";
        private const string MetadataFileExtension = ".json";

        private readonly JsonSerializerSettings _serializerSettings;

        public JsonProfileDao(JsonSerializerSettings settings)
        {
            _serializerSettings = settings;
            _serializerSettings.TypeNameHandling = TypeNameHandling.Objects;
            _serializerSettings.ObjectCreationHandling = ObjectCreationHandling.Replace;

            if (!FileHelper.CheckIfFolderExists(StorageFolder))
            {
                FileHelper.CreateFolder(StorageFolder);
            }
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
                var metadata = files.FirstOrDefault(x => x.Extension == MetadataFileExtension);
                profile = JsonConvert.DeserializeObject<Profile>(TextManager.ReadFileAsWhole(metadata), _serializerSettings);

                var configFiles = files.Where(x => x.Extension == BasicConfig.FileExtension);
                foreach (var file in configFiles)
                {
                    var properties = TextManager.ReadFileLineByLine(file);
                    profile.BasicConfig = TextParseManager.Parse<BasicConfig>(properties);

                    //TODO: Same thing for config.cfg
                }

                profiles.Add(profile);
            });

            return profiles;
        }

        public Profile Get(Profile profile)
        {
            return GetAll().FirstOrDefault(p => Equals(profile.Id, p.Id));
        }


        public void Save(Profile profile)
        {
            var metadataDto = new SaveDataDto
            {
                Content = JsonConvert.SerializeObject(profile, Formatting.Indented, _serializerSettings),
                FileExtension = MetadataFileExtension,
                FileName = "Main",
                Folders = new List<string>
                {
                    StorageFolder,
                    profile.Id.ToString()
                }
            };
            FileHelper.Save(metadataDto);

            if(profile.BasicConfig != null)
            {
                var basicDto = new SaveDataDto
                {
                    Content = string.Join("\r\n", TextParseManager.ConvertToTextLines(profile.BasicConfig)),
                    FileExtension = BasicConfig.FileExtension,
                    FileName = BasicConfig.FileName,
                    Folders = new List<string>
                    {
                        StorageFolder,
                        profile.Id.ToString()
                    }
                };

                FileHelper.Save(basicDto);
            }
        }

        public void Delete(Profile profile)
        {
            var dto = new SaveDataDto
            {
                Folders = new List<string>
                {
                    StorageFolder,
                    profile.Id.ToString()
                }
            };

            FileHelper.DeleteFolder(dto);
        }
    }
}
