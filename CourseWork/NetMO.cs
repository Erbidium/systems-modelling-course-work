using Lab3.Elements;

namespace Lab3;

public class NetMO {
    public List<Element> Elements { get; }
    
    private double _timeNext;
    
    private double _timeCurrent;
    
    public NetMO(List<Element> elements)
        => Elements = elements;

    public void Simulate(double time)
    {
        while (_timeCurrent < time)
        {
            _timeNext = Elements.Min(e => e.TimeNext);
            
            Elements.ForEach(e => e.DoStatistics(_timeNext - _timeCurrent));
            
            _timeCurrent = _timeNext;

            Elements.ForEach(e => e.TimeCurrent = _timeCurrent);

            Console.WriteLine($"-----Current time: {_timeCurrent}----");
            
            foreach (var element in Elements)
            {
                if (element.TimeNext == _timeCurrent)
                {
                    Console.WriteLine($"Next event will be in element {element.Name}");
                    element.Exit();
                }
            }
            
            PrintInfo();
        }
        PrintResult();
    }

    private void PrintInfo()
    {
        foreach (var element in Elements)
        {
            element.PrintInfo();
        }
    }

    private void PrintResult()
    {
        Console.WriteLine("\n-------------RESULTS-------------");

        var processes = Elements.Where(el => el is SystemMO).Select(s => (s as SystemMO)!).ToList();
        
        Console.WriteLine($"Average nodes count in system{processes.Sum(p => p.NodesCountStat) / _timeCurrent}");
        Console.WriteLine();

        var endServing = (Elements.First(el => el is EndServing) as EndServing)!;
        
        
        var repairQualities = endServing.ServedNodes.Select(n => n.ReturnsCount == 0 ? 1 : 1.0 / n.ReturnsCount).ToList();
        
        var meanRepairQuality = repairQualities.Average();
        var repairQualityStandardDeviation = repairQualities.Sum(q => Math.Pow(q - meanRepairQuality, 2)) / repairQualities.Count;

        var frequencyRange = 0.2;
        var repairFrequencies = new List<(double RangeStart, double RangeEnd, int Count)>();
        for (double i = 0; i <= 1 - frequencyRange; i += 0.2)
        {
            var rangeStart = i;
            var rangeEnd = i + frequencyRange;

            int count = repairQualities.Count(f => f > rangeStart && f <= rangeEnd);
            repairFrequencies.Add((rangeStart, rangeEnd, count));
        }
        
        Console.WriteLine($"Mean repair quality: {meanRepairQuality}");
        Console.WriteLine($"Repair quality standard deviation: {repairQualityStandardDeviation}");
        Console.WriteLine("Repair frequencies:");
        foreach (var repairFrequency in repairFrequencies)
        {
            Console.WriteLine($"{repairFrequency.RangeStart:0.0} - {repairFrequency.RangeEnd:0.0} : {repairFrequency.Count}");
        }
        
        var waitingTimes = endServing.ServedNodes.Select(n => n.TimeSpentInQueue).ToList();
        
        var meanWaitingTime = waitingTimes.Average();
        var meanWaitingTimeStandardDeviation = repairQualities.Sum(q => Math.Pow(q - meanWaitingTime, 2)) / waitingTimes.Count;
        
        var waitingTimeRangesCount = 4;
        var maxWaitingTime = waitingTimes.Max();
        
        var waitingTimeRange = maxWaitingTime / waitingTimeRangesCount;
        var waitingFrequencies = new List<(double RangeStart, double RangeEnd, int Count)>();
        for (double i = 0; i <= maxWaitingTime; i += waitingTimeRange)
        {
            var rangeStart = i;
            var rangeEnd = i + waitingTimeRange;

            int count = waitingTimes.Count(f => f > rangeStart && f <= rangeEnd);
            waitingFrequencies.Add((rangeStart, rangeEnd, count));
        }

        var zeroTimeCount = waitingTimes.Count(t => t == 0);
        waitingFrequencies[0] = (waitingFrequencies[0].RangeStart, waitingFrequencies[0].RangeEnd, waitingFrequencies[0].Count + zeroTimeCount);
         
        Console.WriteLine($"Mean waiting time: {meanWaitingTime}");
        Console.WriteLine($"Waiting time standard deviation: {meanWaitingTimeStandardDeviation}");
        Console.WriteLine("Waiting frequencies:");
        foreach (var waitingFrequency in waitingFrequencies)
        {
            Console.WriteLine($"{waitingFrequency.RangeStart:0.0} - {waitingFrequency.RangeEnd:0.0} : {waitingFrequency.Count}");
        }
        
        foreach (var element in Elements) {
            element.PrintResult();
            
            if (element is not SystemMO process)
                continue;
            
            Console.WriteLine($"Mean length of queue = {process.MeanQueueStat / _timeCurrent}");
            Console.WriteLine($"Failure probability = {process.Failure / (double) (process.Failure + process.ServedElementsQuantity)}");
            Console.WriteLine($"Failure rate = {process.Failure / (double) (process.Failure + process.ServedElementsQuantity) * 100} %");
            Console.WriteLine($"Average loading: {process.LoadTimeStat / _timeCurrent}");

            if (process.Devices.Count > 0)
            {
                Console.WriteLine("Devices load");
                for (int i = 0; i < process.Devices.Count; i++)
                {
                    Console.WriteLine($"Device{i} loading: {process.Devices[i].LoadTimeStat / _timeCurrent}");
                }
            }
            
            Console.WriteLine($"Average serving time: {process.LoadTimeStat / process.ServedElementsQuantity}");
            Console.WriteLine($"Average working devices: {process.MeanWorkingDevicesStat / _timeCurrent}");
        }
    }
}