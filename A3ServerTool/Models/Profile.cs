using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Server profile
    /// </summary>
    public class Profile : IDataErrorInfo
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

        #region IDataErrorInfo members

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Name) when string.IsNullOrWhiteSpace(Name):
                        return "Profile name should be not empty";
                    case nameof(Description) when string.IsNullOrWhiteSpace(Description):
                        return "Profile description should be not empty";
                    default:
                        return null;
                }
            }
        }

        [JsonIgnore]
        public string Error => string.Empty;

        #endregion
    }
}
