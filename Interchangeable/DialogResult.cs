using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace Interchangeable
{
    /// <summary>
    /// Custom dialog result
    /// </summary>
    public struct DialogResult<T>
    {
        public MessageDialogResult Message;
        public T Object;

        public DialogResult(MessageDialogResult messageResult, T objectResult)
        {
            Message = messageResult;
            Object = objectResult;
        }
    }
}
