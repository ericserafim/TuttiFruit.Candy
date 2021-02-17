using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Entities;
using TuttiFruit.Candy.Core.Handlers;
using TuttiFruit.Candy.Core.Interfaces;

namespace TuttiFruit.Candy.RedisMq
{
    public class RedisSubscriber : IMqSubscriber
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly ISubscriber _subscriber;
        private readonly SubscriberSettings _settings;
        private string _queueName = string.Empty;

        public event AsyncEventHandler<SubscriberEventArgs> OnMessage;

        public RedisSubscriber(string connectionSttring, string channelName)
        {
            _connection = ConnectionMultiplexer.Connect(connectionSttring);
            _subscriber = _connection.GetSubscriber();

            Console.WriteLine($"{nameof(RedisSubscriber)} has been started.");
        }

        //public RedisSubscriber(IOptions<SubscriberSettings> settings)
        //{
        //    _settings = settings.Value;

        //    _connection = ConnectionMultiplexer.Connect(_settings.ConnectionString);
        //    _subscriber = _connection.GetSubscriber();

        //    Console.WriteLine($"{nameof(RedisSubscriber)} has been started.");
        //}

        public Task SendAckAsync(object message)
        {
            Console.WriteLine($"{nameof(RedisSubscriber)}.{nameof(SendAckAsync)} to {_queueName}");
            return Task.CompletedTask;
        }

        public void SetListenningTo(string queueName)
        {
            _queueName = queueName;

            _subscriber.Subscribe(
                new RedisChannel(_queueName, RedisChannel.PatternMode.Literal),
                handler: async (channel, value) =>
                {
                    if (OnMessage != null)
                        await OnMessage(this, new SubscriberEventArgs(value));
                    else
                        await Task.CompletedTask;
                });

            Console.WriteLine($"{nameof(RedisSubscriber)} listenning to {_queueName}.");
        }
        public void Dispose()
        {
            _connection?.Close();
            _subscriber?.UnsubscribeAll();

            Console.WriteLine($"{nameof(RedisSubscriber)} closed.");
        }
    }
}
