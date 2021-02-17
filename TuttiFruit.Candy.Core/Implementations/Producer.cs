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
        private readonly string _name;
        private readonly ChannelWriter<object> _channelWriter;
        private readonly IMqSubscriber _subscriber;
        private readonly CancellationToken _cancellationToken;

        public Producer(string name, ChannelWriter<object> channelWriter, IMqSubscriber subscriber, CancellationToken cancellationToken)
        {
            _name = name;
            _channelWriter = channelWriter;
            _subscriber = subscriber;
            _cancellationToken = cancellationToken;
            _subscriber.OnMessage += OnMessageAsync;

            Console.WriteLine($"'{nameof(Producer)}.{_name}' has been started.");
        }

        public void Stop()
        {
            _channelWriter.Complete();
            Console.WriteLine($"{nameof(Producer)}.{_name} closed.");
        }

        private async ValueTask OnMessageAsync(object sender, SubscriberEventArgs args)
        {           
            await _channelWriter.WriteAsync(args.Message, _cancellationToken);
        }
    }
}
