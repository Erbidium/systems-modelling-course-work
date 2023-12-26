using Lab3.Delays;
using Lab3.NextElement;
using Lab3.Nodes;

namespace Lab3.Elements;

public abstract class Element {
    private int Id { get; }
    public string Name { get; set; }
    public virtual double TimeCurrent { get; set; }
    public virtual double TimeNext { get; set; } = double.MaxValue;
    public int ServedElementsQuantity { get; set; }
    public INextElementPicker? NextElement { get; set; }
    public virtual bool IsServing { get; set; }

    private readonly IDelay _delay;
    
    protected Element(IDelay delay)
    {
        _delay = delay;
        Id = IdentifierGenerator.GetId();
        Name = $"element{Id}";
    }

    protected double GetDelay(Node node)
        => _delay.Generate(node);
    
    public virtual void Enter(Node node) { }
    
    public virtual void Exit()
        => ServedElementsQuantity++;

    public virtual void PrintResult()
        => Console.WriteLine($"{Name} served quantity = {ServedElementsQuantity}");

    public virtual void PrintInfo()
    {
        Console.WriteLine($"{Name} is {(IsServing ? "serving" : "waiting")}. Served quantity = {ServedElementsQuantity} TimeNext = {TimeNext}");
    }

    public abstract void DoStatistics(double delta);
}
