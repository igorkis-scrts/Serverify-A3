using A3ServerTool.Attributes;
using A3ServerTool.Models;
using System;
using System.Linq;
using System.Text;

namespace A3ServerTool.Helpers.ServerLauncher
{
    /// <summary>
    /// Provides helpers to work with executable file arguments.
    /// </summary>
    /// <seealso cref="A3ServerTool.Helpers.ServerLauncher.IServerStringBuilder" />
    public class ServerStringBuilder : IServerStringBuilder
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        /// <summary>
        /// Parses the argument settings.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>
        /// Final string with all arguments that will be applied to the server executable file.
        /// </returns>
        public string GetFinalArgumentString(Profile profile)
        {
            if (profile == null || profile.ArgumentSettings == null)
            {
                return string.Empty;
            }

            //TODO: DRY violation?
            ParseArgumentProperties(profile.ArgumentSettings);
            ParseProfileProperties(profile);

            return _stringBuilder.ToString();
        }

        /// <summary>
        /// Parses the argument properties.
        /// </summary>
        private void ParseArgumentProperties(ArgumentSettings argumentSettings)
        {
            var properties = argumentSettings.GetType()
                .GetProperties()
                .Select(x => new
                {
                    Property = x,
                    Attribute = (ServerArgument)Attribute.GetCustomAttribute(x, typeof(ServerArgument))
                })
                .Where(x => x.Attribute != null)
                .Select(x => x.Property)
                .ToArray();
            if(properties?.Any() != true)
            {
                return;
            }

            for (int i = 0; i < properties.Length; i++)
            {
                var argumentName = properties[i].GetCustomAttributes(typeof(ServerArgument), false)
                    .FirstOrDefault() as ServerArgument;
                if (argumentName == null)
                {
                    continue;
                }

                var value = properties[i].GetValue(argumentSettings, null);
                if(!string.IsNullOrWhiteSpace(value?.ToString()))
                {
                    AppendArgument(argumentName, value);
                }
            }
        }

        /// <summary>
        /// Parses the profile properties.
        /// </summary>
        /// <param name="profile">The profile.</param>
        private void ParseProfileProperties(Profile profile)
        {
            var properties = profile.GetType()
                .GetProperties()
                .Select(x => new
                {
                    Property = x,
                    Attribute = (ServerArgument)Attribute.GetCustomAttribute(x, typeof(ServerArgument))
                })
                .Where(x => x.Attribute != null)
                .Select(x => x.Property)
                .ToArray();
            if (properties?.Any() != true)
            {
                return;
            }

            for (int i = 0; i < properties.Length; i++)
            {
                var argumentName = properties[i].GetCustomAttributes(typeof(ServerArgument), false)
                    .FirstOrDefault() as ServerArgument;
                if (argumentName == null)
                {
                    continue;
                }

                var value = properties[i].GetValue(profile, null);
                if (!string.IsNullOrWhiteSpace(value?.ToString()))
                {
                    AppendArgument(argumentName, value);
                }
            }
        }

        /// <summary>
        /// Appends the argument.
        /// </summary>
        /// <param name="argumentName">Name of the argument.</param>
        /// <param name="value">The value of the argument.</param>
        private void AppendArgument(ServerArgument argumentName, object value)
        {
            if (value == null)
            {
                return;
            }

            if (argumentName.IsQuotationMarksRequired)
            {
                _stringBuilder.Append('\"').Append(argumentName.Argument)
                    .Append("=").Append(value).Append('\"').Append(" ");
            }
            else
            {
                _stringBuilder.Append(argumentName.Argument).Append("=").Append(value).Append(" ");
            }
        }
    }
}
