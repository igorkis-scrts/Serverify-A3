namespace A3ServerTool.Helpers;

/// <summary>
/// Provides instruments to convert array of text lines into object and back.
/// </summary>
public interface IUniversalParser
{
    /// <summary>
    /// Converts lines of properties with values from config file into object instance.
    /// </summary>
    /// <returns>Instance of T class filled with properties.</returns>
    T Parse<T>(IEnumerable<string> configProperties);

    /// <summary>
    /// Converts object properties with values to config file content string.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <returns>Config file in text representation.</returns>
    string ConvertToText<T>(T instance);
}
