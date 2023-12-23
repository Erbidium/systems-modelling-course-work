using Lab3.Delays;
using Lab3.Items;

namespace Lab3.Elements;

public class SystemMO : Element
{
    public int Failure { get; private set; }
    
    public double MeanQueueStat { get; private set; }
    public double LoadTimeStat { get; private set; }
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

    public override bool IsFull => Devices.All(d => d.IsServing);

    public SystemMO(IDelay delay, int devicesCount) : base(delay)
    {
        for (int i = 0; i < devicesCount; i++)
            Devices.Add(new Device(delay));
    }
    
    public override void Enter(SimpleItem item)
    {
        if (!IsFull)
        {
            var freeDevice = Devices.First(d => !d.IsServing);
            freeDevice.Enter(item);
        }
        else if (Queue.Items.Count < Queue.MaxCount)
        {
            Queue.Add(item);
        }
        else
        {
            Failure++;
        }
    }

    public override void Exit()
    {
        var finishedDevices = Devices.FindAll(d => d.TimeNext == TimeNext);
        
        ServedElementsQuantity += finishedDevices.Count;

        foreach (var device in finishedDevices)
        {
            SimpleItem processedItem = device.ProcessedItem!;
            device.Exit();
            
            if (Queue.Items.Count > 0)
            {
                var itemFromQueue = Queue.Remove();
                device.Enter(itemFromQueue);
            }
            
            NextElement?.NextElement(processedItem)?.Enter(processedItem);
        }
    }

    public override void PrintResult()
    {
        base.PrintResult();
        Console.WriteLine("Failure quantity = " + Failure);
    }
    
    public override void PrintInfo()
    {
        base.PrintInfo();
        // Console.WriteLine("Failure quantity = " + Failure);
    }

    public override void DoStatistics(double delta)
    {
        MeanQueueStat += Queue.Items.Count * delta;
        
        if (!IsServing)
            return;
        
        MeanWorkingDevicesStat += Devices.Count(d => d.IsServing) * delta;
        LoadTimeStat += delta;
    }
}
