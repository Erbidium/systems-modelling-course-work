﻿using Lab3.Delays;

namespace Lab3.Elements;

public sealed class EndServing : Element
{
    public List<Node> ServedNodes { get; } = new();

    public EndServing(IDelay delay) : base(delay)
    {
        TimeNext = double.MaxValue;
    }

    public override void Enter(Node node)
    {
        node.EndServing(TimeCurrent);
        ServedNodes.Add(node);
        ServedElementsQuantity++;
    }

    public override void DoStatistics(double delta) { }
}