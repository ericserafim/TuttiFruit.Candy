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
        private IList<IProducer> _producers = new List<IProducer>();
        private IList<IConsumer> _consumers = new List<IConsumer>();

        public BackgroundWorkerService(
            IOptions<TuttiFruitCandySettings> settings, 
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
            var consumersTasks = _consumers.Select(c => c.StartConsumeAsync(cancellationToken));

            await Task.WhenAll(consumersTasks);
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
                _producers.Add(_producerFactory.Create(producer, cancellationToken));

                for (int i = 0; i < producer.NumberOfConsumers; i++)
                {
                    _consumers.Add(_consumerFactory.Create(producer.SubscriberType));
                }
            }
        }
    }
}
