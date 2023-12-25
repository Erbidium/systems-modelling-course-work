using Lab3;

const int simulationTime = 10000;
const int timesToRunSimulation = 100;

var simulationResults = SimulationRunner.RunModelSimulationMultipleTimes(simulationTime, timesToRunSimulation);
Console.WriteLine($"Simulation was repeated {timesToRunSimulation} times");
Results.PrintModelResults(simulationResults);

// var model = ModelCreator.CreateMachineRepairWorkshopModel();
// var simulationResults = model.Simulate(simulationTime);
// Results.PrintModelResults(simulationResults, model);





