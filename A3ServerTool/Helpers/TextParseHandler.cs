using A3ServerTool.Attributes;
using A3ServerTool.Models.Config;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace A3ServerTool
{
    /// <summary>
    /// Provides instruments to convert array of text lines into object and back.
    /// </summary>
    public static class TextParseHandler
    {
        /// <summary>
        /// Converts lines of properties with values from config file into object instance.
        /// </summary>
        /// <returns>Instance of T class filled with properties.</returns>
        public static T Parse<T>(IEnumerable<string> configProperties)
        {
            var textProperties = configProperties.ToArray();
            if (!textProperties.Any()) return default;

            var result = (T)Activator.CreateInstance(typeof(T));

            Dictionary<string, string> nameToValueDictionary = ConvertFromTextToDictionary(textProperties, result.GetType());

            foreach (var property in typeof(T).GetProperties())
            {
                var configProperty = property.GetCustomAttributes(typeof(ConfigProperty), false).FirstOrDefault() as ConfigProperty;

                if (configProperty?.IgnoreParsing != false) continue;

                nameToValueDictionary.TryGetValue(configProperty.PropertyName, out var value);
                if (value == null) continue;

                if (property.PropertyType == typeof(int))
                {
                    property.SetValue(result, Convert.ToInt32(value));
                }
                else if (property.PropertyType == typeof(int?))
                {
                    if (int.TryParse(value, out int nullInt))
                    {
                        property.SetValue(result, nullInt);
                    }
                }
                else if (property.PropertyType == typeof(float))
                {
                    property.SetValue(result, float.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture));
                }
                else if (property.PropertyType == typeof(float?))
                {
                    if (float.TryParse(value, out float nullFloat))
                    {
                        property.SetValue(result, nullFloat);
                    }
                }
                else if (property.PropertyType == typeof(string[]))
                {
                    property.SetValue(result, value?.Split(','));
                }
                else if (property.PropertyType == typeof(int[]))
                {
                    //TODO: very dirty, do better
                    if(property.Name == "SlowNetworkKickRules")
                    {
                        value = Regex.Replace(value, @"\s+", "");
                        var valueAsIntArray = value.Split(',').Select(int.Parse).ToArray();
                        property.SetValue(result, valueAsIntArray);
                    }
                }
                else if (property.PropertyType == typeof(List<string>))
                {
                    value = Regex.Replace(value, @"\s+", "");
                    property.SetValue(result, value?.Split(',').ToList());
                }
                else if (property.PropertyType == typeof(string))
                {
                    value = value?.Replace("\"", string.Empty);
                    property.SetValue(result, Convert.ToString(value));
                }
                else if (property.PropertyType == typeof(bool))
                {
                    property.SetValue(result, Convert.ToBoolean(value));
                }
            }

            return result;
        }

        /// <summary>
        /// Converts object properties to text representation.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>List of string properties.</returns>
        public static List<string> ConvertToText<T>(T instance)
        {
            var result = new List<string>();

            foreach (var property in instance.GetType().GetProperties())
            {
                var configProperty = property.GetCustomAttributes(true).FirstOrDefault() as ConfigProperty;
                if (configProperty?.IgnoreParsing != false || configProperty == null) continue;

                var value = property.GetValue(instance, null);

                if (property.PropertyType == typeof(string))
                {
                    value = "\"" + value + "\"";
                }

                else if (property.PropertyType == typeof(int[]))
                {
                    if (value is int[] valueAsArray)
                    {
                        if (valueAsArray.Length == 0) continue;
                        value = ParseArrayProperty(valueAsArray);
                    }
                }

                else if (property.PropertyType == typeof(string[]))
                {
                    if (value is string[] valueAsArray)
                    {
                        if (!valueAsArray.Any() || valueAsArray.Any(x => x == null)) continue;
                        value = ParseArrayProperty(valueAsArray);
                    }

                    if (string.IsNullOrWhiteSpace(value?.ToString())) continue;
                }
                else if (property.PropertyType == typeof(List<string>))
                {
                    if (value is List<string> valueAsList)
                    {
                        if (!valueAsList.Any()  ||valueAsList.Any(x => x == null)) continue;
                        value = ParseArrayProperty(valueAsList.ToArray());
                    }

                    if (string.IsNullOrWhiteSpace(value?.ToString())) continue;
                }
                else if (property.PropertyType == typeof(bool))
                {
                    value = value.ToString().ToLowerInvariant();
                }
                else if (property.PropertyType == typeof(int?))
                {
                    if (!int.TryParse(value?.ToString(), out int nullInt))
                    {
                        continue;
                    }
                }
                else if (property.PropertyType == typeof(float?))
                {
                    if (!float.TryParse(value?.ToString(), out float nullFloat))
                    {
                        continue;
                    }
                }

                var line = configProperty.PropertyName + " = " + value + ";";
                result.Add(line);
            }
            return result;
        }

        /// <summary>
        /// Parses the array config properties.
        /// </summary>
        /// <param name="valueAsArray">The value as array.</param>
        /// <returns></returns>
        public static string ParseArrayProperty(string[] valueAsArray)
        {
            if (valueAsArray == null || (valueAsArray.Length == 1 && string.IsNullOrEmpty(valueAsArray[0]))) return string.Empty;

            //TODO: new attribute to check if property has empty lines 
            for (int i = 0; i < valueAsArray.Length; i++)
            {
                valueAsArray[i] = Regex.Replace(valueAsArray[i], @"[^\S ]+", string.Empty);
                valueAsArray[i] = valueAsArray[i].Replace("\"", string.Empty);

                if (string.IsNullOrWhiteSpace(valueAsArray[i]))
                {
                    valueAsArray[i] = Regex.Replace(valueAsArray[i], @"\s+", string.Empty);
                }
                valueAsArray[i] = "\"" + valueAsArray[i] + "\"";
            }

            if (valueAsArray.Any())
            {
                valueAsArray[0] = "\t" + valueAsArray[0];
            }

            return "{\n" + string.Join("\n\t,", valueAsArray) + "\n}";
        }

        /// <summary>
        /// Parses the array config properties.
        /// </summary>
        /// <param name="valueAsArray">The value as array.</param>
        /// <returns></returns>
        public static string ParseArrayProperty(int[] valueAsArray)
        {
            if (valueAsArray?.Any() != true) return string.Empty;
            var result = "\t" + string.Join("\n\t,", valueAsArray) + "\n}";
            return "{\n" + result;
        }

        /// <summary>
        /// Converts from plain text to properties dictionary.
        /// </summary>
        /// <param name="textProperties">Text properties.</param>
        /// <returns>"Property-value dictionary."</returns>
        public static Dictionary<string, string> ConvertFromTextToDictionary(string[] textProperties, Type type)
        {
            var nameToValueDictionary = new Dictionary<string, string>();

            for (int i = 0; i < textProperties.Length; i++)
            {
                var splittedProperty = textProperties[i].Split('=')
                    .Where(x => x != "=")
                    .Select(x => x.Trim())
                    .ToArray();
                if (splittedProperty.Length != 2) continue;

                if (splittedProperty[0].Contains("[]"))
                {
                    string value = string.Empty;

                    for (int j = i + 1; j < textProperties.Length; j++)
                    {
                        if (textProperties[j] == "};")
                        {
                            value = Regex.Replace(value, @"[^\S ]+", "");
                            nameToValueDictionary.Add(splittedProperty[0], value?.Replace("\"", string.Empty));
                            break;
                        }

                        value += textProperties[j];
                    }
                }
                else if (type == typeof(ServerConfig)
                    && (splittedProperty[0] == "template" || splittedProperty[0] == "difficulty"))
                {
                    //there is separate parser for missions, lets ignore these properties
                    continue;
                }
                else
                {
                    var separatorIndex = splittedProperty[1].LastIndexOf(";");
                    if (separatorIndex != -1)
                    {
                        splittedProperty[1] = splittedProperty[1].Remove(separatorIndex, 1);
                    }
                    else
                    {
                        splittedProperty[1] = splittedProperty[1].Replace(";", string.Empty);
                    }

                    nameToValueDictionary.Add(splittedProperty[0], splittedProperty[1]);
                }
            }

            return nameToValueDictionary;
        }
    }
}
