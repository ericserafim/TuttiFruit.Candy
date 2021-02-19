using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuttiFruit.Candy.Rabbit.Entities
{
    public struct Message
    {
        public object Raw { get; private set; }

        public Message(object raw)
        {            
            Raw = raw;
        }
    }
}
