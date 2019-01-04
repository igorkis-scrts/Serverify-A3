using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Server profile
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Name of setting
        /// </summary>
        public string Name { get; set; }
       
        /// <summary>
        /// Profile type
        /// </summary>
        public ProfileType Type { get; set; }

        /// <summary>
        /// Short description of server profile
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Server settings
        /// </summary>
        public IServerSettings ServerSettings { get; set; }
    }
}
