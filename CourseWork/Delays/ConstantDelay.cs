using Lab3.Items;

namespace Lab3.Delays;

public class ConstantDelay : IDelay
{
    private readonly double _delay;

    public ConstantDelay(double delay)
        => _delay = delay;

    public double Generate(SimpleItem item)
        => _delay;
}