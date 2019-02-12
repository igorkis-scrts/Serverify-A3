using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Attributes
{
    /// <summary>
    /// For settings that passed to server executable as command line arguments
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CommandLineArgument : Attribute
    {
        public string Argument { get; set; }
    }
}
