using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A3ServerTool.Models.ConfigStorages;

namespace A3ServerTool.Models
{
    public interface IProfile
    {   
        /// <summary>
        /// Profile identifier
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Profile name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Short description of server profile
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Settings that wiil be send to server executable as arguments
        /// </summary>
        ArgumentSettings ArgumentSettings { get; }

        /// <summary>
        /// Represents basic.cfg
        /// </summary>
        BasicConfig BasicConfig { get; set; }
    }
}
