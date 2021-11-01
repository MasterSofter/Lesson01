using System;
using EventBus.Composite.Presentation.Events;

public sealed class MenuExitEvent : CompositePresentationEvent<MenuExitEventArg>
{
}

public sealed class MenuExitEventArg : EventArgs
{
    public readonly bool Started;
    public MenuExitEventArg(bool started)
    {
        Started = started;
    }
}