using System;

namespace autumn_berries_mix.CallbackSystem.Signals
{
    public sealed class SignalSubscription
    {
        public Type SubscriptionType => _subscriber.SubscriptionType;
        private readonly SignalSubscriber _subscriber;

        public SignalSubscription(SignalSubscriber subscriber)
        {
            _subscriber = subscriber;
        }
    }
}