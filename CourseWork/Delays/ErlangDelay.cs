using Lab3.Nodes;

namespace Lab3.Delays;

public class ErlangDelay : IDelay
{
    private readonly double _timeMean;
    private readonly int _k;

    private readonly Random _random = new();

    public ErlangDelay(double timeMean, int k)
    {
        _timeMean = timeMean;
        _k = k;
    }
    
    public double Generate(Node node)
    {
        double erlangGeneratedValue = 0;

        for (int i = 0; i < _k; i++)
        {
            erlangGeneratedValue += Math.Log(_random.NextDouble());
        }

        return -erlangGeneratedValue * _timeMean;
    }
}