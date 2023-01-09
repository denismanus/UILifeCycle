using System;
using UnityEngine;
using UnityEngine.UI;

public abstract partial class PopupBase : MonoBehaviour, INotifyClosed
{
    [SerializeField] protected Button _closeButton;

    public event Action<PopupBase> OnShowRequested;
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
        _popupConfig = new PopupConfig();
    }

    public PopupBase Show(PopupDisplayParameters popupDisplayParameters)
    {
        PopupDisplayParameters = popupDisplayParameters;
        OnShowRequested?.Invoke(this);
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

public abstract partial class PopupBase
{
    private PopupConfig _popupConfig;

    public PopupConfig Config => _popupConfig;

    public PopupBase WithPopupConfig(PopupConfig popupConfig)
    {
        _popupConfig = popupConfig;
        return this;
    }

    public PopupBase SetBlackoutInvisible()
    {
        _popupConfig.BlackoutInvisible = true;
        return this;
    }

    public PopupBase AllowInput()
    {
        _popupConfig.BlockInput = false;
        return this;
    }

    public PopupBase CloseWhenOverlayClicked()
    {
        _popupConfig.CloseOnOverlayByClick = true;
        return this;
    }

    public PopupBase ClosePreviousPopups()
    {
        _popupConfig.ClearPopupsStack = true;
        return this;
    }
}

public struct PopupConfig
{
    public bool BlackoutInvisible;
    public bool ShowBlackout;
    public bool CloseOnOverlayByClick;
    public bool BlockInput;
    public bool ClearPopupsStack;
}