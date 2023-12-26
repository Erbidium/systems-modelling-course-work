using Lab3.Nodes;

namespace Lab3.Delays;

public class ErlangDelay : IDelay
{
    private readonly double _lambda;
    private readonly int _k;

    private readonly Random _random = new();

    public ErlangDelay(double timeMean, int variance)
    {
        _lambda = timeMean / variance;
        _k = (int)(timeMean * _lambda);
    }
    
    public double Generate(Node node)
    {
        double erlangGeneratedValue = 0;

        for (int i = 0; i < _k; i++)
        {
            erlangGeneratedValue += Math.Log(_random.NextDouble());
        }

        return - erlangGeneratedValue / _lambda;
    }
}