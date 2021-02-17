using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Extensions;
using TuttiFruit.Candy.Core.Interfaces;
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

            services.AddTransient(sp => 
            new RedisSubscriber(
                "redis-19488.c16.us-east-1-2.ec2.cloud.redislabs.com:19488,password=hZrYhwwaNYWkAu71rtUm78otBvoXyPep", 
                "TuttiFruitCandy"));

            services.AddTransient<IMessageHandler, MessageHandler>();
            services.AddTuttiFruitCandy(Configuration);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
        }
    }
}
