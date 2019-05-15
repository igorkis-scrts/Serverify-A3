using A3ServerTool.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Represents missions that will be played on server.
    /// </summary>
    public class Mission
    {
        /// <summary>
        /// Gets or sets mission name.
        /// </summary>
        [ConfigProperty(PropertyName = "template")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets game difficulty on this mission.
        /// </summary>
        [ConfigProperty(PropertyName = "difficulty")]
        public string Difficulty { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        [ConfigProperty(IgnoreParsing =true)]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is whitelisted.
        /// </summary>
        [ConfigProperty(IgnoreParsing = true)]
        public bool IsWhitelisted { get; set; }
    }
}
