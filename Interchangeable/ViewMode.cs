using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interchangeable
{
    /// <summary>
    /// Enum for creational views - editing existing instance of object or creating completely new.
    /// </summary>
    public enum ViewMode
    {
        None = 0,
        Edit = 1,
        New = 2,
        Save = 3
    }
}
