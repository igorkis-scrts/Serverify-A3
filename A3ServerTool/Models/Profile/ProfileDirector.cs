using A3ServerTool.Helpers;
using A3ServerTool.Models.Config;
using A3ServerTool.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A3ServerTool.Models
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
            RetrieveModifications(profile);

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
        private void RetrieveModifications(Profile profile)
        {
            if (profile.ArgumentSettings == null)
            {
                profile.ArgumentSettings = new ArgumentSettings();
            }

            //merge stored mods in arma folder and mods located in config
            var storedMods = _modDao.GetAll(_locationFinder.GetGameInstallationPath(profile))
                .ToList();
            var configMods = profile.ArgumentSettings.Modifications;

            Parallel.ForEach(storedMods, mod =>
            {
                var configMod = configMods.FirstOrDefault(x => x.Name == mod.Name);
                if(configMod?.IsClientMod == true)
                {
                    mod.IsClientMod = true;
                }

                if (configMod?.IsServerMod == true)
                {
                    mod.IsServerMod = true;
                }
            });
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
                SetServerConfigDefaultValues(profile);
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

        [Obsolete("Will be deleted in the next update.")]
        public void SetDefaultValues(Profile profile)
        {
            SetServerConfigDefaultValues(profile);
        }

        /// <summary>
        /// Sets the server configuration default values.
        /// </summary>
        /// <param name="profile">The profile.</param>
        private void SetServerConfigDefaultValues(Profile profile)
        {
            if (profile == null) return;

            if (profile.ServerConfig == null)
            {
                profile.ServerConfig = new ServerConfig();
            }

            profile.ServerConfig.MaximumAmountOfPlayers = 64;
            profile.ServerConfig.FilePatchingMode = 0;
            profile.ServerConfig.DisconnectTimeout = 90;
            profile.ServerConfig.MaximumPing = 200;
            profile.ServerConfig.MaximumDesync = 150;
            profile.ServerConfig.MaximumPacketLoss = 50;
            profile.ServerConfig.HasBattleEye = true;
            profile.ServerConfig.TimeStampFormat = "none";
            profile.ServerConfig.RotorLibSimulationType = 0;
            profile.ServerConfig.LogFileName = "server_console.log";
            profile.ServerConfig.VoiceCodecType = 0;
            profile.ServerConfig.VoiceCodecQuality = 3;
            profile.ServerConfig.IsDrawingOnMapAllowed = true;
            profile.ServerConfig.SignatureVerificationMode = 2;
            profile.ServerConfig.LobbyIdleTimeout = 99999;
            profile.ServerConfig.DebriefingTimeout = 45;
            profile.ServerConfig.BriefingTimeout = 60;
            profile.ServerConfig.RoleSelectionTimeout = 99999;
            profile.ServerConfig.HasBattleEye = true;
            profile.ServerConfig.SignatureVerificationMode = 2;
            profile.ServerConfig.FilePatchingMode = 0;
            profile.ServerConfig.LoadFileExtensionsWhitelist.AddRange(new[] { "hpp", "sqs", "sqf", "fsm", "cpp", "paa", "txt", "xml", "inc", "ext", "sqm", "ods", "fxy", "lip", "csv", "kb", "bik", "bikb", "html", "htm", "biedi" });
            profile.ServerConfig.PreprocessFileExtensionsWhitelist.AddRange(new[] { "hpp", "sqs", "sqf", "fsm", "cpp", "paa", "txt", "xml", "inc", "ext", "sqm", "ods", "fxy", "lip", "csv", "kb", "bik", "bikb", "html", "htm", "biedi" });
            profile.ServerConfig.HtmlFileExtensionsWhitelist.AddRange(new[] { "htm", "html", "xml", "txt" });
        }
    }
}
