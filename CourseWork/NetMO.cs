using Lab3.Elements;
using Lab3.ModelStats;
using Lab3.NextElement;

namespace Lab3;

public class NetMO {
    public List<Element> Elements { get; }
    
    private double _timeNext;
    
    private double _timeCurrent;
    
    public NetMO(List<Element> elements)
        => Elements = elements;

    public void Simulate(double time, IModelStatsPrinter? statsPrinter = null)
    {
        while (_timeCurrent < time)
        {
            _timeNext = Elements.Select(e => e.TimeNext).Min();
            
            Elements.ForEach(e => e.DoStatistics(_timeNext - _timeCurrent));
            statsPrinter?.DoStatistics(_timeNext - _timeCurrent);
            
            _timeCurrent = _timeNext;

            Elements.ForEach(e => e.TimeCurrent = _timeCurrent);

            //Console.WriteLine($"-----Current time: {_timeCurrent}----");
            
            foreach (var element in Elements)
            {
                if (element.TimeNext == _timeCurrent)
                {
                    //Console.WriteLine($"Next event will be in element {element.Name}");
                    element.Exit();
                }
            }
            
            //PrintInfo();
        }
        //PrintResult();
        //statsPrinter?.PrintModelStats(_timeCurrent);
    }

    private void PrintInfo()
    {
        foreach (var element in Elements)
        {
            element.PrintInfo();
        }
    }

    private void PrintResult()
    {
        Console.WriteLine("\n-------------RESULTS-------------");
        
        foreach (var element in Elements) {
            element.PrintResult();
            
            if (element is not SystemMO process)
                continue;
            
            Console.WriteLine($"Mean length of queue = {process.MeanQueueStat / _timeCurrent}");
            Console.WriteLine($"Failure probability = {process.Failure / (double) (process.Failure + process.ServedElementsQuantity)}");
            Console.WriteLine($"Failure rate = {process.Failure / (double) (process.Failure + process.ServedElementsQuantity) * 100} %");
            Console.WriteLine($"Average loading: {process.LoadTimeStat / _timeCurrent}");
            Console.WriteLine($"Average serving time: {process.LoadTimeStat / process.ServedElementsQuantity}");
            Console.WriteLine($"Average working devices: {process.MeanWorkingDevicesStat / _timeCurrent}");
        }
    }
}