using Lab3.Items;

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
    
    public double Generate(SimpleItem item)
    {
        double randomGeneratedProduct = 1.0;

        for (int i = 0; i < _k; i++)
        {
            randomGeneratedProduct *= _random.NextDouble();
        }

        return -Math.Log(randomGeneratedProduct) / _timeMean / _k;
    }
}