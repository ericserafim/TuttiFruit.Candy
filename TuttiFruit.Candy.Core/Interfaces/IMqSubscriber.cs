using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Entities;
using TuttiFruit.Candy.Core.Handlers;

namespace TuttiFruit.Candy.Core.Interfaces
{
    public interface IMqSubscriber
    {
        event AsyncEventHandler<SubscriberEventArgs> OnMessage;

        Task SendAckAsync(object message);
    }
}
