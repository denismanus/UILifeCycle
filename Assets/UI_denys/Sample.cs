using System.Threading.Tasks;
using UnityEngine;

public class Sample : MonoBehaviour
{
    public UIService _uiService;

    private async void Start()
    {
        await ShowInternal();
    }

    private async Task ShowInternal()
    {
        await _uiService
            .Get<ConfirmationPopup>()
            .SetPositiveCallback(HandleYes)
            .SetNegativeCallback(HandleNo)
            .SetPopupClosedCallback(HandleClosed)
            .SetBlackoutInvisible()
            .Show(PopupDisplayParameters.Default);
    }
    
    private void HandleYes() {}
    private void HandleNo() {}
    private void HandleClosed() {}
}