using Lab3.Delays;

namespace Lab3.Nodes;

public class NodeFactory
{
    private readonly IDelay _repairTimeGenerator;
    
    public NodeFactory(IDelay repairTimeGenerator)
    {
        _repairTimeGenerator = repairTimeGenerator;
    }
    
    public Node CreateNode(double? repairTime = null)
    {
        return new Node(repairTime ?? _repairTimeGenerator.Generate(null!));
    }
}