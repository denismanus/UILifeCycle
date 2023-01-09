using System;

public class ConfirmationPopup : PopupBase
{
    public ConfirmationPopup SetPositiveCallback(Action handleYes)
    {
        return this;
    }

    public ConfirmationPopup SetNegativeCallback(Action handleNo)
    {
        return this;
    }

    public ConfirmationPopup SetInitData(object data)
    {
        return this;
    }
}