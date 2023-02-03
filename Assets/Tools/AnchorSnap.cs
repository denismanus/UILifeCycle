﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Tools
{
    public static class AnchorSnapTool
    {
        [MenuItem("Tools/UI/SnapAnchors!")]
        // Start is called before the first frame update
        public static void SnapAnchors()
        {
            var go = Selection.activeGameObject;
            if (go == null)
                return;

            if (!go.TryGetComponent(out RectTransform target))
                return;

            var canvas = target.GetComponentInParent<Canvas>();
            if (canvas == null)
                return;

            var canvasRect = canvas.GetComponent<RectTransform>().rect;
            var targetRect = target.rect;
            var percentWidth = Mathf.Clamp(targetRect.width / canvasRect.width, 0f, 1f);
            var percentHeight = Mathf.Clamp(targetRect.height / canvasRect.height, 0f, 1f);

            var anchorMinX =
                Mathf.Clamp((canvasRect.width / 2 + target.anchoredPosition.x) / canvasRect.width - percentWidth / 2,
                    0f, 1f);
            var anchorMinY =  Mathf.Clamp((canvasRect.height / 2 + target.anchoredPosition.y) / canvasRect.height -
                             percentHeight / 2, 0f, 1f);

            target.anchorMin = new Vector2(anchorMinX, anchorMinY);
            target.anchorMax = new Vector2(anchorMinX + percentWidth, anchorMinY + percentHeight);
            target.sizeDelta = Vector2.zero;
            target.anchoredPosition = Vector2.zero;
        }
    }
}
#endif