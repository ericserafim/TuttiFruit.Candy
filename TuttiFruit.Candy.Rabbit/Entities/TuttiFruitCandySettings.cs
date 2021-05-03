namespace TuttiFruit.Candy.Rabbit.Entities
{
  public sealed class TuttiFruitCandySettings
  {
    public int NumberOfConsumers { get; set; }

    public ChannelSettings ChannelSettings { get; set; }

    public RabbitSettings RabbitSettings { get; set; }
  }
}
