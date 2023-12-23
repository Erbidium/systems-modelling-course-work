using Lab3.Elements;
using Lab3.Items;

namespace Lab3.NextElement;

public interface INextElementPicker
{
    public Element? NextElement(SimpleItem item);
}