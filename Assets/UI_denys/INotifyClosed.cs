using System;

public interface INotifyClosed
{
    event Action OnClosed;
}