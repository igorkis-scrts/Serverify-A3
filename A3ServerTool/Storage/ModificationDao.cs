using A3ServerTool.Models;
using Interchangeable.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Storage
{
    /// <summary>
    /// Provides access to game modifications.
    /// </summary>
    /// <seealso cref="A3ServerTool.Storage.IDao{A3ServerTool.Models.Modification}" />
    public class ModificationDao : IDao<Modification>
    {
        /// <summary>
        /// Gets all stored objects from storage.
        /// </summary>
        /// <param name="location"></param>
        /// <exception cref="NotImplementedException"></exception>
        public IList<Modification> GetAll(string location)
        {
            var mods = new List<Modification>();

            var modFolders = FileHelper.GetFolder(location)?
                .Where(f => f.Name.StartsWith("@")).ToList();
            if (modFolders?.Any() != true)
            {
                return mods;
            }

            foreach(var folder in modFolders)
            {
                var mod = new Modification
                {
                    Name = folder.Name
                };
                mods.Add(mod);
            }

            return mods;
        }

        /// <summary>
        /// NOT IMPLEMENTED!
        /// </summary>
        public void Delete(Modification item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NOT IMPLEMENTED!
        /// </summary>
        public Modification Get(Modification item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NOT IMPLEMENTED!
        /// </summary>
        public IList<Modification> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NOT IMPLEMENTED!
        /// </summary>
        public void Save(Modification item)
        {
            throw new NotImplementedException();
        }
    }
}
