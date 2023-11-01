namespace ArchitectVisualizer.Common;

public class Visualizer
{
    private readonly RegisterModel _model = new RegisterModel();

    public Visualizer AddCurrentService(string serviceName)
    {
        _model.CurrentService = new NodeModel() { Type = ModelType.Current };
        _model.CurrentService.Name = serviceName;
        return this;
    }

    public Visualizer AddToQueue(string queueName)
    {
        _model.CurrentService.ToDependencies.Add(new NodeModel() { Type = ModelType.Queue, Name = queueName });
        return this;
    }

    public RegisterModel GetServiceModel()
    {
        return _model;
    }
}
