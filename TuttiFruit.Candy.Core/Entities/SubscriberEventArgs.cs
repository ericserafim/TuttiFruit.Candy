using System;

namespace TuttiFruit.Candy.Core.Entities
{
    public sealed class SubscriberEventArgs : EventArgs
    {
        public object Message { get; }

        public SubscriberEventArgs(object message)
        {
            Message = message;
        }

    }
}
