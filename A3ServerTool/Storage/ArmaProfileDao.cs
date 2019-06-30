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

        private readonly IArmaProfileParsingHelper _parsingHelper;

        public ArmaProfileDao(IArmaProfileParsingHelper parsingHelper)
        {
            _parsingHelper = parsingHelper;
        }

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

            return _parsingHelper.GetProfile(properties);
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

            profile.ArmaProfile.FileLocation = armaProfileDto.GetFullPath();
            FileHelper.Save(armaProfileDto);
        }
    }
}
