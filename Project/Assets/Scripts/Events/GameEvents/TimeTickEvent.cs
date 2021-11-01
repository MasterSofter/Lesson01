using System;
using EventBus.Composite.Presentation.Events;

public sealed class TimeTickEvent : CompositePresentationEvent<TimeTickEventArg>
{
}

public sealed class TimeTickEventArg : EventArgs
{
    public readonly float DeltaTime;
    public TimeTickEventArg(float deltaTime)
    {
        DeltaTime = deltaTime;
    }
}