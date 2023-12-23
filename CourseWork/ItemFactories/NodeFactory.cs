using Lab3.Delays;
using Lab3.Items;

namespace Lab3.ItemFactories;

public class NodeFactory : IItemFactory
{
    private readonly IDelay _repairTimeGenerator;
    
    public NodeFactory(IDelay repairTimeGenerator)
    {
        _repairTimeGenerator = repairTimeGenerator;
    }
    
    public SimpleItem CreateItem(double currentTime)
    {
        return new Node(currentTime, _repairTimeGenerator.Generate(null!));
    }
}