using System.Threading;
using System.Threading.Tasks;

namespace TuttiFruit.Candy.Rabbit.Interfaces
{
  public interface IConsumer
  {
    Task StartConsumeAsync(CancellationToken cancellationToken);
  }
}
