using System;

public interface INotifyClosed
{
    public event Action OnClosed;
}