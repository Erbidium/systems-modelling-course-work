using Lab3;
using Lab3.ModelStats;

const int simulationTime = 600;

var model = ModelCreator.CreateMachineRepairWorkshopModel();
var statsPrinter = new ModelStats(model);

model.Simulate(simulationTime, statsPrinter);