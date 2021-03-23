using Microsoft.Extensions.Options;
using RabbitMQ.Client;
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
        private readonly RabbitSettings _rabbitSettings;

        private IConnection _connection;

        public event AsyncEventHandler<SubscriberEventArgs> OnMessage;

        public event AsyncEventHandler<ConnectionEventArgs> OnConnectionError;
        

        public RabbitSubscriber(IOptions<RabbitSettings> rabbitSettings)
        {
            _rabbitSettings = rabbitSettings.Value;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var factory = new ConnectionFactory();
                
                factory.UserName = _rabbitSettings.UserName;
                factory.Password = _rabbitSettings.Password;
                factory.VirtualHost = _rabbitSettings.VirtualHost;
                factory.HostName = _rabbitSettings.HostName;
                factory.Port = _rabbitSettings.Port;                

                _connection = factory.CreateConnection();

                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                OnConnectionError?.Invoke(this, new ConnectionEventArgs(e));
            }
        }

        public Task SendAckAsync(Message message)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
