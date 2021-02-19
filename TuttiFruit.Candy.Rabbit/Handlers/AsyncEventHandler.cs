using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuttiFruit.Candy.Rabbit.Handlers
{
    public delegate ValueTask AsyncEventHandler<in TEvent>(object sender, TEvent @event) where TEvent : EventArgs;
}
