namespace Lab3;

public static class RandomHelper
{
    public static T GetWeightedRandomValue<T>(List<(T, double)> weightedElements, Random random)
    {
        double totalChancesSum = weightedElements.Sum(el => el.Item2);
        double chanceGeneratedValue = random.NextDouble() * totalChancesSum;
            
        double chancesAccumulatedSum = 0;
        for (int i = 0; i < weightedElements.Count; i++)
        {
            chancesAccumulatedSum += weightedElements[i].Item2;
            if (chancesAccumulatedSum > chanceGeneratedValue)
            {
                return weightedElements[i].Item1;
            }
        }
        
        return default!;
    }
}