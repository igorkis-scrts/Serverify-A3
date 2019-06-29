using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Helpers
{
    public class StringArrayComparer : IComparer<string[]>
    {
        public int Compare(string[] x, string[] y)
        {
            return x.Length - y.Length;
        }
    }
}
