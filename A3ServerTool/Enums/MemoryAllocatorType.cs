using System.ComponentModel;

namespace A3ServerTool.Enums
{
    /// <summary>
    /// Provides enum for various memory allocators for A3.
    /// </summary>
    public enum MemoryAllocatorType
    {
        [Description("Not Specified")]
        NotSpecified = 0,

        [Description("TBB4_BI")]
        tbb4malloc_bi = 1,

        [Description("JEMalloc_BI")]
        jemalloc_bi = 2,

        [Description("System")]
        system = 3
    }
}
