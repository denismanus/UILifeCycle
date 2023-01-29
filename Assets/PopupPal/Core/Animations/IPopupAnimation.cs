using System.Threading.Tasks;
using UnityEngine;

namespace PopupPal.Core.Animations
{
    public interface IPopupAnimation
    {
        Task Show(RectTransform rectTransform);
        Task Hide(RectTransform rectTransform);
    }
}
