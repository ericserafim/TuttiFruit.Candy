using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;

namespace TuttiFruit.Candy.Rabbit.Interfaces
{
    public interface IChannelFactory
    {
        Channel<Message> CreateChannel();
    }
}
