using System.Threading.Tasks;
using UnityEngine;

namespace PopupPal.Core.Animations
{
    public interface IPopupAnimation
    {
        public Task Show(RectTransform rectTransform);
        public Task Hide(RectTransform rectTransform);
    }
}
