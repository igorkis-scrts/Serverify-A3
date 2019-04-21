using System.Collections.Generic;
using System.Collections.ObjectModel;
using A3ServerTool.Models;

namespace A3ServerTool.Storage
{
    /// <summary>
    /// Provides Data Access Object for profiles,configs
    /// </summary>
    public interface IDao<T> where T: class
    {
        IList<T> GetAll();
        T Get(T item);
        void Save(T item);
        void Delete(T item);
    }
}
