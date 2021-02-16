using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TuttiFruit.Candy.Core.Interfaces
{
    public interface IChannelFactory
    {
        Channel<object> CreateChannel();
    }
}
