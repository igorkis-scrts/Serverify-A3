using A3ServerTool.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Interchangeable.IO;
using A3ServerTool.Models.Config;
using A3ServerTool.Helpers;

namespace A3ServerTool.Storage
{
    /// <inheritdoc/>
    public class ServerConfigDao : IConfigDao<ServerConfig>
    {
        private const string ConfigFileExtension = ".cfg";
        private const string ConfigFileName = "server";

        private readonly IMissionDirector _missionDirector;

        public ServerConfigDao(IMissionDirector missionDirector)
        {
            _missionDirector = missionDirector;
        }

        /// <inheritdoc/>
        public ServerConfig Get(Profile profile)
        {
            var file = FileHelper.GetFile(Path.Combine(Constants.RootFolder, Constants.ServerProfileFolder, profile.Id.ToString(),
                  ConfigFileName) + ConfigFileExtension);
            if (file == null) return null;

            var properties = TextManager.ReadFileLineByLine(file);
            if (!properties.Any()) return null;

            var result = TextParseHandler.Parse<ServerConfig>(properties);
            result.FileLocation = file.FullName;
            result.Missions = _missionDirector.GetMissions(properties, GetGameInstallationPath(profile.ArgumentSettings.ExecutablePath)).ToList();
            
            foreach(var mission in result.Missions)
            {
                if(result.MissionWhitelist?.Any(m => m == mission.Name) == true)
                {
                    mission.IsWhitelisted = true;
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public void Save(Profile profile)
        {
            var configDto = new SaveDataDto
            {
                Content = string.Join("\r\n", TextParseHandler.ConvertToText(profile.ServerConfig)),
                FileExtension = ConfigFileExtension,
                FileName = ConfigFileName,
                Folders = new List<string>
                    {
                        Constants.RootFolder,
                        Constants.ServerProfileFolder,
                        profile.Id.ToString()
                    }
            };

            configDto.Content = _missionDirector.SaveMissions(profile.ServerConfig.Missions, configDto.Content);

            profile.ServerConfig.FileLocation = Path.Combine(Constants.RootFolder, configDto.GetFullPath());
            FileHelper.Save(configDto);
        }

        /// <summary>
        /// Gets the game installation path.
        /// </summary>
        /// <param name="executablePath">The executable path.</param>
        /// <returns>Installation path</returns>
        public static string GetGameInstallationPath(string executablePath)
        {
            if (string.IsNullOrWhiteSpace(executablePath)) return string.Empty;

            var arrayPath = executablePath.Split('\\').ToList();
            arrayPath.RemoveAll(x => x.Contains(".exe"));

            return string.Join("\\", arrayPath);
        }
    }
}
