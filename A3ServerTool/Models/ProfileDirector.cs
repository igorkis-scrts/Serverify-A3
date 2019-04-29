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

        /// <inheritdoc/>
        public void Delete(Profile profile)
        {
            _profileDao.Delete(profile);
        }

        /// <inheritdoc/>
        public Profile GetById(Guid id)
        {
            return _profileDao.Get(new Profile(id));
        }

        /// <inheritdoc/>
        public bool ExistOnStorage(Profile profile)
        {
            return _profileDao.Get(profile) != null;
        }

        /// <inheritdoc/>
        public IList<Profile> GetAll()
        {
            var profiles = _profileDao.GetAll();

            foreach(var profile in profiles)
            {
                profile.BasicConfig = _basicDao.Get(profile);
            }

            return profiles;
        }

        /// <inheritdoc/>
        public void SaveStorage(Profile profile)
        {
            _profileDao.Save(profile);
            _basicDao.Save(profile);

            //create same thing for basic.cfg, config.cfg etc
        }
    }
}
