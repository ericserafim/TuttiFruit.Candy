using System.Threading.Channels;
using TuttiFruit.Candy.Core.Entities;

namespace TuttiFruit.Candy.Core.Interfaces
{
    public interface IChannelFactory
    {
        Channel<Message> CreateChannel();
    }
}
