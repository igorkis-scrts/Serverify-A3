using A3ServerTool.Models.Config;
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
           if (profile == null) return null;

           profile.BasicConfig = _basicDao.Get(profile);
           if(profile.BasicConfig == null)
           {
                profile.BasicConfig = new BasicConfig();
                _basicDao.Save(profile);
           }

           profile.ServerConfig = _serverDao.Get(profile);
           if (profile.ServerConfig == null)
           {
               profile.ServerConfig = new ServerConfig();
                _serverDao.Save(profile);
           }

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

        public void SetDefaultValues(Profile profile)
        {
            SetBasicConfigDefaultValues(profile);
            SetServerConfigDefaultValues(profile);
            //TODO: Default values for CommandLineArguments
        }

        /// <summary>
        /// Sets the basic configuration default values.
        /// </summary>
        /// <param name="profile">The profile.</param>
        private void SetBasicConfigDefaultValues(Profile profile)
        {
            if (profile == null) return;

            if(profile.BasicConfig == null)
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
        }
    }
}
