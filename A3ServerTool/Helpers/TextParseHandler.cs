using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using A3ServerTool.Attributes;

namespace A3ServerTool
{
    /// <summary>
    /// Provides instruments to convert array of text lines into object and back
    /// </summary>
    public static class TextParseHandler
    {
        /// <summary>
        /// Converts lines of properties with values from config file into object instance
        /// </summary>
        /// <returns>Instance of T class filled with properties</returns>
        public static T Parse<T>(IEnumerable<string> textProperties)
        {
            textProperties = textProperties.ToList();
            if (!textProperties.Any()) return default;

            var nameToValueDictionary = new Dictionary<string, string>();
            foreach (var property in textProperties)
            {
                var splittedProperty = property.Split('=').Where(x => x != "=").Select(x => x.Trim()).ToArray();
                if (splittedProperty.Length != 2) continue;
                splittedProperty[1] = splittedProperty[1].Replace(";", string.Empty);

                nameToValueDictionary.Add(splittedProperty[0], splittedProperty[1]);
            }

            var result = (T)Activator.CreateInstance(typeof(T));

            foreach (var property in typeof(T).GetProperties())
            {
                var attribute = property.GetCustomAttributes(true).FirstOrDefault() as ConfigProperty;
                if (attribute?.IgnoreParsing != false) continue;

                nameToValueDictionary.TryGetValue(attribute.PropertyName, out var value);

                if (property.PropertyType == typeof(int))
                {
                    property.SetValue(result, Convert.ToInt32(value));
                }
                else if (property.PropertyType == typeof(float))
                {
                    property.SetValue(result, float.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture));
                }
                else if (property.PropertyType == typeof(string))
                {
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
                var attribute = property.GetCustomAttributes(true).FirstOrDefault() as ConfigProperty;
                if (attribute == null) continue;

                var line = attribute.PropertyName + " = " + property.GetValue(instance, null) + ";";

                if (attribute.PropertyName == "maxPacketSize")
                {
                    result.AddRange(new[] { "class sockets", "{", line, "};" });
                }
                else
                {
                    result.Add(line);
                }
            }

            return result;
        }
    }
}
