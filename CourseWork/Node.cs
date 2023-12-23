namespace Lab3;

public class Node
{
    public double CreationTime { get; }
    
    public double RepairTime { get;  }

    public int ReturnsCount { get; set; }

    public double FinishServingTime { get; private set; }
    
    public double TimeSpentInQueue { get; set; }

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