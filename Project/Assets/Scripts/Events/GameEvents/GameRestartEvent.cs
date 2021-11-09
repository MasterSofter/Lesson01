using System;
using EventBus.Composite.Presentation.Events;

public sealed class GameRestartEvent : CompositePresentationEvent<GameRestartEventArg>
{
}

public sealed class GameRestartEventArg : EventArgs
{
    public readonly bool Started;
    public GameRestartEventArg(bool started)
    {
        Started = started;
    }
}