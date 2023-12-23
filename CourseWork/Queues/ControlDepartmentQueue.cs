using Lab3.Items;

namespace Lab3.Queues;

public class ControlDepartmentQueue : Queue
{
    public override SimpleItem Remove()
    {
        var itemToRemove = Items[0];
        Items.RemoveAt(0);

        return itemToRemove;
    }
}