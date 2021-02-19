using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Interfaces;

namespace TuttiFruit.Candy.Rabbit.Services
{
    public class BackgroundWorkerService : BackgroundService
    {
        private readonly IMQSubscriber _mQSubscriber;
        private readonly IProducer _producer;
        private readonly IConsumerFactory _consumerFactory;

        public BackgroundWorkerService(IMQSubscriber mQSubscriber, IProducer producer, IConsumerFactory consumerFactory)
        {
            _mQSubscriber = mQSubscriber;
            _producer = producer;
            _consumerFactory = consumerFactory;

            _mQSubscriber.OnMessage += _mQSubscriber_OnMessage;
        }

        private ValueTask _mQSubscriber_OnMessage(object sender, SubscriberEventArgs args)
        {
            throw new NotImplementedException();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
