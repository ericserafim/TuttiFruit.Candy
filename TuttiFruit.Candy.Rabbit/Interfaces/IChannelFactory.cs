using System.Threading.Channels;
using TuttiFruit.Candy.Rabbit.Entities;

namespace TuttiFruit.Candy.Rabbit.Interfaces
{
  public interface IChannelFactory
  {
    Channel<Message> CreateChannel();
  }
}
