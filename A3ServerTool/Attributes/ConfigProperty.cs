using System;

namespace A3ServerTool.Attributes
{
    /// <summary>
    /// Provides attributes that helps with config files parsing.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class ConfigProperty : Attribute
    {
        /// <summary>
        ///  Gets or sets actual representation of property in config file.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets a value for enabling/disabling property parsing.
        /// </summary>
        public bool IgnoreParsing { get; set; }
    }
}
