using Lab3.Delays;
using Lab3.ItemFactories;

namespace Lab3.Elements;

public sealed class Create : Element
{
    private readonly IItemFactory _itemFactory;
    
    public Create(IDelay delay, IItemFactory itemFactory) : base(delay)
    {
        TimeNext = 0.0; // імітація розпочнеться з події Create

        _itemFactory = itemFactory;
    }

    public override void Exit() {
        base.Exit();
        var createdItem = _itemFactory.CreateItem(TimeCurrent);
        
        TimeNext = TimeCurrent + GetDelay(createdItem);
        
        NextElement?.NextElement(createdItem)?.Enter(createdItem);
    }

    public override void DoStatistics(double delta) { }
}