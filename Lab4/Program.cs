using System.Diagnostics;
using Lab3;

const int simulationTime = 600;

for (int processesCount = 100; processesCount <= 1000; processesCount += 100)
{
    //var model = ModelCreator.CreateChainedModel(processesCount);
    var model = ModelCreator.CreateBranchedModel(processesCount, 4);

    var startTime = Stopwatch.GetTimestamp();
    
    model.Simulate(simulationTime);
    
    var executionTimeInMilliseconds = Stopwatch.GetElapsedTime(startTime).TotalMilliseconds;
    
    Console.WriteLine($"Total events count: {processesCount + 1}, execution time: {executionTimeInMilliseconds} milliseconds");
}