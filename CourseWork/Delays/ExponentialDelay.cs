namespace Lab3.Delays;

public class ExponentialDelay : IDelay
{
    private readonly double _timeMean;

    private readonly Random _random = new();

    public ExponentialDelay(double timeMean)
        => _timeMean = timeMean;
    
    public double Generate(Node node)
    {
        double randomNumber = 0;
        while (randomNumber == 0)
            randomNumber = _random.NextDouble();
        
        return - _timeMean * Math.Log(randomNumber);
    }
}