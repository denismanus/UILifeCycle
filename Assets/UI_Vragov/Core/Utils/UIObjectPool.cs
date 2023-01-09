using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ConflictCards.Utils
{
    public class UIObjectPool<T> where T : MonoBehaviour
    {
        private readonly RectTransform _contentRoot;
        private readonly T _itemTemplate;
        private readonly List<T> _cachedItems;
        private readonly Queue<int> _separatedItems;

        private int _activeCount;

        public UIObjectPool(RectTransform contentRoot, T itemTemplate)
        {
            _contentRoot = contentRoot;
            _itemTemplate = itemTemplate;
            _cachedItems = new List<T>();
            _separatedItems = new Queue<int>();
            _activeCount = 0;
        }

        public UIObjectPool(RectTransform contentRoot, T itemTemplate, IEnumerable<T> prewarmedItems)
            : this(contentRoot, itemTemplate)
        {
            _cachedItems.AddRange(prewarmedItems);
        }

        public T AddItem()
        {
            T obj;
            if (_separatedItems.TryDequeue(out int index))
            {
                obj = _cachedItems[index];
            }
            else
            {
                _activeCount++;
                if (_activeCount >= _cachedItems.Count)
                {
                    obj = Object.Instantiate(_itemTemplate, _contentRoot);
                    _cachedItems.Add(obj);
                }
                else
                {
                    obj = _cachedItems[_activeCount - 1];
                }
            }

            obj.gameObject.SetActive(true);
            return obj;
        }

        public int RemoveItem(T item, bool skipCheck = true)
        {
            int index = _cachedItems.FindIndex(p => p == item);
            RemoveItem_Internal(index, item, skipCheck);

            return index;
        }

        public T RemoveItem(int index, bool skipCheck = true)
        {
            T item = _cachedItems[index];
            RemoveItem_Internal(index, item, skipCheck);

            return item;
        }

        public void RemoveAll()
        {
            _separatedItems.Clear();
            _activeCount = 0;

            for (int i = 0; i < _cachedItems.Count; i++)
            {
                _cachedItems[i].gameObject.SetActive(false);
            }
        }

        public void SetItems(int count, Action<int, T> onInit)
        {
            _separatedItems.Clear();
            _activeCount = count;

            int maxCount = Mathf.Max(count, _cachedItems.Count);
            for (int i = 0; i < maxCount; i++)
            {
                bool hasView = i < _cachedItems.Count;
                bool isExtraView = i >= count;

                // Get existing view or instantiate a new
                T itemView = hasView
                    ? _cachedItems[i]
                    : Object.Instantiate(_itemTemplate, _contentRoot);

                // Turn on required amount of views / disable extra ones
                itemView.gameObject.SetActive(!isExtraView);

                // We don't need to update state for extra views
                if (!isExtraView)
                {
                    onInit?.Invoke(i, itemView);
                }

                if (!hasView)
                {
                    _cachedItems.Add(itemView);
                }
            }
        }


        private void RemoveItem_Internal(int index, T item, bool skipCheck)
        {
            if (skipCheck || !_separatedItems.Contains(index))
            {
                item.gameObject.SetActive(false);

                // Don't add to separate items if removed item is the last active one
                // In this case simply decrease the active items counter
                if (index != _activeCount - 1)
                {
                    _separatedItems.Enqueue(index);
                }
                else
                {
                    _activeCount--;
                }
            }
        }
    }
}