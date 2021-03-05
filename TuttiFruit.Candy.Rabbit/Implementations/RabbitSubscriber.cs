using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Handlers;
using TuttiFruit.Candy.Rabbit.Interfaces;

namespace TuttiFruit.Candy.Rabbit.Implementations
{
    public class RabbitSubscriber : IMQSubscriber
    {
        private readonly IOptions<RabbitSettings> _rabbitSettings;

        public event AsyncEventHandler<SubscriberEventArgs> OnMessage;

        public event AsyncEventHandler<ConnectionEventArgs> OnConnectionError;

        public RabbitSubscriber(IOptions<RabbitSettings> rabbitSettings)
        {
            _rabbitSettings = rabbitSettings;
        }

        public Task SendAckAsync(Message message)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
