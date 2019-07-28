using A3ServerTool.Attributes;
using A3ServerTool.Models.Config;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace A3ServerTool.Helpers
{
    //TODO: MAJOR REFACTORING BADLY NEEDED
    /// <summary>
    /// Provides instruments to convert array of text lines into object and back.
    /// </summary>
    public class UniversalParser : IUniversalParser
    {
        /// <summary>
        /// Converts lines of properties with values from config file into object instance.
        /// </summary>
        /// <returns>Instance of T class filled with properties.</returns>
        public T Parse<T>(IEnumerable<string> configProperties)
        {
            try
            {
                if (!typeof(IConfig).IsAssignableFrom(typeof(T)))
                {
                    throw new NotSupportedException("T should implement IConfig!");
                }

                var result = (T)Activator.CreateInstance(typeof(T));

                if (configProperties?.Any() != true) return result;

                var textProperties = configProperties.ToArray();
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
                        if (int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int nullInt))
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
                        if (float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out float nullFloat))
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
                        if (property.Name == "SlowNetworkKickRules")
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
            catch (NotSupportedException)
            {
                throw;
            }
        }

        /// <summary>
        /// Converts object properties with values to config file content string.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Config file in text representation.</returns>
        public string ConvertToText<T>(T instance)
        {
            try
            {
                if (!(instance is IConfig))
                {
                    throw new NotSupportedException("Provided type is not a IConfig.");
                }

                WrappingClassAttribute previousWrappingClass = null;
                int maxTab = 0;
                bool hasZeroTabBracket = false;

                var stringBuilder = new StringBuilder();
                var properties = instance.GetType()
                    .GetProperties()
                    .Select(x => new
                    {
                        PropertyInfo = x,
                        Attribute = (WrappingClassAttribute)Attribute.GetCustomAttribute(x, typeof(WrappingClassAttribute))
                    })
                    .OrderByDescending(x => x.Attribute != null ? x.Attribute.ClassNames : new string[0], new StringArrayComparer())
                    .Select(x => x.PropertyInfo)
                    .Select(x => new
                    {
                        Property = x,
                        Attribute = (ConfigProperty)Attribute.GetCustomAttribute(x, typeof(ConfigProperty))
                    })
                    .Where(x => x.Attribute != null)
                    .Select(x => x.Property)
                    .ToArray();

                for (int p = 0; p < properties.Length; p++)
                {
                    var configProperty = properties[p].GetCustomAttributes(typeof(ConfigProperty), false).FirstOrDefault() as ConfigProperty;
                    var wrappingClass = properties[p].GetCustomAttributes(typeof(WrappingClassAttribute), false).FirstOrDefault() as WrappingClassAttribute;

                    if (configProperty == null)
                    {
                        continue;
                    }

                    var classNames = wrappingClass?.ClassNames;
                    var previousClassNames = previousWrappingClass?.ClassNames;

                    if (previousClassNames == null
                        || classNames?.SequenceEqual(previousClassNames) != true)
                    {
                        for (int i = 0; i < classNames?.Length; i++)
                        {
                            if ((previousClassNames != null && i > previousClassNames.Length - 1)
                                || (previousClassNames != null && classNames[i] == previousClassNames[i]))
                            {
                                continue;
                            }

                            if (i != 0)
                            {
                                var tab = new string('\t', i);
                                maxTab = i;
                                stringBuilder.Append(tab).Append("class ")
                                    .AppendLine(classNames[i])
                                    .Append(tab).AppendLine("{");
                            }
                            else
                            {
                                stringBuilder.Append("class ")
                                    .AppendLine(classNames[i])
                                    .AppendLine("{");
                                hasZeroTabBracket = true;
                            }
                        }
                    }

                    previousWrappingClass = wrappingClass;
                    var value = properties[p].GetValue(instance, null);

                    value = ApplyConfigPropertyFlags(configProperty, value);

                    if (properties[p].PropertyType == typeof(int?))
                    {
                        if (!int.TryParse(value?.ToString(), out int nullInt))
                        {
                            continue;
                        }
                    }
                    else if (properties[p].PropertyType == typeof(float?))
                    {
                        if (!float.TryParse(value?.ToString(), out float nullFloat))
                        {
                            continue;
                        }
                    }
                    else if (properties[p].PropertyType == typeof(bool))
                    {
                        value = value.ToString().ToLowerInvariant();
                    }
                    else if (properties[p].PropertyType == typeof(List<string>))
                    {
                        if (value is List<string> valueAsList)
                        {
                            if (!valueAsList.Any() || valueAsList.Any(x => x == null)) continue;
                            value = ParseArrayProperty(valueAsList.ToArray());
                        }

                        if (string.IsNullOrWhiteSpace(value?.ToString())) continue;
                    }
                    else if (properties[p].PropertyType == typeof(string[]))
                    {
                        if (value is string[] valueAsArray)
                        {
                            if (!valueAsArray.Any() || valueAsArray.Any(x => x == null)) continue;
                            value = ParseArrayProperty(valueAsArray);
                        }

                        if (string.IsNullOrWhiteSpace(value?.ToString())) continue;
                    }
                    else if (properties[p].PropertyType == typeof(int[]))
                    {
                        if (value is int[] valueAsArray)
                        {
                            if (valueAsArray.Length == 0) continue;
                            value = ParseArrayProperty(valueAsArray);
                        }
                    }

                    //write string with property and value
                    stringBuilder.Append(new string('\t', wrappingClass == null ? 0 : maxTab + 1))
                        .Append(configProperty.PropertyName)
                        .Append(" = ")
                        .Append(value)
                        .AppendLine(";");

                    if (p + 1 != properties.Length)
                    {
                        var nextElementWrappingClass = properties[p + 1]
                            .GetCustomAttributes(typeof(WrappingClassAttribute), false)
                            .FirstOrDefault() as WrappingClassAttribute;

                        if (nextElementWrappingClass != null)
                        {
                            var nextClassNames = nextElementWrappingClass.ClassNames;
                            for (int i = 0; i < classNames?.Length; i++)
                            {
                                if (i > nextClassNames.Length - 1 || nextClassNames[i] != classNames[i])
                                {
                                    stringBuilder.Append(new string('\t', maxTab--)).AppendLine("};");
                                }
                            }
                        }
                        else
                        {
                            while (maxTab > 0)
                            {
                                stringBuilder.Append(new string('\t', maxTab--)).AppendLine("};");
                            }

                            if (hasZeroTabBracket)
                            {
                                stringBuilder.AppendLine("};");
                                hasZeroTabBracket = false;
                            }
                        }
                    }
                }

                //close all code blocks that left unclosed
                while (maxTab > 0)
                {
                    stringBuilder.Append(new string('\t', maxTab--)).AppendLine("};");
                }

                if (hasZeroTabBracket)
                {
                    stringBuilder.AppendLine("};");
                }

                return stringBuilder.ToString();
            }
            catch (NullReferenceException)
            {
                throw;
            }
        }

        /// <summary>
        /// Applies the configuration property flags, if needed.
        /// </summary>
        /// <param name="configProperty">The configuration property.</param>
        /// <param name="value">The value.</param>
        private object ApplyConfigPropertyFlags(ConfigProperty configProperty, object value)
        {
            if (configProperty.IsQuotationMarksRequired)
            {
                value = "\"" + value?.ToString() + "\"";
            }

            if (configProperty.IsLowerCaseRequired)
            {
                value = value?.ToString().ToLowerInvariant();
            }

            return value;
        }

        /// <summary>
        /// Parses the array config properties.
        /// </summary>
        /// <param name="valueAsArray">The value as array.</param>
        private string ParseArrayProperty(string[] valueAsArray)
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
        public string ParseArrayProperty(int[] valueAsArray)
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
        private Dictionary<string, string> ConvertFromTextToDictionary(string[] textProperties, Type type)
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
