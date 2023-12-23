using Lab3.Items;

namespace Lab3.Queues;

public class AdvancedRepairDepartmentQueue : RepairDepartmentQueue
{
    public override SimpleItem Remove()
    {
        var nodes = Items.Select(i => (i as Node)!).ToList();
        var returnedItems = nodes.Where(n => n.ReturnsCount > 0).ToList();

        if (returnedItems.Count == 0)
        {
            return base.Remove();
        }
        
        var itemToRemove = returnedItems.MaxBy(n => n.ReturnsCount * n.RepairTime)!;
        Items.Remove(itemToRemove);
        return itemToRemove;
    }
}