using Lab3.Elements;

namespace Lab3;

public static class StatsCalculator
{
    private static EndServing GetEndServing(NetMO model)
    {
        return (model.Elements.First(el => el is EndServing) as EndServing)!;
    }

    public static List<double> GetRepairQualities(NetMO model)
    {
        return GetEndServing(model).ServedNodes.Select(n => n.ReturnsCount == 0 ? 1 : 1.0 / n.ReturnsCount).ToList();
    }
    
    public static List<double> GetWaitingTimes(NetMO model)
    {
        return GetEndServing(model).ServedNodes.Select(n => n.TimeSpentInQueue).ToList();
    }
    
    public static List<(double RangeStart, double RangeEnd, int Count)> GetRepairFrequencies(NetMO model)
    {
        var repairQualities = GetRepairQualities(model);
        const double frequencyRange = 0.2;
        var repairFrequencies = new List<(double RangeStart, double RangeEnd, int Count)>();
        for (double i = 0; i <= 1 - frequencyRange; i += 0.2)
        {
            var rangeStart = i;
            var rangeEnd = i + frequencyRange;

            int count = repairQualities.Count(f => f > rangeStart && f <= rangeEnd);
            repairFrequencies.Add((rangeStart, rangeEnd, count));
        }

        return repairFrequencies;
    }

    public static List<(double RangeStart, double RangeEnd, int Count)> GetWaitingFrequencies(NetMO model)
    {
        var waitingTimeRangesCount = 4;
        var waitingTimes = StatsCalculator.GetWaitingTimes(model);
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
        waitingFrequencies[0] = (waitingFrequencies[0].RangeStart, waitingFrequencies[0].RangeEnd,
            waitingFrequencies[0].Count + zeroTimeCount);

        return waitingFrequencies;
    }
}