using System.Threading;
using System.Threading.Tasks;

namespace TuttiFruit.Candy.Core.Interfaces
{
    public interface IConsumer
    {
        Task StartConsumeAsync(CancellationToken cancellationToken);
    }
}
