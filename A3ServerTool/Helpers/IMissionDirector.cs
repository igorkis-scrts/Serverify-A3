namespace A3ServerTool.Helpers;

/// <summary>
/// Represents methods that works with missions stored in server.cfg.
/// </summary>
public interface IMissionDirector
{
    /// <summary>
    /// Gets the missions.
    /// </summary>
    /// <returns>List of missions that will be played on server.</returns>
    /// <param name="properties">Text properties (server.cfg).</param>
    /// <param name="folderPath">Path to Arma 3 installation.</param>
    IList<Mission> GetMissions(IEnumerable<string> properties, string folderPath);

    /// <summary>
    /// Saves the missions into server.cfg.
    /// </summary>
    /// <param name="missions">Missions specified somewhere in the app.</param>
    /// <param name="fileContent">Server config file represented in string.</param>
    /// <returns>fileContent with missions stored as text in the end of the file.</returns>
    string SaveMissions(IEnumerable<Mission> missions, string fileContent);
}
