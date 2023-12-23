namespace Lab3.ModelStats;

public interface IModelStatsPrinter
{
    public void DoStatistics(double delta);
    
    public void PrintModelStats(double currentTime);
}