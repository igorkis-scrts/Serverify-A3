using A3ServerTool.Attributes;
using A3ServerTool.Enums;
using A3ServerTool.Models.Config;
using Interchangeable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A3ServerTool.Helpers
{
    /// <summary>
    /// Provides little helper methods to help with parsing .Arma3Profile file
    /// or converting class instance into .Arm3Profile file back.
    /// </summary>
    /// <seealso cref="A3ServerTool.Helpers.IArmaProfileParsingHelper" />
    public class ArmaProfileParsingHelper : IArmaProfileParsingHelper
    {
        /// <inheritdoc/>
        public ArmaProfile GetProfile(IEnumerable<string> properties)
        {
            //parsing file with ordinary parser
            var result = TextParseHandler.Parse<ArmaProfile>(properties);

            var nameToValueDictionary = TextParseHandler.ConvertFromTextToDictionary(properties.ToArray(), typeof(ArmaProfile));

            foreach (var property in typeof(ArmaProfile).GetProperties())
            {
                var configProperty = property.GetCustomAttributes(typeof(ConfigProperty), false).FirstOrDefault() as ConfigProperty;
                if (configProperty?.IgnoreParsing != false) continue;

                nameToValueDictionary.TryGetValue(configProperty.PropertyName, out var value);
                if (value == null) continue;

                if (property.PropertyType == typeof(AiLevelPresetType))
                {
                    Enum.TryParse(value.Replace("\"", string.Empty).FirstLetterToUpperCase(), out AiLevelPresetType aiPreset);
                    result.AiLevelPreset = aiPreset;
                }

                if (property.PropertyType == typeof(DifficultyType))
                {
                    Enum.TryParse(value.Replace("\"", string.Empty).FirstLetterToUpperCase(), out DifficultyType difficulty);
                    result.Difficulty = difficulty;
                }
            }

            return result;
        }
    }
}
