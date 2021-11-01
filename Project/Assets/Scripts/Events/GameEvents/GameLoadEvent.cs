using System;
using EventBus.Composite.Presentation.Events;

public sealed class GameLoadEvent : CompositePresentationEvent<GameLoadEventArg>
{
}

public sealed class GameLoadEventArg : EventArgs
{
    public readonly bool Started;
    public GameLoadEventArg (bool started)
    {
        Started = started;
    }
}