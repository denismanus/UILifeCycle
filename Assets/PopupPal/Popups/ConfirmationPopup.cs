using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace PopupPal.Popups
{
    public sealed class ConfirmationPopup : AnimatedPopup
    {
        public event Action<ConfirmationPopup> OnConfirm;
        public event Action<ConfirmationPopup> OnCancel;
        
        [SerializeField] private Button _confirm;
        [SerializeField] private Button _cancel;

        private event Action _confirmationCB;
        private event Action _cancelCB;

        protected override void Awake()
        {
            base.Awake();
#pragma warning disable 4014
            _confirm.onClick.AddListener(()=>HandleConfirm());
            _cancel.onClick.AddListener(()=>HandleCancel());
#pragma warning restore 4014
        }

        public ConfirmationPopup SetConfirmCallback(Action cb)
        {
            _confirmationCB += cb;
            return this;
        }
        
        public ConfirmationPopup SetCancelCallback(Action cb)
        {
            _cancelCB += cb;
            return this;
        }

        private async Task HandleConfirm()
        {
            await Close();
            OnConfirm?.Invoke(this);
            OnConfirm = null;
        }

        private async Task HandleCancel()
        {
            await Close();
            OnCancel?.Invoke(this);
            OnCancel = null;
        }
    }
}
