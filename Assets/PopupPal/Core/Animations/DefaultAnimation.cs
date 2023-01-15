using System.Threading.Tasks;
using UnityEngine;

namespace PopupPal.Core.Animations
{
    public class DefaultAnimation : IPopupAnimation
    {
        private readonly float scaleIncrement = 0.05f;
        
        private float _showDuration;
        private float _hideDuration;

        public DefaultAnimation(float showDuration = 0.5f, float hideDuration = 0.5f)
        {
            _showDuration = showDuration;
            _hideDuration = hideDuration;
        }

        public async Task Show(RectTransform popup)
        {
            popup.localScale = Vector3.zero;
            var startTime = Time.time;
            while (Time.time - startTime < _showDuration)
            {
                popup.localScale += Vector3.one * scaleIncrement;
                await Task.Delay(10);
            }
            popup.localScale = Vector3.one;
        }

        public async Task Hide(RectTransform popup)
        {
            var startTime = Time.time;
            while (Time.time - startTime < _hideDuration)
            {
                popup.localScale -= Vector3.one * scaleIncrement;
                await Task.Delay(10);
            }
            popup.localScale = Vector3.zero;
        }
    }
}
