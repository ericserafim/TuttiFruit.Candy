using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Threading.Channels;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Interfaces;

namespace TuttiFruit.Candy.Rabbit.Factories
{
  public sealed class ChannelFactory : IChannelFactory
  {
    private readonly ChannelSettings _channelSettings;

    public ChannelFactory(IOptions<ChannelSettings> channelSettings)
    {
      _channelSettings = channelSettings.Value;
    }

    public Channel<Message> CreateChannel()
    {
      return _channelSettings.Mode switch
      {
        ChannelMode.Bounded => this.CreateBoundedChannel(),
        ChannelMode.Unbounded => this.CreateUnboundedChannel(),
        _ => throw new InvalidEnumArgumentException("Invalid Channel Mode: " + _channelSettings.Mode)
      };
    }

    private Channel<Message> CreateBoundedChannel()
    {
      var options = new BoundedChannelOptions(_channelSettings.Capacity)
      {
        SingleWriter = true,
        FullMode = BoundedChannelFullMode.Wait
      };

      return Channel.CreateBounded<Message>(options);
    }

    private Channel<Message> CreateUnboundedChannel()
    {
      var options = new UnboundedChannelOptions
      {
        SingleWriter = true
      };

      return Channel.CreateUnbounded<Message>(options);
    }
  }
}
