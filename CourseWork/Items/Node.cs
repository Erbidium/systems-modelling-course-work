using Lab3.Delays;

namespace Lab3.Items;

public class Node : SimpleItem
{
    public double CreationTime { get; }
    
    public double RepairTime { get;  }

    public int ReturnsCount { get; set; } = 0;

    public double FinishServingTime { get; private set; }

    public Node(double currentTime, double repairTime)
    {
        CreationTime = currentTime;
        RepairTime = repairTime;
    }

    public void EndServing(double currentTime)
    {
        FinishServingTime = currentTime;
    }
}