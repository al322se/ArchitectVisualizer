using ArchitectVisualizer.Common;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectVisualizer.Controllers;

[ApiController]
[Route("[controller]")]
public class ModelsController : ControllerBase
{

    private readonly ILogger<ModelsController> _logger;

    public ModelsController(ILogger<ModelsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public Task Post(ServiceModel serviceModel)
    {
        throw new NotImplementedException();
    }
}
