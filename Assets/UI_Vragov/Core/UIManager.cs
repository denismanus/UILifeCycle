using System.Collections.Generic;

namespace ConflictCards.UI.Core
{
    public class UIManager
    {
        public IUIPresenter CurrentScreen { get; private set; }
        public IUIPresenter CurrentPopup => _currentPopup;

        private readonly Dictionary<int, IUIPresenter> _screens;
        private readonly Dictionary<int, IUIPresenter> _popups;
        private readonly Stack<IUIPresenter> _screenStack;

        //private IUIPresenter _initialScreen;
        private IUIPresenter _currentPopup;

        public UIManager()
        {
            _screens = new Dictionary<int, IUIPresenter>();
            _popups = new Dictionary<int, IUIPresenter>();

            _screenStack = new Stack<IUIPresenter>();
        }

        public void RegisterScreen(IUIPresenter screenPresenter)
        {
            if (!_screens.ContainsKey(screenPresenter.Id))
            {
                screenPresenter.Initialize();
                _screens.Add(screenPresenter.Id, screenPresenter);
            }
            else
            {
                throw new System.InvalidOperationException($"Screen with Id {screenPresenter.Id} already registered");
            }
        }

        public void RegisterPopup(IUIPresenter popupPresenter)
        {
            if (!_popups.ContainsKey(popupPresenter.Id))
            {
                popupPresenter.Initialize();
                _popups.Add(popupPresenter.Id, popupPresenter);
            }
            else
            {
                throw new System.InvalidOperationException($"Popup with Id {popupPresenter.Id} already registered");
            }
        }


        public void ShowNextScreen(int screenId)
        {
            if (_screens.TryGetValue(screenId, out IUIPresenter screen))
            {
                // Should be always true. The only way it may be false - if _initialScreen is null
                if (_screenStack.Count > 0)
                {
                    // Prevent "re-show" of the current screen
                    IUIPresenter curScreen = _screenStack.Peek();
                    if (curScreen.Id != screenId)
                    {
                        curScreen.Hide();
                        screen.Show();
                        _screenStack.Push(screen);
                    }
                }
                else
                {
                    screen.Show();
                    _screenStack.Push(screen);
                }

                CurrentScreen = screen;
            }
            else
            {
                throw new System.ArgumentException($"Screen with Id: {screenId} is not registered", nameof(screenId));
            }
        }

        public void BackToPreviousScreen()
        {
            if (_screenStack.Count > 1)
            {
                _screenStack.Pop().Hide();
                CurrentScreen = _screenStack.Peek();
                CurrentScreen.Show();
            }
        }

        public void ClearScreenHistory()
        {
            if (_screenStack.Count > 1)
            {
                // Clear all screens in the stack except the current one
                CurrentScreen = _screenStack.Peek();
                _screenStack.Clear();
                _screenStack.Push(CurrentScreen);
            }
        }

        public void ShowPopup(int popupId)
        {
            if (_popups.TryGetValue(popupId, out IUIPresenter popup))
            {
                // Disable input for current screen
                _screenStack.Peek().BlockInput();

                _currentPopup?.Hide();
                _currentPopup = popup;
                popup.Show();
            }
            else
            {
                throw new System.ArgumentException($"Popup with Id: {popupId} is not registered", nameof(popupId));
            }
        }

        public void ShowPopup<T>(int popupId, T payload)
        {
            if (_popups.TryGetValue(popupId, out IUIPresenter popup) && popup is IUIPresenter<T> popupWithData)
            {
                // Disable input for current screen
                _screenStack.Peek().BlockInput();

                _currentPopup?.Hide();
                _currentPopup = popupWithData;
                popupWithData.Show(payload);
            }
            else
            {
                throw new System.ArgumentException($"Popup with Id: {popupId} which accepts data of type '{typeof(T).Name}' is not registered");
            }
        }

        public void HidePopup()
        {
            if (_currentPopup != null)
            {
                // Enable input for current screen
                _screenStack.Peek().RestoreInput();

                _currentPopup.Hide();
                _currentPopup = null;
            }
        }
    }
}