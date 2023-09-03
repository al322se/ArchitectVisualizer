using ArchitectVisualizer.Client;

namespace ExampleConsumer;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<ArchitectVisualizerOptions>(configuration.GetSection(nameof(ArchitectVisualizerOptions)));

        services
            .Configure<KafkaOptions>(configuration.GetSection(nameof(KafkaOptions)))

            .Configure<ExampleAppOptions>(configuration.GetSection(nameof(ExampleAppOptions)));

        services.AddHostedService<KafkaConsumer>();
        services.RegisterArchitectVisualizer(configuration);
        services.AddHostedService<ArchitectVisualizerRegistrar>();
        return services;
    }


}

public class ArchitectVisualizerRegistrar : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task StopAsync(CancellationToken cancellationToken)
        => throw new NotImplementedException();
}
