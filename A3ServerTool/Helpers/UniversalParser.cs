using A3ServerTool.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Helpers
{
    /// <summary>
    /// Provides instruments to convert array of text lines into object and back.
    /// </summary>
    public static class UniversalParser
    {
        /// <summary>
        /// Converts object properties with values to config file content string.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Config file in text representation.</returns>
        public static string ConvertToText<T>(T instance)
        {
            WrappingClassAttribute previousWrappingClass = null;
            int maxTab = 0;

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
                        }
                    }
                }

                previousWrappingClass = wrappingClass;
                var value = properties[p].GetValue(instance, null);

                if (configProperty.IsIntValueRequired)
                {
                    value = Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()));
                }

                if (properties[p].PropertyType == typeof(string))
                {
                    value = "\"" + value.ToString().ToLower() + "\"";
                }
                else if (properties[p].PropertyType == typeof(int?))
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

                //writing string with property and value
                stringBuilder.Append(new string('\t', maxTab + 1))
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
                        while (maxTab >= 0)
                        {
                            stringBuilder.Append(new string('\t', maxTab--)).AppendLine("};");
                        }
                    }
                }
            }

            //close all code blocks that left unclosed
            while (maxTab >= 0)
            {
                stringBuilder.Append(new string('\t', maxTab--)).AppendLine("};");
            }

            return stringBuilder.ToString();
        }
    }
}
