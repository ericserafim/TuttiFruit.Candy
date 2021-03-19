using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TuttiFruit.Candy.Rabbit.Exceptions
{
    public class UnhandledException : Exception
    {
        public UnhandledException()
        {
        }

        public UnhandledException(string message) : base(message)
        {
        }

        public UnhandledException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnhandledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
