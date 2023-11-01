using ArchitectVisualizer.Common;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectVisualizer.Controllers;

[ApiController]
[Route("[controller]")]
public class ModelsController : ControllerBase
{
    private readonly ModelsStore _modelsStore;
    private readonly GraphVizGenerator _graphVizGenerator;
    private readonly ILogger<ModelsController> _logger;
    public ModelsController(ILogger<ModelsController> logger,ModelsStore modelsStore,GraphVizGenerator graphVizGenerator)
    {
        _logger = logger;
        _modelsStore = modelsStore;
        _graphVizGenerator = graphVizGenerator;
    }

    [HttpPost]
    public Task Post(RegisterModel registerModel)
    {
        _modelsStore.AddModel(registerModel);
        return Task.CompletedTask;
    }

    [HttpGet("/graph")]
    public Task<GraphResult> GetGraph()
    {
        var graph= _graphVizGenerator.GenerateGraph();

        return  Task.FromResult(new GraphResult(){Text=graph});
    }
}
