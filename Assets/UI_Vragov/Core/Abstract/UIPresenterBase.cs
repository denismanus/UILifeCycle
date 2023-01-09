namespace ConflictCards.UI.Core
{
    public abstract class UIPresenterBase<T> : UIPresenterBase, IUIPresenter<T>
    {
        protected UIPresenterBase(UIViewBase<T> view, UIManager uiManager)
            : base(view, uiManager)
        {
        }

        public abstract void Show(T data);

        protected UIViewBase<T> GetView()
        {
            return _view as UIViewBase<T>;
        }
    }

    public abstract class UIPresenterBase : IUIPresenter
    {
        public abstract int Id { get; }

        protected readonly UIViewBase _view;
        protected readonly UIManager _uiManager;

        protected UIPresenterBase(UIViewBase view, UIManager uiManager)
        {
            _view = view;
            _uiManager = uiManager;
        }

        public abstract void Initialize();

        public virtual void Show()
        {
            _view.Show();
        }

        public virtual void Hide()
        {
            _view.Hide();
        }

        public virtual void BlockInput()
        {
            // TODO: Change property to multi-value logic with priorities
            _view.InputEnabled = false;
        }

        public virtual void RestoreInput()
        {
            _view.InputEnabled = true;
        }
    }
}