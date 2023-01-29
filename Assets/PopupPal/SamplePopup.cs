using PopupPal.Popups;
using SimplePopups;
using UnityEngine;
using UnityEngine.UI;

namespace PopupPal
{
    public class SamplePopup : AnimatedPopup
    {
        [SerializeField] private UIService _uiService;
        [SerializeField] private Button _showWithHidingPreviousButton;

        protected override void Awake()
        {
            base.Awake();
            _showWithHidingPreviousButton.onClick.AddListener(ShowAndHidePreviousPopups);
        }

        private void ShowAndHidePreviousPopups()
        {
            _uiService
                .Get<ConfirmationPopup>()
                .WithConfigs(
                    new PopupConfig()
                    {
                        BlockInput = true,
                        ClearPopupsStack = true
                    })
                .Show();
        }
    }
}
