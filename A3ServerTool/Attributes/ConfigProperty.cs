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
        ///  Parent class while parsing.
        /// </summary>
        public string Parent { get; set; }

        /// <summary>
        /// Gets or sets a value for enabling/disabling property parsing.
        /// </summary>
        public bool IgnoreParsing { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is lower case required.
        /// </summary>
        public bool IsLowerCaseRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is quotation marks required.
        /// </summary>
        public bool IsQuotationMarksRequired { get; set; }
    }
}
