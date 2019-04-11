using System;
using A3ServerTool.Attributes;

namespace A3ServerTool.ProfileStorage.ConfigStorages
{
    /// <summary>
    /// Class that represents "basic.cfg" file. 
    /// For better expanation please visit <see href="https://community.bistudio.com/wiki/basic.cfg">BIS Wiki</see>
    /// </summary>
    public class BasicConfig
    {
        /// <summary>
        /// Another default parameter, intentionally read-only
        /// </summary>
        [BasicConfigProperty(PropertyName = "adapter", IgnoreParsing = true)]
        public int Adapter { get; } = -1;

        /// <summary>
        /// Another default parameter, intentionally read-only
        /// </summary>
        [BasicConfigProperty(PropertyName = "3D_Performance", IgnoreParsing = true)]
        public float Performance3D { get; } = 1;

        /// <summary>
        /// Resolution width, intentionally read-only
        /// </summary>
        [BasicConfigProperty(PropertyName = "Resolution_W", IgnoreParsing = true)]
        public int ResolutionWidth { get; } = 0;

        /// <summary>
        /// Resolution height, intentionally read-only
        /// </summary>
        [BasicConfigProperty(PropertyName = "Resolution_H", IgnoreParsing = true)]
        public int ResolutionHeight { get; } = 0;

        /// <summary>
        /// Resolution... bpp? Intentionally read-only
        /// </summary>
        [BasicConfigProperty(PropertyName = "Resolution_Bpp", IgnoreParsing = true)]
        public int ResolutionBpp { get; } = 32;

        /// <summary>
        /// Window screen mode, intentionally read-only
        /// </summary>
        [BasicConfigProperty(PropertyName = "Windowed", IgnoreParsing = true)]
        public int IsWindowed { get; } = 0;

        /// <summary>
        /// Maximum number of packets (aggregate messages) that can be sent in one simulation cycle ("frame"). 
        /// Increasing this value can decrease lag on high upload bandwidth servers.
        /// </summary>
        [BasicConfigProperty(PropertyName = "MaxMsgSend")]
        public int MaxMessagesSend { get; set; }

        /// <summary>
        /// Maximum size (payload) of guaranteed packet in bytes (without headers).
        /// Small messages are packed to larger packets(aggregate messages).
        /// Guaranteed packets(aggregate messages) are used for non-repetitive events like shooting.
        /// </summary>
        [BasicConfigProperty(PropertyName = "MaxSizeGuaranteed")]
        public int MaxSizeGuaranteed { get; set; }

        /// <summary>
        /// Maximum size (payload) of non-guaranteed packet in bytes (without headers).
        /// Small messages are packed to larger packets(aggregate messages).
        /// Non-guaranteed packets(aggregate messages) are used  for repetitive updates like soldier or vehicle position.
        /// Increasing this value may improve bandwidth requirement, but it may increase lag.
        /// </summary>
        [BasicConfigProperty(PropertyName = "MaxSizeNonguaranteed")]
        public int MaxSizeNonguaranteed { get; set; }

        /// <summary>
        /// Bandwidth the server is guaranteed to have (in bps).
        /// This value helps server to estimate bandwidth available.
        /// Increasing it to too optimistic values can increase lag and CPU load, as too many messages will be sent but discarded.
        /// </summary>
        [BasicConfigProperty(PropertyName = "MinBandwidth")]
        public int MinBandwidth { get; set; }

        /// <summary>
        /// Bandwidth the server is guaranteed to never have (in bps).
        /// This value helps the server to estimate bandwidth available.
        /// </summary>
        [BasicConfigProperty(PropertyName = "MaxBandwidth")]
        public int MaxBandwdith { get; set; }

        /// <summary>
        /// Minimal error to send updates across network.
        /// Using a smaller value can make units observed by binoculars or sniper rifle to move smoother at the trade off of
        /// increased network traffic.
        /// </summary>
        [BasicConfigProperty(PropertyName = "MinErrorToSend")]
        public float MinErrorToSend { get; set; }

        /// <summary>
        /// Minimal error to send updates across network for near units.
        /// Using larger value can reduce traffic sent for near units. Used to control client to server traffic as well.
        /// </summary>
        [BasicConfigProperty(PropertyName = "MinErrorToSendNear")]
        public float MinErrorToSendNear { get; set; }

        /// <summary>
        /// Maximum size of user generated content (custom faces, clan logos etc)
        /// </summary>
        [BasicConfigProperty(PropertyName = "MaxCustomFileSize")]
        public int MaxCustomFileSize { get; set; }

        /// <summary>
        /// Maximum packet size
        /// </summary>
        [BasicConfigProperty(PropertyName = "maxPacketSize")]
        public int MaxPacketSize { get; set; }

        /// <summary>
        /// Terrain render distance
        /// </summary>
        [BasicConfigProperty(PropertyName = "terrainGrid")]
        public int TerrainGridViewDistance { get; set; }

        /// <summary>
        /// Object view distance
        /// </summary>
        [BasicConfigProperty(PropertyName = "viewDistance")]
        public int ObjectViewDistance { get; set; }
    }
}
