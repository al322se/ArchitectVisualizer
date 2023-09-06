using ArchitectVisualizer.Common;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectVisualizer.Controllers;

[ApiController]
[Route("[controller]")]
public class ModelsController : ControllerBase
{
    private readonly ModelsStore _modelsStore;
    private readonly GraphVizGenerator _graphVizGenerator;

    public ModelsController(ILogger<ModelsController> logger,ModelsStore modelsStore,GraphVizGenerator graphVizGenerator)
    {
        _logger = logger;
        _modelsStore = modelsStore;
        _graphVizGenerator = graphVizGenerator;
    }

    private readonly ILogger<ModelsController> _logger;



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

public class GraphResult
{
    public string Text { get; set; }
}

public class ModelsStore
{
    private List<RegisterModel> _registerModels = new List<RegisterModel>();

    public void AddModel(RegisterModel registerModel)
    {
        _registerModels.Add(registerModel);
    }
}

public class GraphVizGenerator
{
    private readonly ModelsStore _modelsStore;

    public GraphVizGenerator(ModelsStore modelsStore)
    {
        _modelsStore = modelsStore;
    }

    public string GenerateGraph()
    {
        return "digraph {c -> d}";
    }
}
