using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Entities;

namespace TuttiFruit.Candy.Core.Extensions
{
    public static class OptionsExtensions
    {
        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TuttiFruitCandySettings>(configuration.GetSection(nameof(TuttiFruitCandySettings)));
        }
    }
}
