namespace A3ServerTool.Models.Config;

/// <summary>
/// Provides common interface for config files.
/// </summary>
public interface IConfig
{
    /// <summary>
    /// Gets or sets config file location.
    /// </summary>
    string FileLocation { get; set; }
}
