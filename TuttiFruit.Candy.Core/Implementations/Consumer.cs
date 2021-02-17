using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Interfaces;

namespace TuttiFruit.Candy.Core.Implementations
{
    public class Consumer : IConsumer
    {
        private readonly ChannelReader<object> _channelReader;
        private readonly IMqSubscriber _subscriber;
        private readonly IMessageHandler _messageHandler;
        private readonly Guid _id = Guid.NewGuid();

        public Consumer(
            ChannelReader<object> channelReader, 
            IMqSubscriber subscriber,
            IMessageHandler messageHandler)
        {
            _channelReader = channelReader;
            _subscriber = subscriber;
            _messageHandler = messageHandler;
        }

        public async Task StartConsumeAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(Consumer)}:{_id} has been started.");

            await foreach (var message in _channelReader.ReadAllAsync(cancellationToken))
            {
                Console.WriteLine($"{nameof(Consumer)}:{_id} => '{message}' received.");

                await _messageHandler.ProcessMessageAsync(message);
                await _subscriber.SendAckAsync(message);
            }
        }
    }
}
