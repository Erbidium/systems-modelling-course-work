using Lab3.Nodes;

namespace Lab3.Delays;

public interface IDelay
{
    public double Generate(Node node);
}