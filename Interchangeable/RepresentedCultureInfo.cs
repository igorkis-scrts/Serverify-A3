using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interchangeable
{
    /// <summary>
    /// Ties language enum with according CultureInfo.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    //[AttributeUsage(AttributeTargets.ReturnValue)]
    public class RepresentedCultureInfo : Attribute
    {
        /// <summary>
        /// Gets or sets the culture of language.
        /// </summary>
        public string Culture { get; set; }
    }
}
