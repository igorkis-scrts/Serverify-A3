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
                SetArmaProfileDefaultValues(profile);
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
                SetBasicConfigDefaultValues(profile);
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

        public void SetDefaultValues(Profile profile)
        {
            SetBasicConfigDefaultValues(profile);
            SetServerConfigDefaultValues(profile);
            SetArmaProfileDefaultValues(profile);
            SetStartupParameterDefaultValues(profile);
        }

        /// <summary>
        /// Sets the startup parameter default values.
        /// </summary>
        /// <param name="profile">The profile.</param>
        private void SetStartupParameterDefaultValues(Profile profile)
        {
            if (profile == null) return;

            if (profile.ArgumentSettings == null)
            {
                profile.ArgumentSettings = new ArgumentSettings();
            }

            profile.ArgumentSettings.Port = "2302";
        }

        /// <summary>
        /// Sets the basic configuration default values.
        /// </summary>
        /// <param name="profile">The profile.</param>
        private void SetBasicConfigDefaultValues(Profile profile)
        {
            if (profile == null) return;

            if (profile.BasicConfig == null)
            {
                profile.BasicConfig = new BasicConfig();
            }

            profile.BasicConfig.MaxMessagesSend = 128;
            profile.BasicConfig.MaxSizeGuaranteed = 512;
            profile.BasicConfig.MaxSizeNonguaranteed = 256;
            profile.BasicConfig.MinBandwidth = 131072;
            profile.BasicConfig.MaxBandwidth = 1048576000;
            profile.BasicConfig.MinErrorToSend = 0.001F;
            profile.BasicConfig.MinErrorToSendNear = 0.01F;
            profile.BasicConfig.MaxCustomFileSize = 160;
            profile.BasicConfig.MaxPacketSize = 1400;
            profile.BasicConfig.ObjectViewDistance = 2000;
            profile.BasicConfig.TerrainGridViewDistance = 25.0F;
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

        /// <summary>
        /// Sets the basic configuration default values.
        /// </summary>
        /// <param name="profile">The profile.</param>
        private void SetArmaProfileDefaultValues(Profile profile)
        {
            if (profile == null) return;

            if (profile.ArmaProfile == null)
            {
                profile.ArmaProfile = new ArmaProfile();
            }

            profile.ArmaProfile.GroupIndicationType = 1;
            profile.ArmaProfile.FriendlyTagsVisibilityType = 1;
            profile.ArmaProfile.EnemyTagsVisibilityType = 0;
            profile.ArmaProfile.DetectedMinesVisibilityType = 1;
            profile.ArmaProfile.CommandsVisibilityType = 1;
            profile.ArmaProfile.WaypointsVisibilityType = 2;
            profile.ArmaProfile.TacticalPingType = 1;
            profile.ArmaProfile.WeaponInfoVisibilityType = 2;
            profile.ArmaProfile.StanceIndicatorVisibilityType = 2;
            profile.ArmaProfile.IsStaminaBarShown = 1;
            profile.ArmaProfile.IsCrosshairShown = 1;
            profile.ArmaProfile.IsVisionAidAllowed = 0;
            profile.ArmaProfile.IsThirdPersonViewAllowed = 1;
            profile.ArmaProfile.IsCameraShakeAllowed = 1;
            profile.ArmaProfile.IsScoreTableShown = 1;
            profile.ArmaProfile.AreDeathMessagesShown = 1;
            profile.ArmaProfile.AreVonIdsShown = 1;
            profile.ArmaProfile.IsExtendedMapEnemyContentAllowed = 0;
            profile.ArmaProfile.IsExtendedMapMinesContentAllowed = 1;
            profile.ArmaProfile.IsExtendedMapFriendlyContentAllowed = 1;
            profile.ArmaProfile.IsAutoReportEnabled = 1;
            profile.ArmaProfile.AreMultipleSavesAllowed = 1;
            profile.ArmaProfile.AiPrecision = 0.5F;
            profile.ArmaProfile.AiLevelPreset = 2;
            profile.ArmaProfile.AiSkill = 0.5F;
            profile.ArmaProfile.DefaultDifficulty = "recruit";
        }
    }
}
