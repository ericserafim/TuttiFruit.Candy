using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TuttiFruit.Candy.Core.Extensions;
using TuttiFruit.Candy.Core.Interfaces;
using TuttiFruit.Candy.RabbitMq;
using TuttiFruit.Candy.RedisMq;

namespace TuttiFruit.Candy.TestHarness
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();            
            services.AddTuttiFruitCandy(Configuration);            

            //TODO Ver como injetar os Subscribers via appsettings
            services.AddTransient(sp =>
                new RedisSubscriber(
                    "redis-19488.c16.us-east-1-2.ec2.cloud.redislabs.com:19488,password=hZrYhwwaNYWkAu71rtUm78otBvoXyPep",
                    "TuttiFruitCandy"));

            services.AddSingleton<RabbitSubscriberFake>();
            services.AddTransient<IMessageHandler>(sp => new MessageHandler((typeName) => (IMqSubscriber)sp.GetRequiredService(Type.GetType(typeName))));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
