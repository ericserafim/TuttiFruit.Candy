using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuttiFruit.Candy.Rabbit.Interfaces
{
    public interface IConsumerFactory
    {
        IEnumerable<IConsumer> CreateConsumers();
    }
}
