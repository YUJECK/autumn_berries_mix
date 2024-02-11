using System;

public class TypedEventSubscriber<TEvent> : EventSubscriber
    where TEvent : Event
{
    private readonly Action<TEvent> _subscription;
    
    public TypedEventSubscriber(Action<TEvent> subscription)
    {
        SubscriptionType = typeof(TEvent);
        _subscription = subscription;
    }

    public override void InvokeSubscription(Event nonTypedEvent)
    {
        _subscription?.Invoke(nonTypedEvent as TEvent);
    }
}