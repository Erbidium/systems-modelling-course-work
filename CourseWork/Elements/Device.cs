using Lab3.Delays;

namespace Lab3.Elements;

public sealed class Device : Element
{
    public Node? ProcessedNode { get; set; }
    
    public double LoadTimeStat { get; private set; }
    
    public Device(IDelay delay) : base(delay)
    {
        TimeNext = double.MaxValue;
    }
    
    public override void Enter(Node node)
    {
        IsServing = true;
        TimeNext = TimeCurrent + GetDelay(node);
        ProcessedNode = node;
    }

    public override void Exit()
    {
        IsServing = false;
        TimeNext = double.MaxValue;
        ProcessedNode = null;
        base.Exit();
    }

    public override void DoStatistics(double delta)
    {
        LoadTimeStat += delta;
    }
}