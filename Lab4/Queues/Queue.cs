using Lab3.Items;

namespace Lab3.Queues;

public class Queue
{
    public List<SimpleItem> Items { get; } = new();

    public int MaxCount { get; } = int.MaxValue;
    
    public Queue() { }

    public Queue(int maxCount)
    {
        MaxCount = maxCount;
    }
    
    public Queue(IEnumerable<SimpleItem> items, int maxCount)
    {
        Items.AddRange(items);
        MaxCount = maxCount;
    }

    public virtual void Add(SimpleItem item)
    {
        Items.Add(item);
    }

    public virtual SimpleItem Remove()
    {
        var lastItem = Items[^1];
        Items.RemoveAt(Items.Count - 1);
        return lastItem;
    }
}