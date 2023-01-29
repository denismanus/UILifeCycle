using UnityEngine;

namespace PopupPal.Core.Blackout
{
    [RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
    public class Blackout : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rect;

        public RectTransform Rect => _rect;

        private void Awake()
        {
            if(_canvasGroup!=null)
                return;

            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetAlpha(float alpha)
        {
            _canvasGroup.alpha = alpha;
        }

        public void SetBlockRaycasts(bool value)
        {
            _canvasGroup.blocksRaycasts = value;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
