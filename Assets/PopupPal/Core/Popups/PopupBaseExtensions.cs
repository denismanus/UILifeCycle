using System;
using System.Runtime.CompilerServices;

namespace SimplePopups
{
    public static class PopupBaseExtensions
    {
        public static PopupBaseAwaiter GetAwaiter(this PopupBase popupBase)
        {
            return new PopupBaseAwaiter(popupBase);
        }

        public struct PopupBaseAwaiter : INotifyCompletion
        {
            public bool IsCompleted => false;

            private PopupBase _popupBase;
            private Action _continuation;

            public PopupBaseAwaiter(PopupBase popupBase)
            {
                _popupBase = popupBase;
                _continuation = null;
            }

            public void GetResult()
            {
            }

            private void HandlePopupClosed()
            {
                _continuation();
            }

            public void OnCompleted(Action continuation)
            {
                _continuation = continuation;
                _popupBase.OnClosed += HandlePopupClosed;
            }
        }
    }
}