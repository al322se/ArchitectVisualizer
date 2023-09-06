using ArchitectVisualizer.Client;
using Microsoft.Extensions.Options;

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


        services.RegisterArchitectVisualizer(configuration, (sp,visualizer) =>
        {
            var options = sp.GetRequiredService<IOptions<KafkaOptions>>();
            visualizer.AddCurrentService(nameof(ExampleConsumer)).AddToQueue(options.Value.Topics!);
        });
        services.AddHostedService<KafkaConsumer>();

        return services;
    }
}
