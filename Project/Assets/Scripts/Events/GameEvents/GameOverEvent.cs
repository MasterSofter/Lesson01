using System;
using EventBus.Composite.Presentation.Events;

public sealed class GameOverEvent : CompositePresentationEvent<GameOverEventArg>
{
}

public sealed class GameOverEventArg : EventArgs
{
    public readonly bool Started;
    public GameOverEventArg(bool started)
    {
        Started = started;
    }
}
