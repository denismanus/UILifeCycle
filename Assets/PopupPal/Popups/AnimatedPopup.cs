using System.Threading.Tasks;
using PopupPal.Core.Animations;
using SimplePopups;

namespace PopupPal.Popups
{
    public abstract class AnimatedPopup : PopupBase
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
    }
}