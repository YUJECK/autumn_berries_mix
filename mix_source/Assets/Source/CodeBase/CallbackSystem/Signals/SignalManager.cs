using System;
using System.Collections.Generic;
using autumn_berries_mix.Scenes;

namespace autumn_berries_mix.CallbackSystem.Signals
{
    public static class SignalManager
    {
        private static readonly Dictionary<Type, List<KeyValuePair<SignalSubscription, SignalSubscriber>>> 
            Subscriptions = new();

        static SignalManager()
        {
            SceneSwitcher.OnSceneStartedLoading += ClearSubscriptions;
        }

        private static void ClearSubscriptions(Scene arg1)
        {
            List<SignalSubscription> toRemove = new();
            
            foreach (var subscriptionContainerPair in Subscriptions)
            {
                foreach (var subscriptionPair in subscriptionContainerPair.Value)
                {
                    if (subscriptionPair.Key.ClearOnLoad)
                    {
                        toRemove.Add(subscriptionPair.Key);
                    }
                }
            }

            for (int i = 0; i < toRemove.Count; i++)
            {
                UnsubscribeOnSignal(toRemove[i]);
            }
        }

        public static void PushSignal<TSignal>(TSignal signal)
            where TSignal : Signal
        {
            if (!Subscriptions.ContainsKey(typeof(TSignal))) 
                return;
        
            foreach (var subscriptionPair in Subscriptions[typeof(TSignal)])
            {
                subscriptionPair.Value.InvokeSubscription(signal);
            }
        }

        public static SignalSubscription SubscribeOnSignal<TSignal>(Action<TSignal> action, bool clearOnLoad = true)
            where TSignal : Signal
        {
            var signalType = typeof(TSignal);
            var subscriber = new TypedSignalSubscriber<TSignal>(action);
            var subscription = new SignalSubscription(subscriber, clearOnLoad);

            if (Subscriptions.TryAdd(signalType, new List<KeyValuePair<SignalSubscription, SignalSubscriber>>() { new (subscription, subscriber) }))
                return subscription;

            Subscriptions[signalType].Add(new KeyValuePair<SignalSubscription, SignalSubscriber>(subscription, subscriber));

            return subscription;
        }
    
        public static void UnsubscribeOnSignal(SignalSubscription subscription)
        {
            Subscriptions
                [subscription.SubscriptionType]
                .Remove(Subscriptions[subscription.SubscriptionType]
                .Find((element) => element.Key == subscription));
        }
    }
}