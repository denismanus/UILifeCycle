using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ConflictCards.UI.Core.Debug
{
    [CustomEditor(typeof(UIDebugger), true)]
    public class UIDebuggerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UIDebugger targ = target as UIDebugger;
            if (targ.DebugItems != null)
            {
                foreach (KeyValuePair<string, Action> item in targ.DebugItems)
                {
                    if (GUILayout.Button(item.Key, EditorStyles.miniButton))
                    {
                        item.Value();
                    }
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Please enter the Play Mode", MessageType.Info);
            }
        }
    }
}