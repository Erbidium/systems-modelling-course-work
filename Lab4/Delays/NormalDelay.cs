using Lab3.Items;

namespace Lab3.Delays;

public class NormalDelay : IDelay
{
    private readonly double _timeMean;
    private readonly double _timeDeviation;

    private readonly Random _random = new();

    public NormalDelay(double timeMean, double timeDeviation)
    {
        _timeMean = timeMean;
        _timeDeviation = timeDeviation;
    }
    
    public double Generate(SimpleItem item)
    {
        return _timeDeviation * GenerateMu() + _timeMean;
    }

    private double GenerateMu()
    {
        double sum = 0;
        
        for (int i = 0; i < 12; i++)
            sum += _random.NextDouble();

        return sum - 6;
    }
}