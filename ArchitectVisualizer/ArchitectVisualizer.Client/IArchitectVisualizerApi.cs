using ArchitectVisualizer.Common;
using Refit;

namespace ArchitectVisualizer.Client;

public interface IArchitectVisualizerApi
{
    [Post("/models")]
    Task SetServiceModel(RegisterModel registerModel);
}
