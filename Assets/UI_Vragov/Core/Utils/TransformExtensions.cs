using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ConflictCards
{
    public static class TransformExtensions
    {
        public static void RemoveAllChildren(this Transform parent)
        {
            parent.gameObject.SetActive(false);

            int childCount = parent.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Object.Destroy(parent.GetChild(i).gameObject);
            }

            parent.gameObject.SetActive(true);
        }

        public static void SmartAddChildren<T>(this RectTransform contentRoot, T elementTemplate, int dataCount, ref List<T> cachedViews, Action<int, T> onInit)
            where T : MonoBehaviour
        {
            //RectTransform contentRoot = layout.transform as RectTransform;

            int maxCount = Mathf.Max(dataCount, cachedViews.Count);
            for (int i = 0; i < maxCount; i++)
            {
                bool hasView = i < cachedViews.Count;
                bool isExtraView = i >= dataCount;

                // Get existing view or instantiate a new
                T itemView = hasView
                    ? cachedViews[i]
                    : GameObject.Instantiate(elementTemplate, contentRoot);

                // Turn on required amount of views / disable extra ones
                itemView.gameObject.SetActive(!isExtraView);

                // We don't need to update state for extra views
                if (!isExtraView)
                {
                    onInit?.Invoke(i, itemView);
                }

                if (!hasView)
                {
                    cachedViews.Add(itemView);
                }
            }
        }
    }
}
