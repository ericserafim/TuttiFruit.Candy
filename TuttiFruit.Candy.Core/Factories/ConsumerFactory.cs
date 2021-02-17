using System;
using System.Threading.Channels;
using TuttiFruit.Candy.Core.Entities;
using TuttiFruit.Candy.Core.Implementations;
using TuttiFruit.Candy.Core.Interfaces;

namespace TuttiFruit.Candy.Core.Factories
{
    public class ConsumerFactory : IConsumerFactory
    {
        private readonly ChannelReader<Message> _channelReader;        
        private readonly Func<IMessageHandler> _messageHandlerGetter;

        public ConsumerFactory(
            ChannelReader<Message> channelReader,            
            Func<IMessageHandler> messageHandlerGetter)
        {
            _channelReader = channelReader;            
            _messageHandlerGetter = messageHandlerGetter;
        }

        public IConsumer Create()
        {
            return new Consumer(_channelReader, _messageHandlerGetter());            
        }
    }
}
