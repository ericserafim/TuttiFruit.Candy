using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
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

        public Channel<object> CreateChannel()
        {
            return _channelSettings.Mode switch
            {
                ChannelMode.Bounded => this.CreateBoundedChannel(),
                ChannelMode.Unbounded => this.CreateUnboundedChannel(),
                _ => throw new InvalidEnumArgumentException("Invalid Channel Mode: " + _channelSettings.Mode)
            };
        }

        private Channel<object> CreateBoundedChannel()
        {
            var options = new BoundedChannelOptions(_channelSettings.Capacity)
            {
                SingleWriter = false,
                SingleReader = false,
                FullMode = BoundedChannelFullMode.Wait
            };

            return Channel.CreateBounded<object>(options);
        }

        private Channel<object> CreateUnboundedChannel()
        {
            var options = new UnboundedChannelOptions
            {
                SingleWriter = false,
                SingleReader = false
            };

            return Channel.CreateUnbounded<object>(options);
        }
    }
}
