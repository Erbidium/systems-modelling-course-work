using System.Runtime.InteropServices;
using Lab3.Elements;

namespace Lab3;

public static class SimulationRunner
{
    public static Dictionary<string, double> RunModelSimulationMultipleTimes(int simulationTime, int timesToRunSimulation, bool createPlot = false)
    {
        var waitingTimeSimulationPlotsData = new List<List<(double SimulationTime, double AverageWaitingTime)>>();
            
        var cumulativeSimulationResults = new Dictionary<string, double>();
        
        for (int i = 0; i < timesToRunSimulation; i++)
        {
            var model = ModelCreator.CreateMachineRepairWorkshopModel();

            var simulationResults = model.Simulate(simulationTime);

            waitingTimeSimulationPlotsData.Add((model.Elements[0] as Create)!.WaitingTimeSimulationStatistics);
            
            foreach (var resultKey in simulationResults.Keys)
            {
                ref var accumulatedResultValue = ref CollectionsMarshal.GetValueRefOrAddDefault(cumulativeSimulationResults, resultKey, out _);
                accumulatedResultValue += simulationResults[resultKey];
            }
        }

        var finalResults = new Dictionary<string, double>();
        foreach (var resultKey in cumulativeSimulationResults.Keys)
        {
            var resultValue = cumulativeSimulationResults[resultKey] / timesToRunSimulation;
            finalResults.Add(resultKey, resultValue);
        }

        if (createPlot)
        {
            CreatePlot(waitingTimeSimulationPlotsData);
        }

        return finalResults;
    }

    private static void CreatePlot(List<List<(double SimulationTime, double AverageWaitingTime)>> waitingTimeSimulationPlotsData)
    {
        var plot = new ScottPlot.Plot();
        
        plot.XLabel("Simulation time");
        plot.YLabel("Average waiting time");
        
        int modelRunIndex = 0;
        waitingTimeSimulationPlotsData.ForEach(modelRun =>
        {
            double[] y = modelRun.Select(p => p.AverageWaitingTime).ToArray();
            double[] x = modelRun.Select(p => p.SimulationTime).ToArray();

            plot.PlotScatter(x, y, label: $"Model run: {modelRunIndex++}");
        });

        plot.Title("");
        plot.Legend();

        var plotFileName = "waitingPlot.png";
        plot.SaveFig(plotFileName);

        Console.WriteLine($"Plot was saved as {plotFileName}");
    }
}