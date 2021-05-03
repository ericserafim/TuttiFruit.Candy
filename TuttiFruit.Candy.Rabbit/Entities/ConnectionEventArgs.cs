using System;

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
