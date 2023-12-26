using Lab3.Delays;
using Lab3.Nodes;
using Lab3.Queues;

namespace Lab3.Elements;

public class SystemMO : Element
{
    public Queue Queue { get; init; } = new();
    
    public double LoadTimeStat { get; private set; }
    public double NodesCountStat { get; private set; }
    public double MeanWorkingDevicesStat { get; private set; }

    public List<Device> Devices { get; } = new();

    private double _timeCurrent;

    public override double TimeCurrent
    {
        get => _timeCurrent;
        set
        {
            _timeCurrent = value;
            foreach (var device in Devices)
            {
                device.TimeCurrent = value;
            }
        }
    }
    
    public override double TimeNext => Devices.Count > 0 ? Devices.Min(d => d.TimeNext) : double.MaxValue;

    public override bool IsServing => Devices.Any(d => d.IsServing);

    public bool IsFull => Devices.All(d => d.IsServing);

    public SystemMO(IDelay delay, int devicesCount) : base(delay)
    {
        for (int i = 0; i < devicesCount; i++)
            Devices.Add(new Device(delay));
    }
    
    public override void Enter(Node node)
    {
        if (!IsFull)
        {
            var freeDevice = Devices.First(d => !d.IsServing);
            freeDevice.Enter(node);
        }
        else if (Queue.Nodes.Count < Queue.MaxCount)
        {
            Queue.Add(node);
        }
    }

    public override void Exit()
    {
        var finishedDevices = Devices.FindAll(d => d.TimeNext == TimeNext);
        
        ServedElementsQuantity += finishedDevices.Count;

        foreach (var device in finishedDevices)
        {
            Node processedNode = device.ProcessedNode!;
            device.Exit();
            
            if (Queue.Nodes.Count > 0)
            {
                var nodeFromQueue = Queue.Remove();
                device.Enter(nodeFromQueue);
            }
            
            NextElement?.NextElement(processedNode)?.Enter(processedNode);
        }
    }

    public override void DoStatistics(double delta)
    {
        if (!IsServing)
            return;

        int workingDevicesCount = Devices.Count(d => d.IsServing);
        MeanWorkingDevicesStat += workingDevicesCount * delta;
        NodesCountStat += (Queue.Nodes.Count + workingDevicesCount) * delta;
        
        LoadTimeStat += delta;
        
        foreach (var node in Queue.Nodes)
        {
            node.TimeSpentInQueue += delta;
        }

        foreach (var device in Devices)
        {
            device.DoStatistics(delta);
        }
    }
}
