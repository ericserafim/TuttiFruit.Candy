using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Implementations;
using TuttiFruit.Candy.Core.Interfaces;

namespace TuttiFruit.Candy.Core.Factories
{
    public class ConsumerFactory : IConsumerFactory
    {
        private readonly ChannelReader<object> _channelReader;
        private readonly Func<string, IMqSubscriber> _subscriberGetter;
        private readonly Func<IMessageHandler> _messageHandlerGetter;

        public ConsumerFactory(
            ChannelReader<object> channelReader, 
            Func<string, IMqSubscriber> subscriberGetter, 
            Func<IMessageHandler> messageHandlerGetter)
        {
            _channelReader = channelReader;
            _subscriberGetter = subscriberGetter;
            _messageHandlerGetter = messageHandlerGetter;
        }

        public IConsumer Create(string subscriberType)
        {
            return new Consumer(_channelReader, _subscriberGetter(subscriberType), _messageHandlerGetter());
        }
    }
}
