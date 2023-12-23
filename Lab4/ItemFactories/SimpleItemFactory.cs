using Lab3.Items;

namespace Lab3.ItemFactories;

public class SimpleItemFactory : IItemFactory
{
    public SimpleItem CreateItem(double currentTime)
    {
        return new SimpleItem();
    }
}