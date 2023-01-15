using System.Threading.Tasks;
using SimplePopups;
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
            .WithState(x =>
            {
                x.SetNegativeCallback(HandleNo)
                    .SetPositiveCallback(HandleYes)
                    .SetInitData(null)
                    .SetPopupClosedCallback(HandleClosed);
            })
            .WithConfigs(x =>
            {
                x.SetBlackoutInvisible()
                .ClosePreviousPopups();
            })
            .Show(PopupDisplayParameters.Default);
    }

    private void HandleYes()
    {
    }

    private void HandleNo()
    {
    }

    private void HandleClosed()
    {
    }
}