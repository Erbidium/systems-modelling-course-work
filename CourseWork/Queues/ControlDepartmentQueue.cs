using Lab3.Nodes;

namespace Lab3.Queues;

public class ControlDepartmentQueue : Queue
{
    public override Node Remove()
    {
        var nodeToRemove = Nodes[0];
        Nodes.RemoveAt(0);

        return nodeToRemove;
    }
}