using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Entities;
using TuttiFruit.Candy.Core.Interfaces;

namespace TuttiFruit.Candy.Core.Implementations
{
    public class Producer : IProducer
    {
        private readonly string _logIdentifier;
        private readonly ProducerSettings _settings;
        private readonly ChannelWriter<Message> _channelWriter;
        private readonly IMqSubscriber _subscriber;
        private readonly CancellationToken _cancellationToken;

        public Producer(ProducerSettings settings, ChannelWriter<Message> channelWriter, IMqSubscriber subscriber, CancellationToken cancellationToken)
        {
            _settings = settings;
            _channelWriter = channelWriter;
            _subscriber = subscriber;
            _cancellationToken = cancellationToken;
            _subscriber.OnMessage += OnMessageAsync;

            _logIdentifier = $"{nameof(Producer)}.{_settings.Name}";
            Console.WriteLine($"'{_logIdentifier}' has been started.");

            SetQueueNameToSubscriber();
        }

        public void SetQueueNameToSubscriber()
        {
            _subscriber.SetListenningTo(_settings.QueueName);            
        }

        public void Stop()
        {
            _channelWriter.TryComplete();
            Console.WriteLine($"{_logIdentifier} closed.");
        }

        private async ValueTask OnMessageAsync(object sender, SubscriberEventArgs args)
        {
            var message = new Message(_subscriber.GetType().AssemblyQualifiedName, args.Message);
            await _channelWriter.WriteAsync(message, _cancellationToken);
        }
    }
}
