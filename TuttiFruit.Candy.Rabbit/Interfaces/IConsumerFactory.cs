using System.Collections.Generic;

namespace TuttiFruit.Candy.Rabbit.Interfaces
{
  public interface IConsumerFactory
  {
    IEnumerable<IConsumer> CreateConsumers();
  }
}
