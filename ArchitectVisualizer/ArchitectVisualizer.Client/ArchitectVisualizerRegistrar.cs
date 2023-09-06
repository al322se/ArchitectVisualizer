using ArchitectVisualizer.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArchitectVisualizer.Client;

public class ArchitectVisualizerRegistrar : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Action<IServiceProvider, Visualizer> _register;
    private readonly IArchitectVisualizerApi _api;
    public ArchitectVisualizerRegistrar(IServiceProvider serviceProvider, Action<IServiceProvider,Visualizer> register)
    {
        _serviceProvider = serviceProvider;
        _register = register;
        _api= serviceProvider.GetRequiredService<IArchitectVisualizerApi>();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var visualizer = new Visualizer();
        _register(_serviceProvider, visualizer);
        var serviceModel=  visualizer.GetServiceModel();
        await _api.SetServiceModel(serviceModel);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
