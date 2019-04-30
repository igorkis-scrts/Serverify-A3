using A3ServerTool.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interchangeable.IO;
using A3ServerTool.Models.Config;

namespace A3ServerTool.Storage
{
    /// <inheritdoc/>
    public class ServerConfigDao : IConfigDao<ServerConfig>
    {
        /// <inheritdoc/>
        public ServerConfig Get(IProfile profile)
        {
            var file = FileHelper.GetFile(Path.Combine(Profile.StorageFolder, profile.Id.ToString(),
                   profile.BasicConfig.FileName) + profile.BasicConfig.FileExtension);
            if (file == null) return null;

            var properties = TextManager.ReadFileLineByLine(file);
            if (!properties.Any()) return null;

            var result = TextParseHandler.Parse<ServerConfig>(properties);
            result.FileLocation = file.FullName;
            return result;
        }

        /// <inheritdoc/>
        public void Save(IProfile profile)
        {
            var configDto = new SaveDataDto
            {
                Content = string.Join("\r\n", TextParseHandler.ConvertToText(profile.ServerConfig)),
                FileExtension = profile.ServerConfig.FileExtension,
                FileName = profile.ServerConfig.FileName,
                Folders = new List<string>
                    {
                        Profile.StorageFolder,
                        profile.Id.ToString()
                    }
            };

            profile.ServerConfig.FileLocation = configDto.GetFullPath();
            FileHelper.Save(configDto);
        }
    }
}
