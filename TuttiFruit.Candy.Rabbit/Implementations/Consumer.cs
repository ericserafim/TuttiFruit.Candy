using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Interfaces;

namespace TuttiFruit.Candy.Rabbit.Implementations
{
    public sealed class Consumer : IConsumer
    {
        public Task StartConsumeAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
