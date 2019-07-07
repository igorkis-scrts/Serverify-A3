using A3ServerTool.Helpers;
using A3ServerTool.Models;
using A3ServerTool.Models.Config;
using Interchangeable.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace A3ServerTool.Storage
{
    /// <summary>
    /// Provides access and manipulation methods for various armaProfile subclass.
    /// </summary>
    /// <seealso cref="A3ServerTool.Storage.IConfigDao{A3ServerTool.Models.Config.ArmaProfile}" />
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
            resultArmaProfile.FileLocation = file.FullName;
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
                        UserFolder,
                        Constants.GameProfileName
                    }
            };

            profile.ArmaProfile.FileLocation = armaProfileDto.GetFullPath();
            FileHelper.Save(armaProfileDto);
        }
    }
}
