using Lab3.Items;

namespace Lab3.Queues;

public class RepairDepartmentQueue : Queue
{
    public override SimpleItem Remove()
    {
        var itemToRemove = Items.MinBy(i => (i as Node)!.RepairTime)!;

        Items.Remove(itemToRemove);
        return itemToRemove;
    }
}