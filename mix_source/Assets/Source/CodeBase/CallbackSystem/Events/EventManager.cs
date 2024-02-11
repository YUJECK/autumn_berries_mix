using System;
using System.Collections.Generic;
using Zenject;

public sealed class EventManager : ITickable
{
    private readonly Dictionary<Type, Event> _currentEvents = new();

    private readonly Dictionary<Type, List<EventSubscriber>> _subscribers = new();

    public void Tick()
    {
        foreach (var eventPair in _currentEvents)
            eventPair.Value.Tick();
    }

    public void PushEvent<TEvent>(TEvent eventToPush)
        where TEvent : Event
    {
        if(_currentEvents.ContainsKey(typeof(TEvent)))
            return;
        
        _currentEvents.Add(typeof(TEvent), eventToPush);
        eventToPush.OnEventCompleted += OnEventCompleted;
        
        eventToPush.OnPushed();
        InvokeSubscribers<TEvent>();
    }

    public EventSubscriber SubscribeOnEvent<TEvent>(Action<TEvent> action)
        where TEvent : Event
    {
        var eventType = typeof(TEvent);
        var subscriber = new TypedEventSubscriber<TEvent>(action);

        if (_subscribers.TryAdd(eventType, new List<EventSubscriber>() { subscriber }))
            return subscriber;

        _subscribers[eventType].Add(subscriber);

        return subscriber;
    }
    
    public void UnsubscribeOnEvent(EventSubscriber eventSubscriberAa)
    {
        _subscribers.Remove(eventSubscriberAa.SubscriptionType);
    }

    private void InvokeSubscribers<TEvent>()
        where TEvent : Event
    {
        if (!_subscribers.ContainsKey(typeof(TEvent))) 
            return;

        var subscriberEvent = _currentEvents[typeof(TEvent)];

        foreach (var subscriber in _subscribers[typeof(TEvent)])
        {
            subscriber.InvokeSubscription(subscriberEvent);
        }
    }

    private void OnEventCompleted(Event completedEvent)
    {
        _currentEvents.Remove(completedEvent.GetType());
        
        completedEvent.OnEventCompleted -= OnEventCompleted;
        completedEvent.OnCompleted();
    }
}