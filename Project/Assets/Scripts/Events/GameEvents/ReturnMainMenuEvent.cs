using System;
using EventBus.Composite.Presentation.Events;

public sealed class ReturnMainMenuEvent : CompositePresentationEvent<ReturnMainMenuEventArg>
{
}

public sealed class ReturnMainMenuEventArg : EventArgs
{
    public readonly bool Started;
    public ReturnMainMenuEventArg(bool started)
    {
        Started = started;
    }
}

