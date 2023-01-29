using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PopupPal.Core;
using PopupPal.Core.Blackout;
using UnityEngine;

namespace SimplePopups
{
    public partial class UIService : MonoBehaviour
    {
        [SerializeField] private Blackout _blackout;
        [SerializeField] private GameObject _popupsProviderContainer;

        private IPopupsProvider _popupsProviderInstance;
        private PopupService _popupService;
        private Blackout _blackoutInstance;

        private void Awake()
        {
            TryInitBlackout();
            _popupService = new PopupService(_blackoutInstance);
        }

        private void Start()
        {
            TryInitProvider();
        }

        private void TryInitProvider()
        {
            if (_popupsProviderInstance != null)
                return;

            _popupsProviderInstance = _popupsProviderContainer.GetComponent<IPopupsProvider>();
        }

        private void TryInitBlackout()
        {
            if (_blackout == null)
                _blackout = BlackoutProvider.LoadDefault();

            if (_blackout.gameObject.scene.buildIndex == -1)
                _blackoutInstance = Instantiate(_blackout, transform);
        }

        private void Update()
        {
            _popupService.Update();
        }

        public void SetPopupsProvider(IPopupsProvider popupsProvider)
        {
            _popupsProviderInstance = popupsProvider;
        }

        public PopupBuilder<T> Get<T>() where T : PopupBase
        {
            return new PopupBuilder<T>(
                _popupsProviderInstance.Popups.FirstOrDefault(x => x.GetType() == typeof(T)) as T,
                ShowPopupInternal);
        }

        private void ShowPopupInternal<T>(T product, PopupConfig config)
            where T : PopupBase
        {
            _popupService.Show(product, config);
        }

        public class PopupBuilder<T>
        {
            private readonly T _product;
            private PopupConfig _popupConfig;

            private Action<T, PopupConfig> _show;

            public PopupBuilder(T product, Action<T, PopupConfig> showCallback)
            {
                _product = product;
                _show = showCallback;
            }

            public PopupBuilder<T> WithState(Action<T> action)
            {
                action.Invoke(_product);
                return this;
            }

            public PopupBuilder<T> WithConfigs(PopupConfig popupConfig)
            {
                _popupConfig = popupConfig;
                return this;
            }

            public T Show()
            {
                _show?.Invoke(_product, _popupConfig);
                return _product;
            }
        }

        private void Reset()
        {
            if (_popupsProviderContainer == null)
                return;

            if (_popupsProviderContainer.TryGetComponent(out IPopupsProvider popupsProvider))
            {
                _popupsProviderInstance = popupsProvider;
            }
            else
            {
                throw new Exception($"{_popupsProviderContainer.name} doesn't contain {typeof(IPopupsProvider)}");
            }
        }
    }

    public partial class UIService
    {
        private class PopupService
        {
            private readonly Blackout _blackout;

            //Since we may want to restore popup with specific immutable config we need to store them
            private Dictionary<PopupBase, PopupConfig> _popups = new Dictionary<PopupBase, PopupConfig>();
            private bool _isTransitioning;

            private Queue<PopupBase> _popupQueue = new Queue<PopupBase>();

            public PopupService(Blackout blackout)
            {
                _blackout = blackout;
            }

            public void Show(PopupBase popupBase, PopupConfig popupConfig)
            {
                _popups.Add(popupBase, popupConfig);
                _popupQueue.Enqueue(popupBase);
            }

            public void Update()
            {
                if (_isTransitioning)
                    return;

                if (_popupQueue.Count == 0)
                    return;

                try
                {
#pragma warning disable 4014
                    ShowPopupInternal(_popupQueue.Dequeue());
#pragma warning restore 4014
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }

            private async Task ShowPopupInternal(PopupBase popupBase)
            {
                _isTransitioning = true;

                popupBase.OnClosed += () => _popups.Remove(popupBase);
                ProcessPopupConfig(popupBase, _popups[popupBase]);

                await popupBase.Show();

                _isTransitioning = false;
            }

            private void ProcessPopupConfig(PopupBase popupBase, PopupConfig popupConfig)
            {
                if (popupConfig.ReplaceAnotherPopups)
                    foreach (var pair in _popups.Where(pair => pair.Key != popupBase))
#pragma warning disable 4014
                        pair.Key.Close();
#pragma warning restore 4014

                _blackout.SetActive(popupConfig.ShowBlackout);
                _blackout.SetAlpha(popupConfig.BlackoutInvisible ? 0 : 1);
                _blackout.SetBlockRaycasts(popupConfig.BlockInput);

                if (popupConfig.ShowBlackout)
                {
                    _blackout.transform.SetParent(popupBase.transform.parent);
                    _blackout.transform.SetSiblingIndex(popupBase.transform.GetSiblingIndex());
                    _blackout.Rect.sizeDelta = Vector2.zero;
                    _blackout.Rect.anchoredPosition = Vector2.zero;
                }
            }
        }
    }
}