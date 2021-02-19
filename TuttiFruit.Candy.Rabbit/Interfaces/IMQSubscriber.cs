using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Handlers;

namespace TuttiFruit.Candy.Rabbit.Interfaces
{
    public interface IMQSubscriber
    {
        event AsyncEventHandler<SubscriberEventArgs> OnMessage;

        Task SendAckAsync(Message message);
    }
}
