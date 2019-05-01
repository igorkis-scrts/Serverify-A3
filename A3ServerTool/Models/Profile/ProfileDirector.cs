using A3ServerTool.Storage;
using System;
using System.Collections.Generic;

namespace A3ServerTool.Models
{
    /// <inheritdoc/>
    public class ProfileDirector : IProfileDirector
    {
        private readonly IDao<Profile> _profileDao;
        private readonly BasicConfigDao _basicDao;
        private readonly ServerConfigDao _serverDao;

        public ProfileDirector(IDao<Profile> profileDao, BasicConfigDao basicDao, ServerConfigDao serverDao)
        {
            _profileDao = profileDao;
            _basicDao = basicDao;
            _serverDao = serverDao;
        }

        /// <inheritdoc/>
        public void Delete(Profile profile)
        {
            _profileDao.Delete(profile);
        }

        /// <inheritdoc/>
        public Profile GetById(Guid id)
        {
           var profile = _profileDao.Get(new Profile(id));
           profile.BasicConfig = _basicDao.Get(profile);
           profile.ServerConfig = _serverDao.Get(profile);
           return profile;
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

            foreach (var profile in profiles)
            {
                profile.BasicConfig = _basicDao.Get(profile);
                profile.ServerConfig = _serverDao.Get(profile);
            }

            return profiles;
        }

        /// <inheritdoc/>
        public void SaveStorage(Profile profile)
        {
            _profileDao.Save(profile);
            _basicDao.Save(profile);
            _serverDao.Save(profile);
        }
    }
}
