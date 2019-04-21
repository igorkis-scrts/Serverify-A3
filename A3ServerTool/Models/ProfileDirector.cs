using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A3ServerTool.Models.Config;
using A3ServerTool.Storage;

namespace A3ServerTool.Models
{
    /// <inheritdoc/>
    public class ProfileDirector : IProfileDirector
    {
        private readonly IDao<Profile> _profileDao;
        private readonly BasicConfigDao _basicDao;

        public ProfileDirector(IDao<Profile> profileDao, BasicConfigDao basicDao)
        {
            _profileDao = profileDao;
            _basicDao = basicDao;
        }

        public void Delete(Profile profile)
        {
            _profileDao.Delete(profile);
        }

        public bool ExistOnStorage(Profile profile)
        {
            var tt = _profileDao.Get(profile);
            return _profileDao.Get(profile) != null;
        }

        public ObservableCollection<Profile> GetAll()
        {
            var profiles = _profileDao.GetAll();

            foreach(var profile in profiles)
            {
                profile.BasicConfig = _basicDao.Get(profile);
            }

            return new ObservableCollection<Profile>(profiles);
        }

        /// <inheritdoc/>
        public void SaveStorage(Profile profile)
        {
            _profileDao.Save(profile);
            _basicDao.Save(profile);

            //create same thing for basic.cfg, config.cfg etc
        }

        //public void Construct(IProfileBuilder profileBuilder)
        //{
        //    profileBuilder.BuildArguments();
        //    profileBuilder.BuildBasicConfig();
        //}
    }
}
