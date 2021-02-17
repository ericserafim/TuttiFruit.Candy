using System;
using System.Threading;
using System.Threading.Channels;
using TuttiFruit.Candy.Core.Entities;
using TuttiFruit.Candy.Core.Implementations;
using TuttiFruit.Candy.Core.Interfaces;

namespace TuttiFruit.Candy.Core.Factories
{
    public class ProducerFactory : IProducerFactory
    {
        private readonly ChannelWriter<Message> _channelWriter;
        private readonly Func<string, IMqSubscriber> _subscriberGetter;

        public ProducerFactory(ChannelWriter<Message> channelWriter, Func<string, IMqSubscriber> subscriberGetter)
        {
            _channelWriter = channelWriter;
            _subscriberGetter = subscriberGetter;
        }

        public IProducer Create(ProducerSettings producer, string subscriberType, CancellationToken cancellationToken)
        {
            return new Producer(producer, _channelWriter, _subscriberGetter(subscriberType), cancellationToken);
        }
    }
}
