using Lab3.Elements;
using Lab3.Items;

namespace Lab3.NextElement;

public class PriorityNextElementPicker : INextElementPicker
{
    private readonly List<(Element Element, int Priority)> _nextElementPriorities;
    
    private readonly Random _rand = new();
    
    public PriorityNextElementPicker(List<(Element Element, int Priority)> nextElementPriorities)
    {
        _nextElementPriorities = nextElementPriorities;
    }

    public Element? NextElement(SimpleItem item)
    {
        var freeElements = _nextElementPriorities.Where(tuple => !tuple.Element.IsFull).ToList();

        if (freeElements.Count > 0)
            return FindElementWithMaxPriority(freeElements);
            
        var freeQueues = _nextElementPriorities.Where(tuple => tuple.Element.Queue.Items.Count < tuple.Element.Queue.MaxCount).ToList();

        if (freeQueues.Count > 0)
        {
            var minQueueLength = freeQueues
                .Select(t => t.Element.Queue.Items.Count)
                .Min();

            var elementsWithMinQueue = freeQueues.Where(t => t.Element.Queue.Items.Count == minQueueLength).ToList();
            return FindElementWithMaxPriority(elementsWithMinQueue);
        }

        return _nextElementPriorities.OrderByDescending(tuple => tuple.Priority).Select(t => t.Element).FirstOrDefault();

        Element? FindElementWithMaxPriority(IReadOnlyCollection<(Element Element, int Priority)> elements)
        {
            if (elements.Count == 0)
                return null;
                
            var maxFreePriority = elements.Select(tuple => tuple.Priority).Max();
            var freeElementsWithMaxPriority = elements.Where(tuple => tuple.Priority == maxFreePriority).ToList();
            return freeElementsWithMaxPriority[_rand.Next(freeElementsWithMaxPriority.Count)].Element;
        }
    }
}