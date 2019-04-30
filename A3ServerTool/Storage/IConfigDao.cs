using A3ServerTool.Models;
using A3ServerTool.Models.Config;

namespace A3ServerTool.Storage
{
    /// <summary>
    /// Provides access and manipulation methods for various Profile subclasses stored in DB/file system/etc.
    /// </summary>
    /// <typeparam name="T">Any type that being considered as configuration file.</typeparam>
    public interface IConfigDao<T> where T : IConfig
    {
        //TODO: Fix DRY violation - ConfigDao implementations are same, 
        //TODO: but relies on external information (location, filename, extension)
        //TODO: basically needs to find generic - IConfig way to retrieve and save configs

        /// <summary>
        /// Gets instance of type from storage.
        /// </summary>
        /// <param name="profile">Profile from which to get an instance of the class.</param>
        /// <returns>Instance of type retrived from storage.</returns>
        T Get(IProfile profile);

        /// <summary>
        /// Saves instance of type in according profile storage.
        /// </summary>
        /// <param name="profile">Profile where to store class instance.</param>
        void Save(IProfile profile);
    }
}
