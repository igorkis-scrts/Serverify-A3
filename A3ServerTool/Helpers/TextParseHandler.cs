using A3ServerTool.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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

            Dictionary<string, string> nameToValueDictionary = ConvertFromTextToDictionary(textProperties);

            var result = (T)Activator.CreateInstance(typeof(T));

            foreach (var property in typeof(T).GetProperties())
            {
                var stringName = property.GetCustomAttributes(typeof(ConfigProperty), false).FirstOrDefault() as ConfigProperty;

                if (stringName?.IgnoreParsing != false) continue;

                nameToValueDictionary.TryGetValue(stringName.PropertyName, out var value);

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
                else if (property.PropertyType == typeof(List<string>))
                {
                    if (value != null)
                    {
                        value = Regex.Replace(value, @"\s+", "");
                    }

                    property.SetValue(result, value?.Split(',').ToList());
                }
                else if (property.PropertyType == typeof(string))
                {
                    value = value?.Replace("\"", string.Empty);
                    property.SetValue(result, Convert.ToString(value));
                }

            }

            return result;
        }

        public static List<string> ConvertToText<T>(T instance)
        {
            var result = new List<string>();

            foreach (var property in instance.GetType().GetProperties())
            {
                var stringName = property.GetCustomAttributes(true).FirstOrDefault() as ConfigProperty;
                var wrappingClasses = property.GetCustomAttributes(typeof(WrappingClass), false).Cast<WrappingClass>().ToArray();

                if (stringName == null) continue;

                var value = property.GetValue(instance, null);

                if (property.PropertyType == typeof(string))
                {
                    value = "\"" + value + "\"";
                }
                else if (property.PropertyType == typeof(string[]))
                {
                    if (value is string[] valueAsArray)
                    {
                        value = ParseArrayProperty((string[])value);
                    }

                    if (string.IsNullOrWhiteSpace(value?.ToString())) continue;
                }
                else if(property.PropertyType == typeof(List<string>))
                {
                    if (value is List<string> valueAsList)
                    {
                        value = ParseArrayProperty(valueAsList.ToArray());
                    }

                    if (string.IsNullOrWhiteSpace(value?.ToString())) continue;
                }
                else if(property.PropertyType == typeof(bool))
                {
                    value = value.ToString().ToLowerInvariant();
                }
                else if(property.PropertyType == typeof(int?))
                {
                    if(!int.TryParse(value?.ToString(), out int nullInt))
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

                var line = stringName.PropertyName + " = " + value + ";";

                if (wrappingClasses.Any())
                {
                    result.AddRange(WrapParameter(wrappingClasses, line));
                }
                else
                {
                    result.Add(line);
                }
            }

            return result;
        }

        /// <summary>
        /// Generates text content for wrapped properties in config file.
        /// </summary>
        /// <param name="classes">WrappingClass attributes.</param>
        /// <param name="parameterLine">ParameterLine to insert.</param>
        /// <returns>List with nested properties.</returns>
        private static List<string> WrapParameter(IList<WrappingClass> classes, string parameterLine)
        {
            //I wish someday we will get nested properties
            if (classes?.Any() != true) return new List<string>();

            var result = new List<string>();
            int maxTab = 0;

            for(int i = 0; i < classes.Count(); i++)
            {
                if(i != 0)
                {
                    var tab = new string('\t', i);
                    maxTab = i;
                    result.AddRange(new[] { tab + "class " + classes[i].ClassName, tab + "{" });
                }
                else
                {
                    result.AddRange(new[] { "class " + classes[i].ClassName, "{" });
                }
            }

            result.Add(new string('\t', maxTab + 1) + parameterLine);

            for (int i = 0; i < classes.Count(); i++)
            {
                result.Add(new string('\t', maxTab - i) + "};");
            }

            return result;
        }

        private static string ParseArrayProperty(string[] valueAsArray)
        {
            if (valueAsArray == null || (valueAsArray.Length == 1 && string.IsNullOrEmpty(valueAsArray[0]))) return string.Empty;

            //TODO: new attribute to check if property has empty lines 
            for (int i = 0; i < valueAsArray.Length; i++)
            {
                valueAsArray[i] = Regex.Replace(valueAsArray[i], @"[^\S ]+", "");
                valueAsArray[i] = valueAsArray[i].Replace("\"", string.Empty);

                if (string.IsNullOrWhiteSpace(valueAsArray[i]))
                {
                    valueAsArray[i] = Regex.Replace(valueAsArray[i], @"\s+", "");
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
        /// Converts from plain text to properties dictionary.
        /// </summary>
        /// <param name="textProperties">Text properties.</param>
        /// <returns>"Property-value dictionary."</returns>
        private static Dictionary<string, string> ConvertFromTextToDictionary(string[] textProperties)
        {
            var nameToValueDictionary = new Dictionary<string, string>();

            for (int i = 0; i < textProperties.Length; i++)
            {
                var splittedProperty = textProperties[i].Split('=').Where(x => x != "=").Select(x => x.Trim()).ToArray();
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
