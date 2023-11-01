using ArchitectVisualizer.Common;

namespace ArchitectVisualizer.Controllers;

public class ModelsStore
{
    private readonly List<RegisterModel> _registerModels = new List<RegisterModel>();

    public void AddModel(RegisterModel registerModel)
    {
        _registerModels.Add(registerModel);
    }

    public IEnumerable<RegisterModel> GetModels()
    {
        return _registerModels;
    }
}
