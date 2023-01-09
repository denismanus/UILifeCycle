using System.Collections.Generic;
using UnityEngine;

namespace ConflictCards.UI.Core
{
    public class UIViewCollection : MonoBehaviour
    {
        [SerializeField] private UIViewBase[] _views;

        private readonly Dictionary<int, UIViewBase> _viewCache = new Dictionary<int, UIViewBase>();


        public bool TryGetView(int id, out UIViewBase screenView)
        {
            EnsureCache();
            return _viewCache.TryGetValue(id, out screenView);
        }

        public bool TryGetView<T>(int id, out T screenView) where T : UIViewBase
        {
            EnsureCache();
            _viewCache.TryGetValue(id, out UIViewBase pureView);
            return (screenView = pureView as T) != null;
        }

        private void EnsureCache()
        {
            if (_viewCache.Count == 0)
            {
                for (int i = 0; i < _views.Length; i++)
                {
                    UIViewBase screen = _views[i];
                    _viewCache.Add(screen.Id, screen);
                }
            }
        }
    }
}