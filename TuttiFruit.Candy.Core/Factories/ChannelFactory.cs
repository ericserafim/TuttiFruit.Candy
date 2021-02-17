using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Threading.Channels;
using TuttiFruit.Candy.Core.Entities;
using TuttiFruit.Candy.Core.Interfaces;

namespace TuttiFruit.Candy.Core.Factories
{
    public class ChannelFactory : IChannelFactory
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
                SingleWriter = false,
                SingleReader = false,
                FullMode = BoundedChannelFullMode.Wait
            };

            return Channel.CreateBounded<Message>(options);
        }

        private Channel<Message> CreateUnboundedChannel()
        {
            var options = new UnboundedChannelOptions
            {
                SingleWriter = false,
                SingleReader = false
            };

            return Channel.CreateUnbounded<Message>(options);
        }
    }
}
