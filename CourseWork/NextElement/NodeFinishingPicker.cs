﻿using Lab3.Elements;

namespace Lab3.NextElement;

public class NodeFinishingPicker : INextElementPicker
{
    public required SystemMO RepairDepartment { get; init; }
    
    public required Element EndServing { get; init; }
    
    private readonly Random _rand = new();

    public Element NextElement(Node node)
    {
        var finishingProbability = node.ReturnsCount == 0 ? 0.15 : Math.Pow(0.15, node.ReturnsCount);
        var endServingProbability = 1 - finishingProbability;
        
        var element = RandomHelper.GetWeightedRandomValue(new List<(Element, double)>()
        {
            (RepairDepartment, finishingProbability),
            (EndServing, endServingProbability)
        }, _rand);

        if (element == RepairDepartment)
        {
            node.ReturnsCount++;
        }

        return element;
    }
}