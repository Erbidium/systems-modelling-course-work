using Lab3.Items;

namespace Lab3.Delays;

public class UniformDelay : IDelay
{
    private readonly Random _random = new();

    private readonly double _min;
    private readonly double _max;

    public UniformDelay(double min, double max)
    {
        _max = max;
        _min = min;
    }
    
    public double Generate(SimpleItem item)
    {
        return _min + _random.NextDouble() * (_max - _min);
    }
}