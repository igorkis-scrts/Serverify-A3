using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Where server setting stores parameters in actual server environment
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SettingSource : Attribute
    {
        public SettingSourceType SourceType { get; set; }
    } 
}
