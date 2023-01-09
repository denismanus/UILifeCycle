using System;
using UnityEngine;
using UnityEngine.UI;

public abstract partial class PopupBase : MonoBehaviour, INotifyClosed
{
    [SerializeField] protected Button _closeButton;

    public event Action OnClosed;

    public PopupDisplayParameters PopupDisplayParameters { get; private set; }

    protected virtual void Awake()
    {
        if (_closeButton != null)
            _closeButton.onClick.AddListener(Close);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        OnClosed?.Invoke();

        OnClosed = null;
    }

    public PopupBase Show()
    {
        gameObject.SetActive(true);
        return this;
    }

    public PopupBase Hide()
    {
        gameObject.SetActive(false);
        return this;
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