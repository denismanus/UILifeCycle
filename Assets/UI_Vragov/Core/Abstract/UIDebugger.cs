using System;
using System.Collections.Generic;
using UnityEngine;

namespace ConflictCards.UI.Core
{
    /// <summary>
    /// Helper class to be used in a custom Editor instead of Generic one
    /// </summary>
    public abstract class UIDebugger : MonoBehaviour
    {
        public abstract Dictionary<string, Action> DebugItems { get; }
    }

    /// <summary>
    /// Base class for all UI debuggers
    /// </summary>
    /// <typeparam name="T">Debugger class type</typeparam>
    /// <typeparam name="U">Type of the corresponding UI Presenter</typeparam>
    public abstract class UIDebugger<T, U> : UIDebugger
        where T : UIDebugger<T, U>
        where U : IUIPresenter
    {
        private const string GO_NAME = "DEBUG";

        public override Dictionary<string, Action> DebugItems => _debugItems;
        private Dictionary<string, Action> _debugItems;

        protected U Presenter { get; private set; }


        public static void CreateNew(U presenter)
        {
            GameObject go = GameObject.Find(GO_NAME) ?? new GameObject(GO_NAME);
            go.transform.SetAsFirstSibling();
            T instance = go.AddComponent<T>();

            instance.Initialize(presenter);
        }

        private void Initialize(U presenter)
        {
            Presenter = presenter;

            _debugItems = new Dictionary<string, Action>()
            {
                { "Show", Presenter.Show },
                { "Hide", Presenter.Hide },
            };

            InitializeDebugItems(ref _debugItems);
        }

        protected abstract void InitializeDebugItems(ref Dictionary<string, Action> items);
    }
}