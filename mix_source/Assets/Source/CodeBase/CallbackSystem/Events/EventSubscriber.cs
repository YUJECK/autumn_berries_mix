using System;

public abstract class EventSubscriber
{
    public Type SubscriptionType { get; protected set; }

    public abstract void InvokeSubscription(Event nonTypedEvent);
}