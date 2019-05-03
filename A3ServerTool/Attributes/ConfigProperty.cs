using System;

namespace A3ServerTool.Attributes
{
    /// <summary>
    /// Provides attribute that helps with config files parsing.
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

        /// <summary>
        /// Gets or sets a property tied with main property.
        /// </summary>
        /// <remarks>Some properties should exist together, 
        /// so we need to put them both on config file, or not put at all (if there is no value on one property, for example).
        /// </remarks>
        public string TiedPropertyName { get; set; }
    }
}
