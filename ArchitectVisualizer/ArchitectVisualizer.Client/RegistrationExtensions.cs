using ArchitectVisualizer.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace ArchitectVisualizer.Client;

public static class RegistrationExtensions
{

    public static IServiceCollection RegisterArchitectVisualizer(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IServiceProvider,Visualizer> register)
    {
        services.Configure<ArchitectVisualizerOptions>(configuration.GetSection(nameof(ArchitectVisualizerOptions)));
        services.AddHostedService<ArchitectVisualizerRegistrar>(sp=> new ArchitectVisualizerRegistrar(sp,register));
        services.AddRefitClient<IArchitectVisualizerApi>()
            .ConfigureHttpClient((serviceProvider,client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<ArchitectVisualizerOptions>>();
                client.BaseAddress = new Uri(options.Value.BaseAddress!);
            });
        return services;
    }
}
