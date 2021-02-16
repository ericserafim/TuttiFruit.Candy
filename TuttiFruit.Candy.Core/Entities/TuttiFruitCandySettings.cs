using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuttiFruit.Candy.Core.Entities
{
    public class TuttiFruitCandySettings
    {
        public ChannelSettings ChannelSettings { get; set; }

        public IEnumerable<ProducerSettings> Producers { get; set; }
    }
}
