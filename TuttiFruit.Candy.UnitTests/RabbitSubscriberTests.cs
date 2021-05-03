using FluentAssertions;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Implementations;
using Xunit;

namespace TuttiFruit.Candy.UnitTests
{
  public class RabbitSubscriberTests
  {
    [Fact]
    public async Task StartAsync_WhenSettingsIsInvalid_ShouldRaiseOnConnectionEventAsync()
    {
      //Arrange
      var eventTrigged = false;
      var settings = Options.Create(new RabbitSettings());
      var sut = new RabbitSubscriber(settings);

      sut.OnConnectionError += async (sender, @event) =>
      {
        eventTrigged = true;
        await Task.CompletedTask;
      };

      //Act
      await sut.StartAsync(default);


      //Assert
      eventTrigged.Should().BeTrue();
    }
  }
}
