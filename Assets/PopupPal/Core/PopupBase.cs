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
                _closeButton.onClick.AddListener(()=>Close());
        }

        private async Task Close()
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

        public PopupBase SetPopupClosedCallback(Action callback)
        {
            OnClosed += callback;
            return this;
        }
    }

    public enum PopupDisplayParameters
    {
        Default,
        Animated
    }

    public struct PopupConfig
    {
        public bool BlackoutInvisible;
        public bool ShowBlackout;
        public bool CloseOnOverlayByClick;
        public bool BlockInput;
        public bool ClearPopupsStack;
    
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
            ClearPopupsStack = true;
            return this;
        }
    }
}