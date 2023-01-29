using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace SimplePopups
{
    public partial class UIService : MonoBehaviour
    {
        [SerializeField] private PopupBase[] _popups;

        private PopupService _popupService;

        private void Awake()
        {
            _popupService = new PopupService();
        }

        private void Update()
        {
            _popupService.Update();
        }

        public PopupBuilder<T> Get<T>() where T : PopupBase
        {
            return new PopupBuilder<T>(_popups.FirstOrDefault(x => x.GetType() == typeof(T)) as T, ShowPopupInternal);
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
    }

    public partial class UIService
    {
        private class PopupService
        {
            //Since we may want to restore popup with specific immutable config we need to store them
            private Dictionary<PopupBase, PopupConfig> _popups = new Dictionary<PopupBase, PopupConfig>();
            private bool _isTransitioning;

            private Queue<PopupBase> _popupQueue = new Queue<PopupBase>();

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

                ShowPopupInternal(_popupQueue.Dequeue());
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
                        pair.Key.Close();
            }
        }
    }
}