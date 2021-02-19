using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Interfaces;

namespace TuttiFruit.Candy.Rabbit.Implementations
{
    public class Producer : IProducer
    {
        private readonly ChannelWriter<Message> _channelWriter;
        private readonly ILogger<Producer> _logger;

        public Producer(ChannelWriter<Message> channelWriter, ILogger<Producer> logger)
        {
            _channelWriter = channelWriter;
            _logger = logger;
        }

        public Task PublishMessageAsync(Message message, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            _channelWriter.Complete();
            _logger.LogDebug($"{nameof(Producer)} has been closed.");
        }
    }
}
