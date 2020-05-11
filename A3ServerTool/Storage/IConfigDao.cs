using A3ServerTool.Models.Config;
using A3ServerTool.Models.Profile;

namespace A3ServerTool.Storage
{
    /// <summary>
    /// Provides access and manipulation methods for various Profile subclasses stored in DB/file system/etc.
    /// </summary>
    /// <typeparam name="T">Any type that being considered as configuration file.</typeparam>
    public interface IConfigDao<T> where T : IConfig
    {
        /// <summary>
        /// Gets instance of type from storage.
        /// </summary>
        /// <param name="profile">Profile from which to get an instance of the class.</param>
        /// <returns>Instance of type retrived from storage.</returns>
        T Get(Profile profile);

        /// <summary>
        /// Saves instance of type in according profile storage.
        /// </summary>
        /// <param name="profile">Profile where to store class instance.</param>
        void Save(Profile profile);
    }
}
