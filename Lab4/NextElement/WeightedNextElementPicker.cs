using Lab3.Elements;
using Lab3.Items;

namespace Lab3.NextElement;

public class WeightedNextElementPicker : INextElementPicker
{
    public List<(Element Element, double Chance)> NextElementChances = new();
    
    private readonly Random _rand = new();

    public Element NextElement(SimpleItem item)
    {
        var element = RandomHelper.GetWeightedRandomValue(NextElementChances, _rand);

        //Console.WriteLine($"To element {element.Name}");

        return element;
    }
}