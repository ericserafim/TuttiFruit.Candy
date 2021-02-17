﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Channels;
using TuttiFruit.Candy.Core.Entities;
using TuttiFruit.Candy.Core.Factories;
using TuttiFruit.Candy.Core.Interfaces;
using TuttiFruit.Candy.Core.Services;

namespace TuttiFruit.Candy.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTuttiFruitCandy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions(configuration);

            services.AddSingleton(sp => new ChannelFactory(sp.GetRequiredService<IOptions<ChannelSettings>>()).CreateChannel());
            services.AddSingleton(sp => sp.GetRequiredService<Channel<Message>>().Writer);
            services.AddSingleton(sp => sp.GetRequiredService<Channel<Message>>().Reader);

            services.AddSingleton<IProducerFactory>(sp =>
                new ProducerFactory(
                    sp.GetRequiredService<ChannelWriter<Message>>(),
                    (typeName) => (IMqSubscriber)sp.GetRequiredService(Type.GetType(typeName))));

            services.AddSingleton<IConsumerFactory>(sp =>
                new ConsumerFactory(
                    sp.GetRequiredService<ChannelReader<Message>>(),
                    sp.GetRequiredService<IMessageHandler>));

            services.AddHostedService<BackgroundWorkerService>();
        }
    }
}
