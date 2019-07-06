using A3ServerTool.Helpers;
using A3ServerTool.Models;
using A3ServerTool.Models.Config;
using Interchangeable.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Storage
{
    public class ArmaProfileDao : IConfigDao<ArmaProfile>
    {
        private const string UserFolder = "Users";
        private const string GameProfileFileExtension = ".Arma3Profile";

        /// <inheritdoc/>
        public ArmaProfile Get(Profile profile)
        {
            var file = FileHelper.GetFile(Path.Combine(Constants.RootFolder,
                Constants.ServerProfileFolder,
                profile.Id.ToString(),
                UserFolder,
                Constants.GameProfileName,
                Constants.GameProfileName)
                    + GameProfileFileExtension);
            if (file == null) return null;

            var properties = TextManager.ReadFileLineByLine(file);
            if (!properties.Any()) return null;

            var resultArmaProfile = UniversalParser.Parse<ArmaProfile>(properties);

            //TODO: It will be needed in the future 
            //(maybe, BIS documentation is lacking information about either specifying 
            //specific profile folder or folder with all profiles
            //var lastPathIndex = file.DirectoryName.LastIndexOf("\\");
            //if(lastPathIndex != -1)
            //{
            //    resultArmaProfile.FileLocation = file.DirectoryName.Remove(lastPathIndex);
            //}
            //else
            //{
            //    resultArmaProfile.FileLocation = file.DirectoryName;
            //}

            resultArmaProfile.FileLocation = file.DirectoryName;
            return resultArmaProfile;
        }

        public void Save(Profile profile)
        {
            var armaProfileDto = new SaveDataDto
            {
                Content = string.Join("\r\n", UniversalParser.ConvertToText(profile.ArmaProfile)),
                FileExtension = GameProfileFileExtension,
                FileName = Constants.GameProfileName,
                Folders = new List<string>
                    {
                        Constants.RootFolder,
                        Constants.ServerProfileFolder,
                        profile.Id.ToString(),
                        "Users",
                        Constants.GameProfileName
                    }
            };

            //profile.ArmaProfile.FileLocation = armaProfileDto.GetFullPath();
            FileHelper.Save(armaProfileDto);
        }
    }
}
