using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class UIService : MonoBehaviour
{
    [SerializeField] private PopupBase[] _popups;

    private PopupService _popupService;

    private void Awake()
    {
        Init();

        _popupService = new PopupService();
    }

    private void Init()
    {
        foreach (var popup in _popups)
            popup.OnShowRequested += ShowPopupInternal;
    }

    private void ShowPopupInternal(PopupBase obj)
    {
        _popupService.Show(obj);
    }

    public T Get<T>() where T : PopupBase
    {
        return _popups.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
    }
}

public partial class UIService
{
    private class PopupService
    {
        //Since we may want to restore popup with specific immutable config we need to store them
        private Dictionary<PopupBase, PopupConfig> _configs = new Dictionary<PopupBase, PopupConfig>();
        
        public void Show(PopupBase popupBase)
        {
            _configs.Add(popupBase, popupBase.Config);

            BuildPopupInternal(popupBase);
        }

        private void BuildPopupInternal(PopupBase popupBase)
        {
            var config = _configs[popupBase];
            popupBase.gameObject.SetActive(true);
        }
    }
}