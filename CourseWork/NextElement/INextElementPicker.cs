using Lab3.Elements;
using Lab3.Nodes;

namespace Lab3.NextElement;

public interface INextElementPicker
{
    public Element? NextElement(Node node);
}