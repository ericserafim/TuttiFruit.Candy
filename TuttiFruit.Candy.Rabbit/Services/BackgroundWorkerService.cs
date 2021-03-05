using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Interfaces;

namespace TuttiFruit.Candy.Rabbit.Services
{
    public class BackgroundWorkerService : BackgroundService
    {
        private readonly IMQSubscriber _subscriber;
        private readonly IProducer _producer;
        private readonly IConsumerFactory _consumerFactory;
        private CancellationToken _stoppingtoken = default;

        public BackgroundWorkerService(IMQSubscriber subscriber, IProducer producer, IConsumerFactory consumerFactory)
        {
            _subscriber = subscriber;
            _producer = producer;
            _consumerFactory = consumerFactory;

            _subscriber.OnMessage += OnMessage;
            _subscriber.OnConnectionError += OnConnectionError;
        }

        private async ValueTask OnConnectionError(object sender, ConnectionEventArgs args)
        {
            _subscriber.Dispose();
            _producer.Stop();

            await Task.CompletedTask;
        }

        private async ValueTask OnMessage(object sender, SubscriberEventArgs args)
        {
            await _producer.PublishMessageAsync(args.Message, _stoppingtoken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _stoppingtoken = stoppingToken;
            var consumerTasks = _consumerFactory.CreateConsumers().Select(c => c.StartConsumeAsync(stoppingToken));
            
            await _subscriber.StartAsync(stoppingToken);
            await Task.WhenAll(consumerTasks);
        }
    }
}
