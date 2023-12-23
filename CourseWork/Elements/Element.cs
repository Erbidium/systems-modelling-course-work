using Lab3.Delays;
using Lab3.NextElement;
using Lab3.Queues;

namespace Lab3.Elements;

public abstract class Element {
    private int Id { get; }
    public string Name { get; set; }
    public virtual double TimeCurrent { get; set; }
    public virtual double TimeNext { get; set; } = double.MaxValue;

    public int ServedElementsQuantity { get; protected set; }
    
    public INextElementPicker? NextElement { get; set; }

    public Queue Queue { get; set; } = new();
    
    public virtual bool IsServing { get; set; }

    public virtual bool IsFull => IsServing;

    private readonly IDelay _delay;
    
    protected Element(IDelay delay)
    {
        _delay = delay;
        Id = IdentifierGenerator.GetId();
        Name = $"element{Id}";
    }

    protected virtual double GetDelay(Node node)
        => _delay.Generate(node);
    
    public virtual void Enter(Node node) { }
    
    public virtual void Exit()
        => ServedElementsQuantity++;

    public virtual void PrintResult()
        => Console.WriteLine($"{Name} served quantity = {ServedElementsQuantity}");

    public virtual void PrintInfo()
    {
        // Console.WriteLine($"{Name} is {(IsServing ? "serving" : "waiting")}. Served quantity = {ServedElementsQuantity} TimeNext = {TimeNext}");
    }

    public abstract void DoStatistics(double delta);
}
