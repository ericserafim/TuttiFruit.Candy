using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public event AsyncEventHandler<SubscriberEventArgs> OnMessage;

        public RedisSubscriber(string connectionSttring, string channelName)
        {
            _connection = ConnectionMultiplexer.Connect(connectionSttring);
            _subscriber = _connection.GetSubscriber();

            _subscriber.Subscribe(
                new RedisChannel(channelName, RedisChannel.PatternMode.Literal),
                handler: async (channel, value) =>
                {
                    if (OnMessage != null)
                        await OnMessage(this, new SubscriberEventArgs(value));
                    else
                        await Task.CompletedTask;
                });

            Console.WriteLine($"{nameof(RedisSubscriber)} has been started.");
        }

        public Task SendAckAsync(object message)
        {
            Console.WriteLine($"{nameof(RedisSubscriber)} {nameof(SendAckAsync)}");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _connection?.Close();
            _subscriber?.UnsubscribeAll();

            Console.WriteLine($"{nameof(RedisSubscriber)} closed.");
        }
    }
}
