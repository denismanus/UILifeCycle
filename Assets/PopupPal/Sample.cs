using System;
using System.Threading.Tasks;
using PopupPal;
using PopupPal.Core.Animations;
using PopupPal.Popups;
using SimplePopups;
using UnityEngine;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    [SerializeField] private Button _openMenuButton;
    [SerializeField] private UIService _uiService;

    private void Awake()
    {
        _openMenuButton.onClick.AddListener(ShowMenuPopup);
    }

    private void ShowMenuPopup()
    {
#pragma warning disable 4014
        ShowMenuPopupInternal();
#pragma warning restore 4014
    }

    private async Task ShowMenuPopupInternal()
    {
        await _uiService.Get<SamplePopup>()
            .WithState(x=>
                x.WithAnimation(new DefaultAnimation()))
            .Show();
        Debug.Log("Sample popup shown");
    }

    private void HandleClosed()
    {
        Debug.Log("Closed");
    }
}