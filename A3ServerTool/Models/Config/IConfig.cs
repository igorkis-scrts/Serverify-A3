namespace A3ServerTool.Models.Config
{
    /// <summary>
    /// Provides common interface for config files.
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Gets config file name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets config file extension.
        /// </summary>
        string FileExtension { get; }

        /// <summary>
        /// Gets or sets config file location.
        /// </summary>
        string FileLocation { get; set; }
    }
}
