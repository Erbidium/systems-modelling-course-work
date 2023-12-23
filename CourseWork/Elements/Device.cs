using Lab3.Delays;
using Lab3.Items;

namespace Lab3.Elements;

public sealed class Device : Element
{
    public SimpleItem? ProcessedItem { get; set; }
    
    public Device(IDelay delay) : base(delay)
    {
        TimeNext = double.MaxValue;
    }
    
    public override void Enter(SimpleItem item)
    {
        IsServing = true;
        TimeNext = TimeCurrent + GetDelay(item);
        ProcessedItem = item;
    }

    public override void Exit()
    {
        IsServing = false;
        TimeNext = double.MaxValue;
        ProcessedItem = null;
        base.Exit();
    }

    public override void DoStatistics(double delta) { }
}