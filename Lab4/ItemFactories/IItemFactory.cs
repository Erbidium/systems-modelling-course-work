using Lab3.Items;

namespace Lab3.ItemFactories;

public interface IItemFactory
{
    public SimpleItem CreateItem(double currentTime);
}