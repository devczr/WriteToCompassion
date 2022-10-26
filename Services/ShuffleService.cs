
namespace WriteToCompassion.Services;

public static class ShuffleService
{
    private static Random rng = new();

    public static List<Thought> FYShuffle<Thought>(this List<Thought> thoughts)
    {
        for(int i = thoughts.Count -1; i > 0; --i)
        {
            int z = rng.Next(i + 1);
            Thought tmp = thoughts[z];
            thoughts[z] = thoughts[i];
            thoughts[i] = tmp;
        }
        return thoughts;
    }

}
