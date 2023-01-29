using UnityEngine;

namespace PopupPal.Core.Blackout
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Blackout : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

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
