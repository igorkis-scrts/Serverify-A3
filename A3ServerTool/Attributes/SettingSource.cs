using System;
using A3ServerTool.Models;

namespace A3ServerTool.Attributes
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
