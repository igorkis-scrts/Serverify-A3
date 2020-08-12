using System;
using System.Collections.Generic;
using System.Linq;
using A3ServerTool.Helpers;
using A3ServerTool.Models.Config;
using A3ServerTool.Storage;

namespace A3ServerTool.Models.Profile
{
    /// <inheritdoc/>
    public class ProfileDirector : IProfileDirector
    {
        private readonly IDao<Profile> _profileDao;
        private readonly IConfigDao<BasicConfig> _basicDao;
        private readonly IConfigDao<ServerConfig> _serverDao;
        private readonly IConfigDao<ArmaProfile> _armaProfileDao;
        private readonly IDao<Modification> _modDao;

        private readonly GameLocationFinder _locationFinder;

        public ProfileDirector(IDao<Profile> profileDao, IConfigDao<BasicConfig> basicDao,
            IConfigDao<ServerConfig> serverDao, IConfigDao<ArmaProfile> armaProfileDao, IDao<Modification> modDao, GameLocationFinder locationFinder)
        {
            _profileDao = profileDao;
            _basicDao = basicDao;
            _serverDao = serverDao;
            _armaProfileDao = armaProfileDao;
            _modDao = modDao;

            _locationFinder = locationFinder;
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
            if (profile == null) return null;

            RetrieveBasicConfig(profile);
            RetrieveServerConfig(profile);
            RetrieveArmaProfile(profile);
            GetProfileModifications(profile);

            return profile;
        }

        /// <summary>
        /// Retrieves the arma profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        private void RetrieveArmaProfile(Profile profile)
        {
            profile.ArmaProfile = _armaProfileDao.Get(profile);
            if (profile.ArmaProfile == null)
            {
                profile.ArmaProfile = new ArmaProfile();
                _armaProfileDao.Save(profile);
            }
        }

        /// <summary>
        /// Retrieves the arma profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        public void GetProfileModifications(Profile profile)
        {
            profile.ArgumentSettings ??= new ArgumentSettings();

            //merge stored mods in arma folder and mods located in config
            var storedMods = _modDao.GetAll(_locationFinder.GetGameInstallationPath(profile))
                .ToList();
            var configMods = profile.ArgumentSettings.Modifications;
            if (!configMods.Any())
            {
                profile.ArgumentSettings.Modifications = storedMods;
                return;
            }

            foreach(var mod in storedMods)
            {
                var configMod = configMods.FirstOrDefault(x => x.Name == mod.Name);
                if (configMod?.IsClientMod == true)
                {
                    mod.IsClientMod = true;
                }

                if (configMod?.IsServerMod == true)
                {
                    mod.IsServerMod = true;
                }
            }
        }

        /// <summary>
        /// Retrieves the server configuration.
        /// </summary>
        /// <param name="profile">The profile.</param>
        private void RetrieveServerConfig(Profile profile)
        {
            profile.ServerConfig = _serverDao.Get(profile);
            if (profile.ServerConfig == null)
            {
                profile.ServerConfig = new ServerConfig();
                _serverDao.Save(profile);
            }
        }

        /// <summary>
        /// Retrieves the basic configuration.
        /// </summary>
        /// <param name="profile">The profile.</param>
        private void RetrieveBasicConfig(Profile profile)
        {
            profile.BasicConfig = _basicDao.Get(profile);
            if (profile.BasicConfig == null)
            {
                profile.BasicConfig = new BasicConfig();
                _basicDao.Save(profile);
            }
        }

        /// <inheritdoc/>
        public bool ExistOnStorage(Profile profile)
        {
            return _profileDao.Get(profile) != null;
        }

        /// <inheritdoc/>
        public IList<Profile> GetAll()
        {
            return _profileDao.GetAll();
        }

        /// <inheritdoc/>
        public void SaveStorage(Profile profile)
        {
            _profileDao.Save(profile);
            _basicDao.Save(profile);
            _serverDao.Save(profile);
            _armaProfileDao.Save(profile);
        }
    }
}
