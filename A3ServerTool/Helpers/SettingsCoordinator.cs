using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Helpers
{
    /// <summary>
    /// Provides methods to operate with application settings.
    /// </summary>
    public static class SettingsCoordinator
    {
        /// <summary>
        /// Saves application setting.
        /// </summary>
        /// <param name="type">Type of setting that will be saved.</param>
        /// <param name="value">Value of setting.</param>
        public static void Save(ApplicationSettingType type, object value)
        {
            Properties.Settings.Default[type.ToString()] = value;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Retrieve application setting value
        /// </summary>
        /// <param name="type">Type of setting that will be retrieved.</param>
        /// <returns>Setting value as object.</returns>
        public static object Retrieve(ApplicationSettingType type)
        {
            return Properties.Settings.Default[type.ToString()];
        }
    }
}
