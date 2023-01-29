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
        [SerializeField] private Button _showWithAppendOverPopupsButton;

        protected override void Awake()
        {
            base.Awake();
            _showWithHidingPreviousButton.onClick.AddListener(ShowAndHidePreviousPopups);
            _showWithAppendOverPopupsButton.onClick.AddListener(ShowOverPreviousPopups);
        }

        private void ShowAndHidePreviousPopups()
        {
            _uiService
                .Get<ConfirmationPopup>()
                .WithConfigs(
                    new PopupConfig()
                    {
                        ShowBlackout = true,
                        BlockInput = true,
                        ReplaceAnotherPopups = true
                    })
                .Show();
        }
        
        private void ShowOverPreviousPopups()
        {
            _uiService
                .Get<ConfirmationPopup>()
                .WithConfigs(
                    new PopupConfig()
                    {
                        ShowBlackout = true,
                        BlockInput = true,
                    })
                .Show();
        }
    }
}
