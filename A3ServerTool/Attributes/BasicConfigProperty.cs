using System;

namespace A3ServerTool.Attributes
{
    /// <summary>
    /// Provides attributes that helps with basic.cfg parsing
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    class BasicConfigProperty : Attribute
    {
        /// <summary>
        /// Actual representation of property in basic.cfg
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Is property needed to parse
        /// </summary>
        public bool IgnoreParsing { get; set; }
    }
}
