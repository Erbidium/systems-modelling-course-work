using Lab3;

const int simulationTime = 10000;

var model = ModelCreator.CreateMachineRepairWorkshopModel();

model.Simulate(simulationTime);