using ConflictCards.UI.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ConflictCards.UI.Common
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public abstract class CanvasViewBase : UIViewBase
    {
        public override bool InputEnabled
        {
            get => _raycaster.enabled;
            set => _raycaster.enabled = value;
        }

        [Header("General Stuff")]
        [SerializeField] protected Canvas _canvas;
        [SerializeField] protected GraphicRaycaster _raycaster;

        private void Reset()
        {
            _canvas = GetComponent<Canvas>();
            _raycaster = GetComponent<GraphicRaycaster>();
        }

        public override void Show()
        {
            _canvas.enabled = true;
            InputEnabled = true;
        }

        public override void Hide()
        {
            _canvas.enabled = false;
            InputEnabled = false;

            // Deselect all selected objects to
            // avoid interception of UI events
            EventSystem es = EventSystem.current;
            if (es != null)
            {
                es.SetSelectedGameObject(null);
            }
        }
    }
}