using System;

namespace A3ServerTool.Attributes
{
    /// <summary>
    /// Provides additional information for config file parser
    /// - whether wrap property into classes or not.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class WrappingClassAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets name of class that will be put into config file wrapping marked properties.
        /// </summary>
        public string[] ClassNames { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrappingClassAttribute"/> class.
        /// </summary>
        /// <param name="classNames">The class names.</param>
        /// <remarks>Class names should be in the same order as they appear in the config file!</remarks>
        public WrappingClassAttribute(params string[] classNames)
        {
            ClassNames = classNames;
        }
    }
}
