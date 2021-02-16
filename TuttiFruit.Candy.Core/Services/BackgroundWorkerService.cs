using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Entities;
using TuttiFruit.Candy.Core.Implementations;
using TuttiFruit.Candy.Core.Interfaces;

namespace TuttiFruit.Candy.Core.Services
{
    public class BackgroundWorkerService : BackgroundService
    {
        private readonly TuttiFruitCandySettings _settings;
        private readonly IProducerFactory _producerFactory;
        private readonly IConsumerFactory _consumerFactory;
        private IEnumerable<IProducer> _producers;
        private IEnumerable<IConsumer> _consumers;

        public BackgroundWorkerService(
            IOptionsSnapshot<TuttiFruitCandySettings> settings, 
            IProducerFactory producerFactory, 
            IConsumerFactory consumerFactory)
        {
            _settings = settings.Value;
            _producerFactory = producerFactory;
            _consumerFactory = consumerFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Initialize(cancellationToken);
            await Task.WhenAll(_consumers.Select(c => c.StartConsumeAsync(cancellationToken)));
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var producer in _producers)
            {
                producer.Stop();
            }

            return base.StopAsync(cancellationToken);
        }

        private void Initialize(CancellationToken cancellationToken)
        {
            foreach (var producer in _settings.Producers)
            {
                _producers.Append(_producerFactory.Create(producer, cancellationToken));

                for (int i = 0; i < producer.NumberOfConsumers; i++)
                {
                    _consumers.Append(_consumerFactory.Create(producer.SubscriberType));
                }
            }
        }
    }
}
