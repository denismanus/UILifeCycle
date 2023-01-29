using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SimplePopups
{
    public abstract class PopupBase : MonoBehaviour, INotifyClosed
    {
        [SerializeField] protected Button _closeButton;
        [SerializeField] protected RectTransform _body;

        public event Action OnClosed;

        protected virtual void Awake()
        {
            if (_closeButton != null)
#pragma warning disable 4014
                _closeButton.onClick.AddListener(()=>Close());
#pragma warning restore 4014
        }

        public async Task Close()
        {
            await Hide();
        
            OnClosed?.Invoke();
            OnClosed = null;
        }

        public virtual Task Show()
        {
            gameObject.SetActive(true);
            return Task.CompletedTask;
        }

        public virtual Task Hide()
        {
            gameObject.SetActive(false);
            return Task.CompletedTask;
        }
    }

    public struct PopupConfig
    {
        public bool BlackoutInvisible;
        public bool ShowBlackout;
        public bool CloseOnOverlayByClick;
        public bool BlockInput;
        public bool ReplaceAnotherPopups;
    
        public PopupConfig SetBlackoutInvisible()
        {
            BlackoutInvisible = true;
            return this;
        }

        public PopupConfig AllowInput()
        {
            BlockInput = false;
            return this;
        }

        public PopupConfig CloseWhenOverlayClicked()
        {
            CloseOnOverlayByClick = true;
            return this;
        }

        public PopupConfig ClosePreviousPopups()
        {
            ReplaceAnotherPopups = true;
            return this;
        }
    }
}