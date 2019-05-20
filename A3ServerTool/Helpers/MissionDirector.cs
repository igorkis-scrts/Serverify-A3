using A3ServerTool.Models;
using A3ServerTool.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using A3ServerTool.Enums;
using Interchangeable;
using A3ServerTool.Storage;
using System.Threading.Tasks;

namespace A3ServerTool.Helpers
{
    /// <inheritdoc>
    public class MissionDirector : IMissionDirector
    {
        private IDao<Mission> _missionDao;

        public MissionDirector(IDao<Mission> missionDao)
        {
            _missionDao = missionDao;
        }

        /// <inheritdoc>
        public IList<Mission> GetMissions(IEnumerable<string> configProperties, string folderPath)
        {
            var configMissions = GetMissionsFromConfig(configProperties);
            var storedMissions = GetMissionsFromStorage(folderPath);

            Parallel.ForEach(storedMissions, mission =>
            {
                var configMission = configMissions.FirstOrDefault(x => x.Name == mission.Name);

                if(configMission != null)
                {
                    mission.IsSelected = true;
                    mission.Difficulty = configMission.Difficulty;
                }
            });

            return storedMissions;
        }

        /// <inheritdoc>
        public void SaveMissions(IEnumerable<Mission> missions)
        {
            throw new NotImplementedException();
        }

        private List<Mission> GetMissionsFromConfig(IEnumerable<string> configProperties)
        {
            var missionIndex = configProperties.ToList().FindIndex(x => x.Trim() == "class Missions");
            if (missionIndex == -1) return new List<Mission>();

            var result = new List<Mission>();

            var missionLines = configProperties.ToList().GetRange(missionIndex, configProperties.Count() - missionIndex);
            if (!missionLines.Any()) return result;

            var missionToSettingsDictionary = new Dictionary<string, Dictionary<string, string>>();

            for (int i = 1; i < missionLines.Count; i++)
            {
                if (missionLines[i].Contains("class Mission"))
                {
                    var settingToValueDictionary = new Dictionary<string, string>();

                    for (int j = i + 1; j < missionLines.Count; j++)
                    {
                        var splittedProperty = missionLines[j].Split('=')
                            .Where(x => x != "=")
                            .Select(x => x.Trim())
                            .Select(x => x.Replace("\"", string.Empty))
                            .Select(x => x.Replace(";", string.Empty))
                            .ToArray();
                        if (splittedProperty.Length != 2) continue;

                        settingToValueDictionary.Add(splittedProperty[0], splittedProperty[1]);
                        if (settingToValueDictionary.Count == 2)
                        {
                            break;
                        }
                    }

                    if (settingToValueDictionary.Count == 2)
                    {
                        missionToSettingsDictionary.Add(missionLines[i].Replace("\t", string.Empty), settingToValueDictionary);
                    }
                }
            }

            foreach (var settingsHeader in missionToSettingsDictionary)
            {
                var mission = new Mission
                {
                    Name = settingsHeader.Value["template"],
                    IsSelected = true
                };

                Enum.TryParse(settingsHeader.Value["difficulty"].FirstLetterToUpperCase(), out DifficultyType difficulty);
                mission.Difficulty = difficulty;
                result.Add(mission);
            }

            return result;
        }

        private List<Mission> GetMissionsFromStorage(string folderPath)
        {
            return _missionDao.GetAll(folderPath).ToList();
        }
    }
}
