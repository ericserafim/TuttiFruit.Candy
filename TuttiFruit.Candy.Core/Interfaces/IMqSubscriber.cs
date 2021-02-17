using System;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Entities;
using TuttiFruit.Candy.Core.Handlers;

namespace TuttiFruit.Candy.Core.Interfaces
{
    public interface IMqSubscriber : IDisposable
    {
        event AsyncEventHandler<SubscriberEventArgs> OnMessage;

        void SetListenningTo(string queueName);

        Task SendAckAsync(object message);
    }
}
