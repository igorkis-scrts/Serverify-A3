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
    public struct SaveDialogResult<T>
    {
        public MessageDialogResult Message;
        public T Object;
        public SaveObjectActionType ActionType;

        public SaveDialogResult(MessageDialogResult messageResult, T objectResult, SaveObjectActionType actionType)
        {
            Message = messageResult;
            Object = objectResult;
            ActionType = actionType;
        }
    }
}
