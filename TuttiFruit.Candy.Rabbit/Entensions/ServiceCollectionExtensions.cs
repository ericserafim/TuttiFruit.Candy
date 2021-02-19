using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Factories;
using TuttiFruit.Candy.Rabbit.Implementations;
using TuttiFruit.Candy.Rabbit.Interfaces;
using TuttiFruit.Candy.Rabbit.Services;

namespace TuttiFruit.Candy.Rabbit.Entensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTuttiFruitCandyRabbit<TMessageHandler>(this IServiceCollection services, IConfiguration configuration)
            where TMessageHandler : class
        {
            services.AddOptions(configuration);

            services.AddSingleton(sp => new ChannelFactory(sp.GetRequiredService<IOptions<ChannelSettings>>()).CreateChannel());
            services.AddSingleton(sp => sp.GetRequiredService<Channel<Message>>().Writer);
            services.AddSingleton(sp => sp.GetRequiredService<Channel<Message>>().Reader);
            services.AddSingleton<IMQSubscriber, RabbitSubscriber>();
            services.AddSingleton<IProducer, Producer>();

            services.AddSingleton<IConsumerFactory>(sp =>
                new ConsumerFactory(
                    sp.GetRequiredService<IOptions<TuttiFruitCandySettings>>(),
                    sp.GetRequiredService<ChannelReader<Message>>(),
                    sp.GetRequiredService<IMQSubscriber>(),
                    sp.GetRequiredService<IMessageHandler>));

            services.AddTransient<TMessageHandler>();

            services.AddHostedService<BackgroundWorkerService>();

            return services;
        }
    }
}
