﻿using Lab3.Delays;
using Lab3.Nodes;

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
        ServedNodes.Add(node);
        ServedElementsQuantity++;
    }

    public override void DoStatistics(double delta) { }
}