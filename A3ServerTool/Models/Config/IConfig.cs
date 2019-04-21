using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Models.Config
{
    /// <summary>
    /// Provides common interface for config files
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Config file name
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Config file extension 
        /// </summary>
        string FileExtension { get; }

        /// <summary>
        /// Config file location
        /// </summary>
        string FileLocation { get; }
    }
}
