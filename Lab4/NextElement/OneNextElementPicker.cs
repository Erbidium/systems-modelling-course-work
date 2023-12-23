using Lab3.Elements;
using Lab3.Items;

namespace Lab3.NextElement;

public class OneNextElementPicker : INextElementPicker
{
    private readonly Element _nextElement;
    public OneNextElementPicker(Element nextElement)
    {
        _nextElement = nextElement;
    }

    public Element NextElement(SimpleItem item)
    {
        return _nextElement;
    }
}