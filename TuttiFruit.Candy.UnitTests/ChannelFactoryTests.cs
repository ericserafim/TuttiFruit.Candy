using FluentAssertions;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Channels;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Factories;
using Xunit;

namespace TuttiFruit.Candy.UnitTests
{
  public class ChannelFactoryTests
  {

    [Fact]
    public void CreateChannel_WhenSettingsIsSetToUnbounded_ShouldCreateUnboundedChannell()
    {
      //Arrange
      var expectedChannel = Channel.CreateUnbounded<Message>();
      var settings = Options.Create(new ChannelSettings { Mode = ChannelMode.Unbounded });
      var sut = new ChannelFactory(settings);

      //Act
      var actionResult = sut.CreateChannel();

      //Assert
      actionResult.Should().BeOfType(expectedChannel.GetType());
    }

    [Fact]
    public void CreateChannel_WhenSettingsIsSetToBounded_ShouldCreateBoundedChannell()
    {
      //Arrange
      var capacity = new Random().Next();
      var expectedChannel = Channel.CreateBounded<Message>(capacity);
      var settings = Options.Create(new ChannelSettings { Mode = ChannelMode.Bounded, Capacity = capacity });
      var sut = new ChannelFactory(settings);

      //Act
      var actionResult = sut.CreateChannel();

      //Assert
      actionResult.Should().BeOfType(expectedChannel.GetType());
    }

    [Fact]
    public void CreateChannel_WhenSettingsIsSetToBoundedAndCapacityIsNotSet_ShouldThrowException()
    {
      //Arrange                        
      var settings = Options.Create(new ChannelSettings { Mode = ChannelMode.Bounded });
      var sut = new ChannelFactory(settings);

      //Act
      Func<Channel<Message>> actionResult = () => sut.CreateChannel();

      //Assert
      actionResult.Should().Throw<ArgumentOutOfRangeException>();
    }
  }
}
