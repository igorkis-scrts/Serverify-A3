using A3ServerTool.Attributes;
using A3ServerTool.Enums;
using System;
using System.Collections.Generic;

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

        ///// <summary>
        ///// Gets or sets game difficulty on this mission.
        ///// </summary>
        [ConfigProperty(PropertyName = "difficulty")]
        public DifficultyType Difficulty { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        [ConfigProperty(IgnoreParsing = true)]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is whitelisted.
        /// </summary>
        [ConfigProperty(IgnoreParsing = true)]
        public bool IsWhitelisted { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Mission mission &&
                   Name == mission.Name &&
                   Difficulty == mission.Difficulty &&
                   IsSelected == mission.IsSelected &&
                   IsWhitelisted == mission.IsWhitelisted;
        }

        public override int GetHashCode()
        {
            var hashCode = 456417597;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Difficulty.GetHashCode();
            hashCode = hashCode * -1521134295 + IsSelected.GetHashCode();
            hashCode = hashCode * -1521134295 + IsWhitelisted.GetHashCode();
            return hashCode;
        }
    }
}
