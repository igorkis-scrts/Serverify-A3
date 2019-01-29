using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Current selected profile to perform all our operations
    /// </summary>
    public sealed class CurrentProfile
    {
        // ReSharper disable once InconsistentNaming
        private static readonly CurrentProfile _instance = new CurrentProfile();

        static CurrentProfile() {}

        private CurrentProfile() {}

        // ReSharper disable once ConvertToAutoProperty
        public static CurrentProfile Instance => _instance;
    }
}
