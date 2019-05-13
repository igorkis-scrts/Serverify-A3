using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A3ServerTool.Annotations;
using A3ServerTool.Attributes;

namespace A3ServerTool.Models.Config
{
    /// <summary>
    /// Provides a representation of "basic.cfg" file. 
    /// For better expanation please visit <see href="https://community.bistudio.com/wiki/server.cfg">BIS Wiki</see>
    /// </summary>
    public class ServerConfig : IConfig
    {
        /// <inheritdoc/>
        public string FileName => "server";

        /// <inheritdoc/>
        public string FileExtension => ".cfg";

        /// <inheritdoc/>
        public string FileLocation { get; set; }

        /// <summary>
        /// Gets or sets server name visible in the game browser.
        /// </summary>
        [ConfigProperty(PropertyName = "hostname")]
        public string HostName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets password to protect admin access. 
        /// </summary>
        /// <remarks>Password stored in plain text because it is stored the same way in config file.</remarks>
        [ConfigProperty(PropertyName = "passwordAdmin")]
        public string AdminPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets password required to connect to server. 
        /// </summary>
        /// <remarks>Password stored in plain text because it is stored the same way in config file.</remarks>
        [ConfigProperty(PropertyName = "password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets password required by alternate syntax of serverCommand server-side scripting. 
        /// </summary>
        /// <remarks>Password stored in plain text because it is stored the same way in config file.</remarks>
        [ConfigProperty(PropertyName = "serverCommandPassword")]
        public string ServerCommandPassword { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the maximum number of players that can connect to server. 
        /// </summary>
        [ConfigProperty(PropertyName = "maxPlayers")]
        public int? MaximumAmountOfPlayers { get; set; }

        /// <summary>
        /// Gets or sets welcome messages.
        /// </summary>
        [ConfigProperty(PropertyName = "motd[]")]
        public string[] WelcomeMessages { get; set; } = new string[2];

        /// <summary>
        /// Gets or sets the interval in seconds between welcome messages. 
        /// </summary>
        [ConfigProperty(PropertyName = "motdInterval")]
        public int? IntervalBetweenWelcomeMessages { get; set; }

        /// <summary>
        /// Gets or sets whitelisted Uids for admin access to server.
        /// </summary>
        [ConfigProperty(PropertyName = "admins[]")]
        public List<string> AdminUids { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets whitelisted IPs for headless clients.
        /// </summary>
        [ConfigProperty(PropertyName = "headlessClients[]")]
        public List<string> HeadlessClientIps { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets whitelisted IPs for clients with unlimited bandwidth and nearly no latency.
        /// </summary>
        [ConfigProperty(PropertyName = "localClient[]")]
        public List<string> LocalClientIps { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets percentage of votes needed to confirm a vote. 
        /// </summary>
        [ConfigProperty(PropertyName = "voteThreshold")]
        public float? VoteThreshold { get; set; }

        /// <summary>
        /// Gets or sets a threshold value to start mission-voting on server. 
        /// </summary>
        [ConfigProperty(PropertyName = "voteMissionPlayers")]
        public int? VoteMissionPlayers { get; set; }

        /// <summary>
        /// Gets or sets a allowance of duplcate game Ids. 
        /// </summary>
        [ConfigProperty(PropertyName = "kickduplicate")]
        public int? KickDuplicateIds { get; set; }

        /// <summary>
        /// Gets or sets a value to force server into LAN mode. 
        /// </summary>
        [ConfigProperty(PropertyName = "loopback")]
        public bool IsLan { get; set; }

        /// <summary>
        /// Gets or sets a value to force server into UPNP mode. 
        /// </summary>
        [ConfigProperty(PropertyName = "upnp")]
        public bool IsUpnp { get; set; }

        /// <summary>
        /// Gets or sets a value to allow file patching for clients. 
        /// </summary>
        [ConfigProperty(PropertyName = "allowedFilePatching")]
        public int FilePatchingMode { get; set; }

        /// <summary>
        /// Gets or sets whitelisted Steam IDs allowed file patching.
        /// </summary>
        [ConfigProperty(PropertyName = "filePatchingExceptions[]")]
        public List<string> FilePatchingExceptions { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets a Server wait time before disconnecting client. 
        /// </summary>
        [ConfigProperty(PropertyName = "disconnectTimeout")]
        public int? DisconnectTimeout { get; set; }

        /// <summary>
        /// Gets or sets a max desync value until server kicks the user. 
        /// </summary>
        [ConfigProperty(PropertyName = "maxdesync")]
        public int? MaximumDesync { get; set; }

        /// <summary>
        /// Gets or sets a max ping value until server kick the user. 
        /// </summary>
        [ConfigProperty(PropertyName = "maxping")]
        public int? MaximumPing { get; set; }

        /// <summary>
        /// Gets or sets a max packetloss value until server kick the user. 
        /// </summary>
        [ConfigProperty(PropertyName = "maxpacketloss")]
        public int? MaximumPacketLoss { get; set; }

        /// <summary>
        /// Gets or sets rules for slow network players - log or log and kick.
        /// </summary>
        /// <remarks>Must be four entries with either 0 or 1 value.</remarks>
        /// <example>new[] {0,1,0,1}</example>
        [ConfigProperty(PropertyName = "kickClientsOnSlowNetwork[]")]
        public string[] SlowNetworkKickRules { get; set; } = new string[4];

        /// <summary>
        /// Gets or sets threshold for logging faulted callExtension.
        /// </summary>
        [ConfigProperty(PropertyName = "callExtReportLimit")]
        public float? CallExtensionReportLimit { get; set; }

        /// <summary>
        /// Gets or sets rules for different type of issue - manual kick, connectivity kick, BattleEye kick et cetera.
        /// </summary>
        /// <remarks>First value is ID of issue (from 0 to 3), second one is a timeout in seconds.</remarks>
        //[ConfigProperty(PropertyName = "kickTimeout[]")]
        //public Dictionary<int,int> KickTimeoutRules { get; set; } = new Dictionary<int, int>();

        /// <summary>
        /// Gets or sets a seconds between votings. 
        /// </summary>
        [ConfigProperty(PropertyName = "votingTimeOut")]
        public int? VotingTimeout { get; set; }

        /// <summary>
        /// Gets or sets a seconds for role selection. 
        /// </summary>
        [ConfigProperty(PropertyName = "roleTimeOut")]
        public int? RoleSelectionTimeout { get; set; }

        /// <summary>
        /// Gets or sets a seconds for briefing. 
        /// </summary>
        [ConfigProperty(PropertyName = "briefingTimeOut")]
        public int? BriefingTimeout { get; set; }

        /// <summary>
        /// Gets or sets a seconds for debriefing. 
        /// </summary>
        [ConfigProperty(PropertyName = "debriefingTimeOut")]
        public int? DebriefingTimeout { get; set; }

        /// <summary>
        /// Gets or sets a seconds for lobby idling. 
        /// </summary>
        [ConfigProperty(PropertyName = "lobbyIdleTimeout")]
        public int? LobbyIdleTimeout { get; set; }

        /// <summary>
        /// Gets or sets a value for enabling or disabling signature verification. 
        /// </summary>
        [ConfigProperty(PropertyName = "verifySignatures")]
        public int SignatureVerificationMode { get; set; }

        /// <summary>
        /// Gets or sets a value for enabling or disabling the ability to place markers and draw lines in map. 
        /// </summary>
        [ConfigProperty(PropertyName = "drawingInMap")]
        public bool IsDrawingOnMapAllowed { get; set; }

        /// <summary>
        /// Gets or sets a value for enabling or disabling the Voice over Net. 
        /// </summary>
        [ConfigProperty(PropertyName = "disableVoN")]
        public int? DisableVoice { get; set; }

        /// <summary>
        /// Gets or sets a value for defining VoN codec quality. 
        /// </summary>
        [ConfigProperty(PropertyName = "vonCodecQuality")]
        public int? VoiceCodecQuality { get; set; } = 3;

        /// <summary>
        /// Gets or sets a value for defining VoN codec. 
        /// </summary>
        [ConfigProperty(PropertyName = "vonCodec")]
        public int VoiceCodecType { get; set; }

        /// <summary>
        /// Gets or sets a value for skipping lobby stage for joining players. 
        /// </summary>
        [ConfigProperty(PropertyName = "skipLobby")]
        public bool IsLobbySkipped { get; set; }

        /// <summary>
        /// Gets or sets a value for enabling output of dedicated server console into textfile.
        /// </summary>
        [ConfigProperty(PropertyName = "logFile")]
        public string LogFileName { get; set; }

        /// <summary>
        /// Gets or sets a command when 2nd user with the same ID detected.
        /// </summary>
        [ConfigProperty(PropertyName = "doubleIdDetected")]
        public string OnDoubleIdCommand { get; set; }

        /// <summary>
        /// Gets or sets a command when 2nd user with the same ID detected.
        /// </summary>
        [ConfigProperty(PropertyName = "onUserConnected")]
        public string OnUserConnectedCommand { get; set; }

        /// <summary>
        /// Gets or sets a command when user has disconnected.
        /// </summary>
        [ConfigProperty(PropertyName = "onUserDisconnected")]
        public string OnUserDisconnectedCommand { get; set; }

        /// <summary>
        /// Gets or sets a command when modification of signed pbo detected.
        /// </summary>
        [ConfigProperty(PropertyName = "onHackedData")]
        public string OnHackedDataCommand { get; set; }

        /// <summary>
        /// Gets or sets a command when signed pbo detected with a valid signature.
        /// </summary>
        [ConfigProperty(PropertyName = "onDifferentData")]
        public string OnDifferentDataCommand { get; set; }

        /// <summary>
        /// Gets or sets a command when unsigned data detected.
        /// </summary>
        [ConfigProperty(PropertyName = "onUnsignedData")]
        public string OnUnsignedDataCommand { get; set; }

        /// <summary>
        /// Gets or sets a command when user has kicked.
        /// </summary>
        [ConfigProperty(PropertyName = "onUserKicked")]
        public string OnUserKickedCommand { get; set; }

        /// <summary>
        /// Gets or sets a command for enabling or disabling the BattlEye anti-cheat engine.
        /// </summary>
        [ConfigProperty(PropertyName = "BattlEye")]
        public bool HasBattleEye { get; set; }

        /// <summary>
        /// Gets or sets timestamp format used on each report line in server-side RPT file.
        /// </summary>
        [ConfigProperty(PropertyName = "timeStampFormat")]
        public string TimeStampFormat { get; set; }

        /// <summary>
        /// Gets or sets a value for manipulating RotorLib simulation on server.
        /// </summary>
        [ConfigProperty(PropertyName = "forceRotorLibSimulation")]
        public int? IsRotorLibSimulationForced { get; set; }

        /// <summary>
        /// Gets or sets a value for enabling or disabling server running when all clients disconnected.
        /// </summary>
        [ConfigProperty(PropertyName = "persistent")]
        public int? IsPersistent { get; set; }

        /// <summary>
        /// Gets or sets a value for not allowing to connect all clients with not equal build version.
        /// </summary>
        [ConfigProperty(PropertyName = "requiredBuild")]
        public string RequiredBuild { get; set; }

        /// <summary>
        /// Gets or sets a value for forcing difficulty for every mission on server.
        /// </summary>
        [ConfigProperty(PropertyName = "forcedDifficulty")]
        public string ForcedDifficulty { get; set; }

        /// <summary>
        /// Gets or sets a value for limiting the available missions for the admin for the mission change.
        /// </summary>
        [ConfigProperty(PropertyName = "missionWhitelist[]")]
        public List<string> MissionWhitelist { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets a value for only allowing files with certain extensions to be loaded via loadFile command.
        /// </summary>
        [ConfigProperty(PropertyName = "allowedLoadFileExtensions[]")]
        public List<string> FileExtensionsWhitelist { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets a value for only allowing files with certain extensions 
        /// to be loaded via preprocessFile/preprocessFileLineNumber.
        /// </summary>
        [ConfigProperty(PropertyName = "allowedPreprocessFileExtensions[]")]
        public List<string> PreprocessFileExtensionsWhitelist { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets a value for only allowing files with certain extensions to be loaded via HTMLLoad command.
        /// </summary>
        [ConfigProperty(PropertyName = "allowedHTMLLoadExtensions[]")]
        public List<string> HtmlFileExtensionsWhitelist { get; set; } = new List<string>();

        //TODO: allowedVotedAdminCmds[]
        //TODO: allowedVoteCmds[]
        //TODO: missionsToServerRestart
        //TODO: missionsToShutdown
        //TODO: autoSelectMission
        //TODO: randomMissionOrder
        //TODO: Missions
        //TODO: Kick Rules
        //https://community.bistudio.com/wiki/Arma_3:_Mission_voting
    }
}
