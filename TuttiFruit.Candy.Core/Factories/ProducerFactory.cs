using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Entities;
using TuttiFruit.Candy.Core.Implementations;
using TuttiFruit.Candy.Core.Interfaces;

namespace TuttiFruit.Candy.Core.Factories
{
    public class ProducerFactory : IProducerFactory
    {
        private readonly ChannelWriter<object> _channelWriter;
        private readonly Func<string, IMqSubscriber> _subscriberGetter;

        public ProducerFactory(ChannelWriter<object> channelWriter, Func<string, IMqSubscriber> subscriberGetter)
        {
            _channelWriter = channelWriter;
            _subscriberGetter = subscriberGetter;
        }

        public IProducer Create(ProducerSettings producer, CancellationToken cancellationToken)
        {
            return new Producer(producer.Name, _channelWriter, _subscriberGetter(producer.SubscriberType), cancellationToken);            
        }
    }
}
