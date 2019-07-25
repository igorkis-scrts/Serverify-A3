using A3ServerTool;
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
    /// <inheritdoc/>
    public class BasicConfigDao : IConfigDao<BasicConfig>
    {
        private const string ConfigFileExtension = ".cfg";
        private const string ConfigFileName = "basic";

        private readonly IUniversalParser _parser;

        public BasicConfigDao(IUniversalParser parser)
        {
            _parser = parser;
        }

        /// <inheritdoc/>
        public BasicConfig Get(Profile profile)
        {
            var file = FileHelper.GetFile(Path.Combine(Constants.RootFolder, Constants.ServerProfileFolder, profile.Id.ToString(),
                    ConfigFileName) + ConfigFileExtension);
            if (file == null) return null;

            var properties = TextManager.ReadFileLineByLine(file);
            if (!properties.Any()) return null;

            var result = _parser.Parse<BasicConfig>(properties);
            result.FileLocation = file.FullName;
            return result;
        }

        /// <inheritdoc/>

        /// <inheritdoc/>
        public void Save(Profile profile)
        {
            var configDto = new SaveDataDto
            {
                Content = string.Join("\r\n", _parser.ConvertToText(profile.BasicConfig)),
                FileExtension = ConfigFileExtension,
                FileName = ConfigFileName,
                Folders = new List<string>
                    {
                        Constants.RootFolder,
                        Constants.ServerProfileFolder,
                        profile.Id.ToString()
                    }
            };

            profile.BasicConfig.FileLocation = configDto.GetFullPath();
            FileHelper.Save(configDto);
        }
    }
}
