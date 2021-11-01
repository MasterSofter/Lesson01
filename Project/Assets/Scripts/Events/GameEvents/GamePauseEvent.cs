using System;
using EventBus.Composite.Presentation.Events;

public sealed class GamePauseEvent : CompositePresentationEvent<GamePauseEventArg>
{
}

public sealed class GamePauseEventArg : EventArgs
{
    public readonly bool Started;
    public GamePauseEventArg(bool started)
    {
        Started = started;
    }
}

