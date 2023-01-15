using System;
using System.Threading.Tasks;
using PopupPal.Core.Animations;
using SimplePopups;

public class AnimatedPopup : PopupBase
{
    protected IPopupAnimation _popupAnimation;
    
    public AnimatedPopup WithAnimation(IPopupAnimation popupAnimation)
    {
        _popupAnimation = popupAnimation;
        return this;
    }

    public override async Task Show()
    {
        await base.Show();

        if (_popupAnimation != null)
            await _popupAnimation.Show(_body);
    }

    public override async Task Hide()
    {
        if (_popupAnimation != null)
            await _popupAnimation.Hide(_body);
        
        await base.Hide();
    }

    public AnimatedPopup WithPositiveCallback(Action handleYes)
    {
        return this;
    }

    public AnimatedPopup WithNegativeCallback(Action handleNo)
    {
        return this;
    }

    public AnimatedPopup WithInitData(object data)
    {
        return this;
    }
}