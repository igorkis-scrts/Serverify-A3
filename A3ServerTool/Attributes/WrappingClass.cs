using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Attributes
{
    /// <summary>
    /// Provides additional information for config file parser
    /// - whether wrap property into class or not.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal sealed class WrappingClass : Attribute
    {
        /// <summary>
        /// Gets or sets name of class that will be put into config file wrapping marked properties.
        /// </summary>
        public string ClassName { get; set; }
    }
}
