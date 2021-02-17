using System.Collections.Generic;

namespace TuttiFruit.Candy.Core.Entities
{
    public sealed class TuttiFruitCandySettings
    {
        public int NumberOfConsumers { get; set; }

        public ChannelSettings ChannelSettings { get; set; }

        public IEnumerable<ProducerSettings> Producers { get; set; }
        
        public IEnumerable<SubscriberSettings> Subscribers { get; set; }
    }
}
