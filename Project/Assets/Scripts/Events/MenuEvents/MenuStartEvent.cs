using System;
using EventBus.Composite.Presentation.Events;

public sealed class MenuStartEvent : CompositePresentationEvent<MenuStartEventArg>
{
}

public sealed class MenuStartEventArg : EventArgs
{
    public readonly bool Started;
    public MenuStartEventArg(bool started)
    {
        Started = started;
    }
}