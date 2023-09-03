
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace ArchitectVisualizer.Client;

public static class RegistrationExtensions
{

    public static IServiceCollection RegisterArchitectVisualizer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ArchitectVisualizerOptions>(configuration.GetSection(nameof(ArchitectVisualizerOptions)));
  
        services.AddRefitClient<IArchitectVisualizerApi>()
            .ConfigureHttpClient((serviceProvider,client) =>
            {
                var options = serviceProvider.GetRequiredService<ArchitectVisualizerOptions>();
                client.BaseAddress = new Uri(options.BaseAddress!);
            });
        return services;
    }
}
