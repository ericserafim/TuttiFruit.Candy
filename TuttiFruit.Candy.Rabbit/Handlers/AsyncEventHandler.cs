using System;
using System.Threading.Tasks;

namespace TuttiFruit.Candy.Rabbit.Handlers
{
  public delegate Task AsyncEventHandler<in TEvent>(object sender, TEvent @event) where TEvent : EventArgs;
}
