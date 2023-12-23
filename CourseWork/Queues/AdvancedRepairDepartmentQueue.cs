namespace Lab3.Queues;

public class AdvancedRepairDepartmentQueue : RepairDepartmentQueue
{
    public override Node Remove()
    {
        var returnedNodes = Nodes.Where(n => n.ReturnsCount > 0).ToList();

        if (returnedNodes.Count == 0)
        {
            return base.Remove();
        }
        
        var nodeToRemove = returnedNodes.MaxBy(n => n.ReturnsCount * n.RepairTime)!;
        Nodes.Remove(nodeToRemove);
        return nodeToRemove;
    }
}