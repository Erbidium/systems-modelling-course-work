﻿using Lab3.Elements;

namespace Lab3;

public class NetMO {
    public List<Element> Elements { get; }
    
    private double _timeNext;
    
    private double _timeCurrent;
    
    public NetMO(List<Element> elements)
        => Elements = elements;

    public void Simulate(double time, ModelStats.ModelStats statsPrinter)
    {
        while (_timeCurrent < time)
        {
            _timeNext = Elements.Min(e => e.TimeNext);
            
            Elements.ForEach(e => e.DoStatistics(_timeNext - _timeCurrent));
            statsPrinter.DoStatistics(_timeNext - _timeCurrent);
            
            _timeCurrent = _timeNext;

            Elements.ForEach(e => e.TimeCurrent = _timeCurrent);

            Console.WriteLine($"-----Current time: {_timeCurrent}----");
            
            foreach (var element in Elements)
            {
                if (element.TimeNext == _timeCurrent)
                {
                    Console.WriteLine($"Next event will be in element {element.Name}");
                    element.Exit();
                }
            }
            
            PrintInfo();
        }
        PrintResult();
        statsPrinter.PrintModelStats(_timeCurrent);
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

        var processes = Elements.Where(el => el is SystemMO).Select(s => (s as SystemMO)!).ToList();
        
        Console.WriteLine($"Average nodes count in system{processes.Sum(p => p.NodesCountStat) / _timeCurrent}");
        Console.WriteLine();
        
        foreach (var element in Elements) {
            element.PrintResult();
            
            if (element is not SystemMO process)
                continue;
            
            Console.WriteLine($"Mean length of queue = {process.MeanQueueStat / _timeCurrent}");
            Console.WriteLine($"Failure probability = {process.Failure / (double) (process.Failure + process.ServedElementsQuantity)}");
            Console.WriteLine($"Failure rate = {process.Failure / (double) (process.Failure + process.ServedElementsQuantity) * 100} %");
            Console.WriteLine($"Average loading: {process.LoadTimeStat / _timeCurrent}");

            if (process.Devices.Count > 0)
            {
                Console.WriteLine("Devices load");
                for (int i = 0; i < process.Devices.Count; i++)
                {
                    Console.WriteLine($"Device{i} loading: {process.Devices[i].LoadTimeStat / _timeCurrent}");
                }
            }
            
            Console.WriteLine($"Average serving time: {process.LoadTimeStat / process.ServedElementsQuantity}");
            Console.WriteLine($"Average working devices: {process.MeanWorkingDevicesStat / _timeCurrent}");
        }
    }
}