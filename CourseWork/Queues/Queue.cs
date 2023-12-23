namespace Lab3.Queues;

public class Queue
{
    public List<Node> Nodes { get; } = new();

    public int MaxCount { get; } = int.MaxValue;
    
    public Queue() { }

    public Queue(int maxCount)
    {
        MaxCount = maxCount;
    }
    
    public Queue(IEnumerable<Node> nodes, int maxCount)
    {
        Nodes.AddRange(nodes);
        MaxCount = maxCount;
    }

    public virtual void Add(Node node)
    {
        Nodes.Add(node);
    }

    public virtual Node Remove()
    {
        var lastNode = Nodes[^1];
        Nodes.RemoveAt(Nodes.Count - 1);
        return lastNode;
    }
}