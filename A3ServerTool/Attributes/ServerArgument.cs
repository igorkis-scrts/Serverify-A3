using System;

namespace A3ServerTool.Attributes
{
    /// <summary>
    /// Provides command line argument representation for various parameters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class ServerArgument : Attribute
    {
        /// <summary>
        /// Gets or sets the command line argument.
        /// </summary>
        public string Argument { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is quotation marks required.
        /// </summary>
        public bool IsQuotationMarksRequired { get; set; }
    }
}
