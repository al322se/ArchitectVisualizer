using ArchitectVisualizer.Common;
using Refit;

namespace ArchitectVisualizer.Client;

public interface IArchitectVisualizerApi
{
    [Post("/users/{user}")]
    Task GetUser(ServiceModel serviceModel);
}
