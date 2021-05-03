using System.Security.Authentication;

namespace TuttiFruit.Candy.Rabbit.Entities
{
  public sealed class RabbitSettings
  {
    public string UserName { get; set; }

    public string Password { get; set; }

    public string VirtualHost { get; set; }

    public string HostName { get; set; }

    public int Port { get; set; }

    public string QueueName { get; set; }

    public bool SslEnabled { get; set; }

    public string SslServerName { get; set; }

    public SslProtocols SslVersion { get; set; } = SslProtocols.Tls12;

    public string SslCertPath { get; set; }
  }

}
