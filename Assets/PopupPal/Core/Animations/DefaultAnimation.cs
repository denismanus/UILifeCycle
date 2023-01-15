using System.Threading.Tasks;
using UnityEngine;

namespace PopupPal.Core.Animations
{
    //This is ChatGPT implementation :)
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
                popup.localScale = new Vector3(Mathf.Min(popup.localScale.x, 1), Mathf.Min(popup.localScale.y, 1), Mathf.Min(popup.localScale.z, 1));
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
                popup.localScale = new Vector3(Mathf.Max(popup.localScale.x, 0), Mathf.Max(popup.localScale.y, 0), Mathf.Max(popup.localScale.z, 0));
                await Task.Delay(10);
            }
            popup.localScale = Vector3.zero;
            popup.gameObject.SetActive(false);
        }
    }
}
