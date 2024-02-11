using System;

namespace autumn_berries_mix.CallbackSystem.Signals
{
    public class TypedSignalSubscriber<TSubscription> : SignalSubscriber
        where TSubscription : Signal
    {
        private readonly Action<TSubscription> _subscription;

        public TypedSignalSubscriber(Action<TSubscription> subscription)
        {
            _subscription = subscription;
        }

        public override void InvokeSubscription(Signal nonTypedEvent)
        {
            _subscription?.Invoke(nonTypedEvent as TSubscription);
        }
    }
}
