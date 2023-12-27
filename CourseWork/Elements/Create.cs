using Lab3.Delays;
using Lab3.Nodes;

namespace Lab3.Elements;

public sealed class Create : Element
{
    private readonly NodeFactory _nodeFactory;

    public List<(double SimulationTime, double AverageWaitingTime)> WaitingTimeSimulationStatistics { get; } = new();

    public List<Node> AllNodes { get; }= new();
    
    private const int WaitingTimeStep = 5;
    
    public Create(IDelay delay, NodeFactory nodeFactory) : base(delay)
    {
        TimeNext = 0.0; // імітація розпочнеться з події Create

        _nodeFactory = nodeFactory;
    }

    public override void Exit() {
        base.Exit();
        var createdNode = _nodeFactory.CreateNode();
        AllNodes.Add(createdNode);
        
        TimeNext = TimeCurrent + GetDelay(createdNode);
        
        NextElement?.NextElement(createdNode)?.Enter(createdNode);
    }

    public override void DoStatistics(double delta)
    {
        if ((WaitingTimeSimulationStatistics.Count == 0
            || TimeCurrent - WaitingTimeSimulationStatistics[^1].SimulationTime >= WaitingTimeStep)
            && AllNodes.Count > 0)
        {
            double average = AllNodes.Select(n => n.TimeSpentInQueue).Average();
            
            WaitingTimeSimulationStatistics.Add((TimeCurrent, average));
        }
    }
}