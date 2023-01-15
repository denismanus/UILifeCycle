using System;

namespace SimplePopups
{
    public interface INotifyClosed
    {
        event Action OnClosed;
    }
}