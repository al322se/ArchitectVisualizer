namespace ArchitectVisualizer.Common;

public class NodeModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Type { get; set; }

    public List<NodeModel> ToDependencies { get; set; } = new();
    public List<NodeModel> FromDependencies { get; set; } = new();

}
