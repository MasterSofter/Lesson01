using System;
using EventBus.Composite.Presentation.Events;
public sealed class GameResumeEvent : CompositePresentationEvent<GameResumeEventArg>
{
}

public sealed class GameResumeEventArg : EventArgs
{
    public readonly bool Started;
    public GameResumeEventArg(bool started)
    {
        Started = started;
    }
}

