using System;

public class CustomRepeatEvent<T>
{
    private Action<T> actionEvent;
    public void Invoke(T value)
    {
        if (actionEvent == null) return;
        actionEvent(value);
    }

    public void SubscribeMethod(Action<T> inAction)
    {
        actionEvent += inAction;
    }
    public void RemoveOneShotMethod(Action<T> inAction)
    {
        actionEvent -= inAction;
    }
}

public class CustomRepeatEvent
{
    private Action actionEvent;
    public void Invoke()
    {
        if (actionEvent == null) return;
        actionEvent();
    }
    
    public void SubscribeMethod(Action inAction)
    {
        actionEvent += inAction;
    }
    
    public void RemoveOneShotMethod(Action inAction)
    {
        actionEvent -= inAction;
    }
}

public class CustomUniqueEvent
{
    private Action actionEvent;
    public void Invoke()
    {
        if (actionEvent == null) return;
        actionEvent();
        actionEvent = null;
    }
    
    public void SubscribeMethod(Action inAction)
    {
        actionEvent += inAction;
    }
    
    public void RemoveOneShotMethod(Action inAction)
    {
        actionEvent -= inAction;
    }
}

public class CustomUniqueEvent<T>
{
    private Action<T> actionEvent;
    public void Invoke(T value)
    {
        if (actionEvent == null) return;
        actionEvent(value);
    }
    public void SubscribeMethod(Action<T> inAction)
    {
        actionEvent += inAction;
    }
}