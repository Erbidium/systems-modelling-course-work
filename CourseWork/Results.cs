namespace Lab3;

public static class Results
{
    public static void PrintModelResults(IReadOnlyDictionary<string, double> results, NetMO? model = null)
    {
        Console.WriteLine("\n-------------RESULTS-------------");

        PrintValueForKey(ResultKeys.RepairDepartmentDevice1Loading);
        PrintValueForKey(ResultKeys.RepairDepartmentDevice2Loading);
        PrintValueForKey(ResultKeys.RepairDepartmentDevice3Loading);
        Console.WriteLine();

        PrintValueForKey(ResultKeys.MeanWaitingTime);
        PrintValueForKey(ResultKeys.WaitingTimeStandardDeviation);
        if (model is not null)
        {
            Console.WriteLine("Waiting frequencies:");
            foreach (var waitingFrequency in StatsCalculator.GetWaitingFrequencies(model))
            {
                Console.WriteLine(
                    $"{waitingFrequency.RangeStart:0.0} - {waitingFrequency.RangeEnd:0.0} : {waitingFrequency.Count}");
            }
        }

        Console.WriteLine();
        PrintValueForKey(ResultKeys.AverageNodesCount);
        Console.WriteLine();

        PrintValueForKey(ResultKeys.MeanRepairQuality);
        PrintValueForKey(ResultKeys.RepairQualityStandardDeviation);
        if (model is not null)
        {
            Console.WriteLine("Repair frequencies:");
            foreach (var repairFrequency in StatsCalculator.GetRepairFrequencies(model))
            {
                Console.WriteLine(
                    $"{repairFrequency.RangeStart:0.0} - {repairFrequency.RangeEnd:0.0} : {repairFrequency.Count}");
            }
        }

        Console.WriteLine();

        if (model is not null)
        {
            foreach (var element in model.Elements)
            {
                element.PrintResult();
            }
        }

        return;

        void PrintValueForKey(string key)
        {
            Console.WriteLine($"{key}: {results[key]}");
        }
    }
}