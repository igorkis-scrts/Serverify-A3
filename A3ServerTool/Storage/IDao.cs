using System.Collections.Generic;

namespace A3ServerTool.Storage
{
    /// <summary>
    /// Provides Data Access Object for profiles,configs
    /// </summary>
    public interface IDao<T> where T : class
    {
        /// <summary>
        /// Gets all stored T items.
        /// </summary>
        /// <returns>IList with T.</returns>
        IList<T> GetAll();

        /// <summary>
        /// Gets all stored objects from storage.
        /// </summary>
        IList<T> GetAll(string location);

        /// <summary>
        /// Gets the certain T item.
        /// </summary>
        /// <param name="item">The item.</param>
        T Get(T item);

        /// <summary>
        /// Saves the certain T item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Save(T item);

        /// <summary>
        /// Deletes the certain T item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Delete(T item);
    }
}
