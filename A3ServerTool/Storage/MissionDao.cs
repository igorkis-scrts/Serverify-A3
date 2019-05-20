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
    public class MissionDao : IDao<Mission>
    {
        private const string MissionFolderName = "MPMissions";
        private const string MissionExtension = ".pbo";

        public IList<Mission> GetAll(string gamePath)
        {
            var missions = new List<Mission>();

            var tt = Path.Combine(gamePath, MissionFolderName);

            var missionFolderContent = FileHelper.GetFolder(gamePath).FirstOrDefault(f => f.Name == MissionFolderName);
            if (missionFolderContent == null) return missions;

            var files = FileHelper.GetAllFiles(missionFolderContent).Where(f => f.Extension == MissionExtension);

            Parallel.ForEach(files, file =>
            {
                var mission = new Mission();
                mission.Name = file.Name.Replace(MissionExtension, string.Empty);
                missions.Add(mission);
            });

            return missions;
        }

        public void Delete(Mission item)
        {
            throw new NotImplementedException();
        }

        public Mission Get(Mission item)
        {
            throw new NotImplementedException();
        }

        public IList<Mission> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(Mission item)
        {
            throw new NotImplementedException();
        }
    }
}
