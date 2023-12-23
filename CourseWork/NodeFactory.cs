using Lab3.Delays;

namespace Lab3;

public class NodeFactory
{
    private readonly IDelay _repairTimeGenerator;
    
    public NodeFactory(IDelay repairTimeGenerator)
    {
        _repairTimeGenerator = repairTimeGenerator;
    }
    
    public Node CreateNode()
    {
        return new Node(_repairTimeGenerator.Generate(null!));
    }
}