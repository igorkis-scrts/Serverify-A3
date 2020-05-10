using Interchangeable;
using MahApps.Metro.Controls.Dialogs;

namespace A3ServerTool.Helpers
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
