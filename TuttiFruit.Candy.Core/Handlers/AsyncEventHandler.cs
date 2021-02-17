using System;
using System.Threading.Tasks;

namespace TuttiFruit.Candy.Core.Handlers
{
    public delegate ValueTask AsyncEventHandler<in TEvent>(object sender, TEvent @event) where TEvent : EventArgs;
}
