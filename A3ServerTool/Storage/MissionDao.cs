using A3ServerTool.Models;
using Interchangeable.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Storage
{
    /// <summary>
    /// Provides access to missions stored on server.
    /// </summary>
    /// <seealso cref="A3ServerTool.Storage.IDao{A3ServerTool.Models.Mission}" />
    public class MissionDao : IDao<Mission>
    {
        private const string MissionFolderName = "MPMissions";
        private const string MissionFileExtension = ".pbo";

        /// <summary>
        /// Gets all missions.
        /// </summary>
        /// <param name="gamePath">The game path.</param>
        /// <returns>IList of missions.</returns>
        public IList<Mission> GetAll(string gamePath)
        {
            var missions = new List<Mission>();

            var missionFolderContent = FileHelper.GetFolder(gamePath)?
                .FirstOrDefault(f => string.Equals(f.Name, MissionFolderName, StringComparison.OrdinalIgnoreCase));
            if (missionFolderContent == null) return missions;

            var files = FileHelper.GetAllFiles(missionFolderContent)
                .Where(f => f.Extension == MissionFileExtension);
            if(files == null)
            {
                return missions;
            }

            foreach(var file in files)
            {
                var mission = new Mission
                {
                    Name = file.Name.Replace(MissionFileExtension, string.Empty)
                };
                missions.Add(mission);
            }

            return missions;
        }

        /// <summary>
        /// NOT IMPLEMENTED!
        /// </summary>
        public void Delete(Mission item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NOT IMPLEMENTED!
        /// </summary>
        public Mission Get(Mission item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NOT IMPLEMENTED!
        /// </summary>
        public IList<Mission> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// NOT IMPLEMENTED!
        /// </summary>
        public void Save(Mission item)
        {
            throw new NotImplementedException();
        }
    }
}
