using Lab3.Delays;
using Lab3.Items;

namespace Lab3.Elements;

public sealed class EndServing : Element
{
    public List<Node> ServedPatients { get; } = new();

    public EndServing(IDelay delay) : base(delay)
    {
        TimeNext = double.MaxValue;
    }

    public override void Enter(SimpleItem item)
    {
        if (item is not Node patient)
            return;
        
        patient.EndServing(TimeCurrent);
        ServedPatients.Add(patient);
        ServedElementsQuantity++;
    }

    public override void DoStatistics(double delta) { }
}