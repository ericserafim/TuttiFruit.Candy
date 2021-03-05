﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuttiFruit.Candy.Rabbit.Entities
{
    public class SubscriberEventArgs : EventArgs
    {
        public Message Message { get; }

        public SubscriberEventArgs(Message message)
        {
            Message = message;
        }
    }
}
