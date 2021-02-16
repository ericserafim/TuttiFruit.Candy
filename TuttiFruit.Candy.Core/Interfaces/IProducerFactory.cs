using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Entities;

namespace TuttiFruit.Candy.Core.Interfaces
{
    public interface IProducerFactory
    {
        IProducer Create(ProducerSettings producer, CancellationToken cancellationToken);
    }
}
