namespace Monopoly;

public static class Util
{
    private static readonly Random Random = new();

    // the result of rolling a die, may be bigger than the player count?
    public static int RollDie(int UpperBound)
    {
        return Random.Next(1, UpperBound + 1);
    }

    public static int RollTwoDice()
    {
        return Random.Next(1, 6 + 1) + Random.Next(1, 6 + 1); ;
    }

    public static void ShuffleArray<T>(T[] array)
    {
        Random random = new();

        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);
            (array[randomIndex], array[i]) = (array[i], array[randomIndex]);
        }
    }
}