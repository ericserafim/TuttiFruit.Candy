using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Implementations;
using TuttiFruit.Candy.Rabbit.Interfaces;

namespace TuttiFruit.Candy.Rabbit.Factories
{
    public sealed class ConsumerFactory : IConsumerFactory
    {
        private readonly TuttiFruitCandySettings _settings;
        private readonly ChannelReader<Message> _channelReader;
        private readonly IMQSubscriber _mQSubscriber;
        private readonly Func<IMessageHandler> _messageHandlerGetter;

        public ConsumerFactory(
            IOptions<TuttiFruitCandySettings> settings,
            ChannelReader<Message> channelReader, 
            IMQSubscriber mQSubscriber, 
            Func<IMessageHandler> messageHandlerGetter)
        {
            _settings = settings.Value;
            _channelReader = channelReader;
            _mQSubscriber = mQSubscriber;
            _messageHandlerGetter = messageHandlerGetter;
        }

        public IEnumerable<IConsumer> CreateConsumers()
        {
            for (int i = 0; i < _settings.NumberOfConsumers; i++)
            {
                yield return new Consumer();
            }
        }
    }
}
