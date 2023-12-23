namespace Lab3.Queues;

public class RepairDepartmentQueue : Queue
{
    public override Node Remove()
    {
        var nodeToRemove = Nodes.MinBy(n => n.RepairTime)!;
        Nodes.Remove(nodeToRemove);
        return nodeToRemove;
    }
}