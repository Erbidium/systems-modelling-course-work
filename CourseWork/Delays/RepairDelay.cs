namespace Lab3.Delays;

public class RepairDelay : IDelay
{
    public double Generate(Node node)
    {
        return node.RepairTime;
    }
}