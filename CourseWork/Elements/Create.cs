using Lab3.Delays;

namespace Lab3.Elements;

public sealed class Create : Element
{
    private readonly NodeFactory _nodeFactory;
    
    public Create(IDelay delay, NodeFactory nodeFactory) : base(delay)
    {
        TimeNext = 0.0; // імітація розпочнеться з події Create

        _nodeFactory = nodeFactory;
    }

    public override void Exit() {
        base.Exit();
        var createdNode = _nodeFactory.CreateNode(TimeCurrent);
        
        TimeNext = TimeCurrent + GetDelay(createdNode);
        
        NextElement?.NextElement(createdNode)?.Enter(createdNode);
    }

    public override void DoStatistics(double delta) { }
}