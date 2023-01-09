namespace ConflictCards.UI.Core
{
    public interface IUIPresenter
    {
        int Id { get; }
        void Initialize();
        void Show();
        void Hide();
        void BlockInput();
        void RestoreInput();
    }

    public interface IUIPresenter<in T> : IUIPresenter
    {
        void Show(T data);
    }
}