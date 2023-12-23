using Lab3.Items;

namespace Lab3.Delays;

public interface IDelay
{
    public double Generate(SimpleItem item);
}