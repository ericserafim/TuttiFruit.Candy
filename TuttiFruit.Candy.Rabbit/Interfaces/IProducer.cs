using System.Threading;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;

namespace TuttiFruit.Candy.Rabbit.Interfaces
{
  public interface IProducer
  {
    Task PublishMessageAsync(Message message, CancellationToken cancellationToken);

    void Complete();
  }
}
