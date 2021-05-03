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

    public async Task PublishMessageAsync(Message message, CancellationToken cancellationToken)
    {
      await _channelWriter.WriteAsync(message, cancellationToken);
    }

    public void Complete()
    {
      _channelWriter.TryComplete();
      _logger.LogDebug($"{nameof(Producer)} has been closed.");
    }
  }
}
