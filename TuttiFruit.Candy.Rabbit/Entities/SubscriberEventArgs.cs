using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuttiFruit.Candy.Rabbit.Entities
{
    public class SubscriberEventArgs : EventArgs
    {
        public object Message { get; }

        public SubscriberEventArgs(object message)
        {
            Message = message;
        }
    }
}
