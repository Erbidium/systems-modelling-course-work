using Lab3.Elements;

namespace Lab3.NextElement;

public class OneNextElementPicker : INextElementPicker
{
    private readonly Element _nextElement;
    public OneNextElementPicker(Element nextElement)
    {
        _nextElement = nextElement;
    }

    public Element NextElement(Node node)
    {
        return _nextElement;
    }
}