using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interchangeable
{
    /// <summary>
    /// Represents helpers to work with Uri's. 
    /// </summary>
    public static class UriDirector
    {
        /// <summary>
        /// Opens the URI.
        /// </summary>
        /// <param name="absoluteUri">The absolute URI.</param>
        public static void OpenUri(string absoluteUri)
        {
            Process.Start(new ProcessStartInfo(absoluteUri));
        }
    }
}
