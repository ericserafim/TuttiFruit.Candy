using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Handlers;

namespace TuttiFruit.Candy.Rabbit.Interfaces
{
    public interface IMQSubscriber : IDisposable
    {
        event AsyncEventHandler<SubscriberEventArgs> OnMessage;

        event AsyncEventHandler<ConnectionEventArgs> OnConnectionError;

        Task SendAckAsync(Message message);

        Task StartAsync(CancellationToken cancellationToken);
    }
}
