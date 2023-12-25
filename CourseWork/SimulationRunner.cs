using System.Runtime.InteropServices;

namespace Lab3;

public static class SimulationRunner
{
    public static Dictionary<string, double> RunModelSimulationMultipleTimes(int simulationTime, int timesToRunSimulation)
    {
        var cumulativeSimulationResults = new Dictionary<string, double>();
        
        for (int i = 0; i < timesToRunSimulation; i++)
        {
            var model = ModelCreator.CreateMachineRepairWorkshopModel();

            var simulationResults = model.Simulate(simulationTime);
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

        return finalResults;
    }
}