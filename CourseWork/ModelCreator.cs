using Lab3.Delays;
using Lab3.Elements;
using Lab3.ItemFactories;
using Lab3.NextElement;
using Lab3.Queues;

namespace Lab3;

public static class ModelCreator
{
    public static NetMO CreateMachineRepairWorkshopModel()
    {
        var nodeCreator = new Create(new ExponentialDelay(10.25), new NodeFactory(new ErlangDelay(22, 242))) { Name = "NODE_CREATOR" };
        var repairDepartment = new SystemMO(new RepairDelay(), 3)
        {
            Name = "REPAIR_DEPARTMENT",
            Queue = new RepairDepartmentQueue()
        };
        var controlDepartment = new SystemMO(new ConstantDelay(6), 1)
        {
            Name = "CONTROL_DEPARTMENT",
            Queue = new ControlDepartmentQueue()
        };
        var endServing = new EndServing(new ConstantDelay(0)){ Name = "END_NODES_SERVING" };

        nodeCreator.NextElement = new OneNextElementPicker(repairDepartment);
        repairDepartment.NextElement = new OneNextElementPicker(controlDepartment);
        controlDepartment.NextElement = new WeightedNextElementPicker
        {
            NextElementChances = { (repairDepartment, 0.15), (endServing, 0.85) }
        };

        var elements = new List<Element> { nodeCreator, repairDepartment, controlDepartment, endServing };

        return new NetMO(elements);
    }
}