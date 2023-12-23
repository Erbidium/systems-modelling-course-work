namespace Lab3;

public class Node
{
    public double RepairTime { get;  }

    public int ReturnsCount { get; set; }
    
    public double TimeSpentInQueue { get; set; }

    public Node(double repairTime)
    {
        RepairTime = repairTime;
    }
}