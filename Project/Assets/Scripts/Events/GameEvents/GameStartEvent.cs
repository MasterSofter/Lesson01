using System;
using EventBus.Composite.Presentation.Events;

public sealed class GameStartEvent : CompositePresentationEvent<GameStartEventArg>
{
}

public sealed class GameStartEventArg : EventArgs
{
    public readonly bool Started;
    public GameStartEventArg(bool started)
    {
        Started = started;
    }
}