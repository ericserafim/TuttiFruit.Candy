using System;
using System.Runtime.Serialization;

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
