using System.Threading;
using TuttiFruit.Candy.Core.Entities;

namespace TuttiFruit.Candy.Core.Interfaces
{
    public interface IProducerFactory
    {
        IProducer Create(ProducerSettings producer, string subscriberType, CancellationToken cancellationToken);
    }
}
