using Lab3.Delays;

namespace Lab3;

public class NodeFactory
{
    private readonly IDelay _repairTimeGenerator;
    
    public NodeFactory(IDelay repairTimeGenerator)
    {
        _repairTimeGenerator = repairTimeGenerator;
    }
    
    public Node CreateNode(double currentTime)
    {
        return new Node(currentTime, _repairTimeGenerator.Generate(null!));
    }
}