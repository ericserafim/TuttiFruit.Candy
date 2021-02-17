using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TuttiFruit.Candy.Core.Entities;

namespace TuttiFruit.Candy.Core.Extensions
{
    public static class OptionsExtensions
    {
        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TuttiFruitCandySettings>(configuration.GetSection(nameof(TuttiFruitCandySettings)));
            services.Configure<ChannelSettings>(configuration.GetSection($"{nameof(TuttiFruitCandySettings)}:{nameof(ChannelSettings)}"));            
        }
    }
}
