using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;

namespace TuttiFruit.Candy.Rabbit.Interfaces
{
    public interface IProducer
    {
        Task PublishMessageAsync(Message message, CancellationToken cancellationToken);

        void Stop();
    }
}
