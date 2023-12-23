using Lab3.Items;

namespace Lab3.Delays;

public class RepairDelay : IDelay
{
    public double Generate(SimpleItem item)
    {
        return (item as Node)!.RepairTime;
    }
}