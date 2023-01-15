using System.Threading.Tasks;
using PopupPal.Core.Animations;
using SimplePopups;
using UnityEditor.Search;
using UnityEngine;

public class Sample : MonoBehaviour
{
    public UIService _uiService;

    private void Start()
    {
    }

    [ContextMenu("SHow")]
    private void Show()
    {
        ShowInternal();
    }


    private async Task ShowInternal()
    {
        await _uiService
            .Get<AnimatedPopup>()
            .WithState(x =>
            {
                x.WithNegativeCallback(HandleNo)
                    .WithPositiveCallback(HandleYes)
                    .WithInitData(null)
                    .WithAnimation(new DefaultAnimation())
                    .SetPopupClosedCallback(HandleClosed);
            })
            .WithConfigs(x =>
            {
                x.SetBlackoutInvisible()
                .ClosePreviousPopups();
            })
            .Show();
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