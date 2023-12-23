using System.Diagnostics;
using Lab3.Delays;
using Lab3.Elements;
using Lab3.ItemFactories;
using Lab3.Items;
using Lab3.NextElement;

namespace Lab3;

public static class ModelCreator
{
    public static NetMO CreateChainedModel(int processesCount)
    {
        var delay = new ExponentialDelay(0.5);
        var simpleItemFactory = new SimpleItemFactory();
        
        var create = new Create(delay, simpleItemFactory) { Name = "CREATOR" };

        var elements = new List<Element> { create };

        Element previousElement = create;
        
        for (int i = 0; i < processesCount; i++)
        {
            var process = new SystemMO(delay, 1) { Name = $"PROCESSOR{i + 1}" };
            
            process.Enter(new SimpleItem());
            
            previousElement.NextElement = new WeightedNextElementPicker
            {
                NextElementChances = { (process, 1) }
            };
            
            previousElement = process;
            
            elements.Add(process);
        }

        return new NetMO(elements);
    }
    
    public static NetMO CreateBranchedModel(int processesCount, int branchesCount)
    {
        var delay = new ExponentialDelay(0.5);
        var simpleItemFactory = new SimpleItemFactory();
        
        var create = new Create(delay, simpleItemFactory) { Name = "CREATOR" };

        var elements = new List<Element> { create };

        var branches = new Element[branchesCount];
        for (int i = 0; i < branchesCount; i++)
        {
            branches[i] = new SystemMO(delay, 1) { Name = $"PROCESS{i + 1}"};
            elements.Add(branches[i]);
        }

        create.NextElement = new WeightedNextElementPicker
        {
            NextElementChances = branches.Select(b => (b, 1.0)).ToList()
        };
        
        int createdProcessesCount = branchesCount;

        for (int i = 0; i < branchesCount; i++)
        {
            for (int j = 0; j < (processesCount - branchesCount) / branchesCount; j++)
            {
                var process = new SystemMO(delay, 1){ Name = $"PROCESS{createdProcessesCount + 1}" };
                elements.Add(process);
                
                branches[i].NextElement = new WeightedNextElementPicker
                {
                    NextElementChances = { (process, 1) }
                };
                branches[i] = process;
                
                createdProcessesCount++;
                if (createdProcessesCount == processesCount)
                {
                    goto exitLoop;
                }
            }
        }
        
        exitLoop:

        int remainingProcessesCount = (processesCount - branchesCount) % branchesCount;
        for (int i = 0; i < remainingProcessesCount; i++)
        {
            var process = new SystemMO(delay, 1){ Name = $"PROCESS{createdProcessesCount + 1}" };
            elements.Add(process);
                
            branches[i].NextElement = new WeightedNextElementPicker
            {
                NextElementChances = { (process, 1) }
            };
            branches[i] = process;
                
            createdProcessesCount++;
        }
        
        // Console.WriteLine($"Created count: {createdProcessesCount}");

        return new NetMO(elements);
    }
}