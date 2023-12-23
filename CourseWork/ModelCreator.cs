using Lab3.Delays;
using Lab3.Elements;
using Lab3.ItemFactories;
using Lab3.Items;
using Lab3.NextElement;
using Lab3.Queues;

namespace Lab3;

public static class ModelCreator
{
    public static NetMO CreateMachineRepairWorkshopModel()
    {
        var repairTimeDelayGenerator = new ErlangDelay(22, 242);
        var nodeFactory = new NodeFactory(repairTimeDelayGenerator);
        var nodeCreator = new Create(new ExponentialDelay(10.25), nodeFactory) { Name = "NODE_CREATOR" };
        var repairDepartment = new SystemMO(new RepairDelay(), 3)
        {
            Name = "REPAIR_DEPARTMENT",
            Queue = new RepairDepartmentQueue()
        };

        var repairedItem1 = nodeFactory.CreateItem(0);
        repairDepartment.Devices[0].IsServing = true;
        repairDepartment.Devices[0].TimeNext = (repairedItem1 as Node)!.RepairTime;
        repairDepartment.Devices[0].ProcessedItem = repairedItem1;
        
        var repairedItem2 = nodeFactory.CreateItem(0);
        repairDepartment.Devices[1].IsServing = true;
        repairDepartment.Devices[1].TimeNext = (repairedItem2 as Node)!.RepairTime;
        repairDepartment.Devices[1].ProcessedItem = repairedItem2;
        
        
        var controlDepartment = new SystemMO(new ConstantDelay(6), 1)
        {
            Name = "CONTROL_DEPARTMENT",
            Queue = new ControlDepartmentQueue()
        };
        var endServing = new EndServing(new ConstantDelay(0)){ Name = "END_NODES_SERVING" };

        nodeCreator.NextElement = new OneNextElementPicker(repairDepartment);
        repairDepartment.NextElement = new OneNextElementPicker(controlDepartment);
        controlDepartment.NextElement = new NodeFinishingPicker
        {
            RepairDepartment = repairDepartment,
            EndServing = endServing
        };

        var elements = new List<Element> { nodeCreator, repairDepartment, controlDepartment, endServing };

        return new NetMO(elements);
    }
}