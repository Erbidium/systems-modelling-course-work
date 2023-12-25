using Lab3.Elements;

namespace Lab3;

public class NetMO {
    public List<Element> Elements { get; }
    
    private double _timeNext;
    
    private double _timeCurrent;
    
    public NetMO(List<Element> elements)
        => Elements = elements;

    public Dictionary<string, double> Simulate(double time)
    {
        while (_timeCurrent < time)
        {
            _timeNext = Elements.Min(e => e.TimeNext);
            
            Elements.ForEach(e => e.DoStatistics(_timeNext - _timeCurrent));
            
            _timeCurrent = _timeNext;

            Elements.ForEach(e => e.TimeCurrent = _timeCurrent);

            // Console.WriteLine($"-----Current time: {_timeCurrent}----");
            
            foreach (var element in Elements)
            {
                if (element.TimeNext == _timeCurrent)
                {
                    // Console.WriteLine($"Next event will be in element {element.Name}");
                    element.Exit();
                }
            }
            
            // PrintInfo();
        }

        return CalculateResults();
    }

    private void PrintInfo()
    {
        foreach (var element in Elements)
        {
            element.PrintInfo();
        }
    }

    private Dictionary<string, double> CalculateResults()
    {
        var processes = Elements.Where(el => el is SystemMO).Select(s => (s as SystemMO)!).ToList();
        
        var results = new Dictionary<string, double>();
        
        var repairDepartment = processes[0];
        results.Add(ResultKeys.RepairDepartmentDevice1Loading, repairDepartment.Devices[0].LoadTimeStat / _timeCurrent);
        results.Add(ResultKeys.RepairDepartmentDevice2Loading, repairDepartment.Devices[1].LoadTimeStat / _timeCurrent);
        results.Add(ResultKeys.RepairDepartmentDevice3Loading, repairDepartment.Devices[2].LoadTimeStat / _timeCurrent);
        
        var waitingTimes = StatsCalculator.GetWaitingTimes(this);
        var meanWaitingTime = waitingTimes.Average();
        var waitingTimeStandardDeviation = waitingTimes.Sum(q => Math.Pow(q - meanWaitingTime, 2)) / waitingTimes.Count;
        results.Add(ResultKeys.MeanWaitingTime, meanWaitingTime);
        results.Add(ResultKeys.WaitingTimeStandardDeviation, waitingTimeStandardDeviation);
        
        double averageNodesCount = processes.Sum(p => p.NodesCountStat) / _timeCurrent;
        results.Add(ResultKeys.AverageNodesCount, averageNodesCount);
        
        var repairQualities = StatsCalculator.GetRepairQualities(this);
        var meanRepairQuality = repairQualities.Average();
        var repairQualityStandardDeviation = repairQualities.Sum(q => Math.Pow(q - meanRepairQuality, 2)) / repairQualities.Count;
        results.Add(ResultKeys.MeanRepairQuality, meanRepairQuality);
        results.Add(ResultKeys.RepairQualityStandardDeviation, repairQualityStandardDeviation);
        
        return results;
    }
}