using System.ComponentModel;

namespace A3ServerTool.Enums
{
    /// <summary>
    /// Provides representation of allowedFilePatching config property in code.
    /// </summary>
    public enum FilePatchingType
    {
        [Description("No Clients")]
        NoClients = 0,

        [Description("Headless Clients")]
        HeadlessClients = 1,

        [Description("All Clients")]
        AllClients = 2
    }
}
