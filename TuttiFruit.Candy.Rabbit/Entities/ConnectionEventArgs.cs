using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuttiFruit.Candy.Rabbit.Entities
{
    public class ConnectionEventArgs : EventArgs
    {
        public Exception Exception { get; }

        public ConnectionEventArgs(Exception exception)
        {
            Exception = exception;
        }

    }
}
