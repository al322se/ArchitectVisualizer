using System.Text;
using ArchitectVisualizer.Common;

namespace ArchitectVisualizer.Controllers;

public class GraphVizGenerator
{
    private readonly ModelsStore _modelsStore;

    public GraphVizGenerator(ModelsStore modelsStore)
        => _modelsStore = modelsStore;

    public string GenerateGraph()
    {
        var models = _modelsStore.GetModels();
        var graphBuilder = new StringBuilder();
        graphBuilder.OpenGlobalGraph();
        foreach (var model in models)
        {
            graphBuilder
                .OpenCluster()
                .SetLabel(model.CurrentService.Name);
            string? produceNodeName = null;
            if (model.CurrentService.ToDependencies.Any(x => x.Type == ModelType.Queue))
                graphBuilder.SetProduceNode(out produceNodeName);

            graphBuilder.CloseCluster();

            foreach (var toDependency in model.CurrentService.ToDependencies)
            {
                if (toDependency.Type == ModelType.Queue)
                {
                    graphBuilder.AddTopic(toDependency.Name)
                        .SetDependency(produceNodeName!, toDependency.Name);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
        graphBuilder.CloseGlobalGraph();
        return graphBuilder.ToString();
    }
}

public static class GraphBuilderExtensions
{
    private static string GetUniqPostfix()
    {
        return Guid.NewGuid().ToString();
    }

    public static StringBuilder SetLabel(this StringBuilder graphBuilder,string label)
    {
        graphBuilder.AppendLine($"label = \"{label}\";");
        return graphBuilder;
    }

    public static StringBuilder SetProduceNode(this StringBuilder graphBuilder,out string nodeName)
    {
        graphBuilder.AppendLine("node [style=filled,color=white,label=produce];");
        nodeName= $"produce_{GetUniqPostfix()}";
        graphBuilder.AppendLine(nodeName);
        return graphBuilder;
    }

    public static StringBuilder AddTopic(this StringBuilder graphBuilder,string topicName)
    {
        graphBuilder.AppendLine($"{topicName} [shape=Msquare;label=\"{topicName}\"]");
        return graphBuilder;
    }

    public static StringBuilder SetDependency(this StringBuilder graphBuilder, string from,string to)
    {
        graphBuilder.AppendLine($"{from} -> {to}");
        return graphBuilder;
    }

    public static StringBuilder OpenCluster(this StringBuilder graphBuilder)
    {
        graphBuilder.AppendLine($"subgraph cluster_{GetUniqPostfix()} {{");
        return graphBuilder;
    }
    public static StringBuilder CloseCluster(this StringBuilder graphBuilder)
    {
        graphBuilder.AppendLine("}");
        return graphBuilder;
    }

    public static StringBuilder OpenGlobalGraph(this StringBuilder graphBuilder)
    {
        graphBuilder.AppendLine($"digraph GlobalGraph_{GetUniqPostfix()} {{");
        return graphBuilder;
    }
    public static StringBuilder CloseGlobalGraph(this StringBuilder graphBuilder)
    {
        graphBuilder.AppendLine("}");
        return graphBuilder;
    }
}
