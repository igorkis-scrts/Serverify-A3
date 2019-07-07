namespace A3ServerTool.Models
{
    /// <summary>
    /// Represents game mod.
    /// </summary>
    public class Modification
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        public bool IsClientMod { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is whitelisted.
        /// </summary>
        public bool IsServerMod { get; set; }
    }
}
