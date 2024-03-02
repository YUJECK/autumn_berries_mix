using System;

namespace autumn_berries_mix.CallbackSystem.Signals
{
    public sealed class SignalSubscription
    {
        public Type SubscriptionType => _subscriber.SubscriptionType;
        private readonly SignalSubscriber _subscriber;
        public readonly bool ClearOnLoad;

        public SignalSubscription(SignalSubscriber subscriber, bool clearOnLoad)
        {
            _subscriber = subscriber;
            ClearOnLoad = clearOnLoad;
        }
    }
}