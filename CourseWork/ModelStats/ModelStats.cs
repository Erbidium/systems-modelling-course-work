namespace Lab3.ModelStats;

public class ModelStats
{
    private readonly NetMO _model;
    
    public ModelStats(NetMO model)
    {
        _model = model;
    }
    
    public void DoStatistics(double delta) { }

    public void PrintModelStats(double currentTime) { }
}