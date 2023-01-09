using UnityEngine;

namespace ConflictCards.UI.Core
{
    public abstract class UIViewBase : MonoBehaviour
    {
        public abstract int Id { get; }
        public abstract bool InputEnabled { get; set; }


        public abstract void Initialize();
        public abstract void Show();
        public abstract void Hide();
    }

    public abstract class UIViewBase<T> : UIViewBase
    {
        public abstract void Show(T data);
    }
}